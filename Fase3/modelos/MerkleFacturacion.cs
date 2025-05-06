using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Gtk;

public class Factura
{
    public int ID { get; set; }
    public int ID_Servicio { get; set; }
    public float Total { get; set; }
    public string? Fecha { get; set; }
    public string? MetodoPago { get; set; }

    public override string ToString()
    {
        return $"{ID}-{ID_Servicio}-{Total}-{Fecha}-{MetodoPago}";
    }
}

public class MerkleNode
{
    public MerkleNode? Left { get; set; }
    public MerkleNode? Right { get; set; }
    public string Hash { get; set; }
    public Factura? Data { get; set; }

    public MerkleNode(string hash, Factura data)
    {
        Hash = hash;
        Data = data;
    }

    public MerkleNode(MerkleNode left, MerkleNode right)
    {
        Left = left;
        Right = right;
        Hash = ComputeHash(left.Hash + right.Hash);
    }

    public static string ComputeHash(string input)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}

public class MerkleTree
{
    private List<Factura> _facturas;
    public MerkleNode? Root { get; private set; }

    public MerkleTree(IEnumerable<Factura> facturas)
    {
        _facturas = new List<Factura>(facturas);
        BuildTree();
    }

    public void AgregarFactura(Factura factura)
    {
        _facturas.Add(factura);
        BuildTree();
    }

    private void BuildTree()
    {
        var nodes = _facturas
            .Select(f => new MerkleNode(MerkleNode.ComputeHash(f.ToString()), f))
            .ToList();

        while (nodes.Count > 1)
        {
            var next = new List<MerkleNode>();
            for (int i = 0; i < nodes.Count; i += 2)
            {
                var left = nodes[i];
                var right = (i + 1 < nodes.Count) ? nodes[i + 1] : left;
                next.Add(new MerkleNode(left, right));
            }
            nodes = next;
        }

        Root = nodes.FirstOrDefault();
    }

    public List<(string siblingHash, bool isLeft)> GetProof(int index)
    {
        var leafHashes = _facturas
            .Select(f => MerkleNode.ComputeHash(f.ToString()))
            .ToList();

        var proof = new List<(string siblingHash, bool isLeft)>();
        BuildProof(leafHashes, index, proof);
        return proof;
    }

    private void BuildProof(List<string> levelHashes, int idx, List<(string siblingHash, bool isLeft)> proof)
    {
        if (levelHashes.Count <= 1) return;

        var nextLevel = new List<string>();
        for (int i = 0; i < levelHashes.Count; i += 2)
        {
            string left = levelHashes[i];
            string right = (i + 1 < levelHashes.Count) ? levelHashes[i + 1] : left;
            nextLevel.Add(MerkleNode.ComputeHash(left + right));

            // Si el índice se encuentra en este par, agregamos el hermano y su posición
            if (i == idx || i + 1 == idx)
            {
                bool isLeft = (i != idx);
                string siblingHash = isLeft ? left : right;
                proof.Add((siblingHash, isLeft));
                idx = nextLevel.Count - 1;
            }
        }
        BuildProof(nextLevel, idx, proof);
    }

    public static bool VerifyProof(string leafHash, List<(string siblingHash, bool isLeft)> proof, string rootHash)
    {
        string computed = leafHash;
        foreach (var (sibling, isLeft) in proof)
        {
            computed = isLeft
                ? MerkleNode.ComputeHash(sibling + computed)
                : MerkleNode.ComputeHash(computed + sibling);
        }
        return computed == rootHash;
    }

    public string GetRootHash() => Root?.Hash ?? string.Empty;

    public void Imprimir()
    {
        ImprimirRecursivo(Root!);
    }

    public bool EstaVacia()
    {
        return Root == null;
    }
    private void ImprimirRecursivo(MerkleNode nodo)
    {
        if (nodo == null)
            return;

        if (nodo.Left != null)
            ImprimirRecursivo(nodo.Left);

        if (nodo.Data != null)
        {
            Console.WriteLine($"{nodo.Hash} | ID: {nodo.Data.ID}, Servicio: {nodo.Data.ID_Servicio}, Total: {nodo.Data.Total}, Fecha: {nodo.Data.Fecha}, Pago: {nodo.Data.MetodoPago}");
        }
        else
        {
            Console.WriteLine(nodo.Hash);
        }

        if (nodo.Right != null)
            ImprimirRecursivo(nodo.Right);
    }

    public ListStore CrearModeloTabla(MerkleTree tree)
    {
        var store = new ListStore(typeof(string), typeof(string), typeof(string));
        foreach (var node in tree.InOrderNodes())
        {
            if (node.Data != null)
            {
                store.AppendValues(
                    node.Data.ID.ToString(),
                    node.Data.ID_Servicio.ToString(),
                    node.Data.Total.ToString("F2")
                );
            }
        }
        return store;
    }

    private void ImprimirRecursivo(MerkleNode nodo, ListStore modelo)
    {
        if (nodo == null)
            return;

        if (nodo.Left != null)
            ImprimirRecursivo(nodo.Left, modelo);
        modelo.AppendValues(nodo.Hash);
        if (nodo.Right != null)
            ImprimirRecursivo(nodo.Right, modelo);
    }

    public void Graficar()
    {
        StringBuilder dot = new StringBuilder();
        dot.AppendLine("digraph G {");
        dot.AppendLine("  node [shape=record, fontsize=10];");
        dot.AppendLine("  graph [rankdir=BT];"); // Cambiado a BT para invertir la dirección
        dot.AppendLine("  subgraph cluster_0 {");
        dot.AppendLine("    label=\"Facturas\";");

        if (Root != null)
            GraficarRecursivo(Root, dot);

        dot.AppendLine("  }");
        dot.AppendLine("}");

        string rutaDot = "reportedot/MerkleTree.dot";
        string rutaReporte = "Reportes/MerkleTree.png";

        Directory.CreateDirectory("reportedot");
        Directory.CreateDirectory("Reportes");

        try
        {
            File.WriteAllText(rutaDot, dot.ToString());
            Process proceso = new Process();
            proceso.StartInfo.FileName = "dot";
            proceso.StartInfo.Arguments = $"-Tpng {rutaDot} -o {rutaReporte}";
            proceso.StartInfo.RedirectStandardOutput = true;
            proceso.StartInfo.UseShellExecute = false;
            proceso.StartInfo.CreateNoWindow = true;
            proceso.Start();
            proceso.WaitForExit();

            if (File.Exists(rutaReporte))
            {
                Console.WriteLine("Reporte generado con éxito");
                Process.Start(new ProcessStartInfo(rutaReporte) { UseShellExecute = true });
            }
            else
            {
                Console.WriteLine("Error al generar el reporte");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("[ERROR] No se pudo ejecutar Graphviz: " + ex.Message);
        }
    }

    private void GraficarRecursivo(MerkleNode nodo, StringBuilder dot)
    {
        if (nodo == null) return;
        string id = $"n{nodo.Hash.Substring(0, 8)}";
        string label;
        if (nodo.Data != null)
        {
            // Mejor presentación: cada campo en una línea
            label = $"{Escape(nodo.Hash)}\\l"
                  + $"ID: {nodo.Data.ID}\\l"
                  + $"Servicio: {nodo.Data.ID_Servicio}\\l"
                  + $"Total: {nodo.Data.Total}\\l"
                  + $"Fecha: {Escape(nodo.Data.Fecha ?? string.Empty)}\\l"
                  + $"Pago: {Escape(nodo.Data.MetodoPago ?? string.Empty)}\\l";
        }
        else
        {
            label = Escape(nodo.Hash);
        }
        dot.AppendLine($"    {id} [label=\"{label}\"];");
        if (nodo.Left != null)
        {
            var lid = $"n{nodo.Left.Hash.Substring(0, 8)}";
            dot.AppendLine($"    {lid} -> {id};"); // Invertir la flecha
            GraficarRecursivo(nodo.Left, dot);
        }
        if (nodo.Right != null)
        {
            var rid = $"n{nodo.Right.Hash.Substring(0, 8)}";
            dot.AppendLine($"    {rid} -> {id};"); // Invertir la flecha
            GraficarRecursivo(nodo.Right, dot);
        }
    }


    private string Escape(string s) => s.Replace("\"", "\\\"");

    public IEnumerable<MerkleNode> InOrderNodes()
    {
        return TraverseInOrder(Root!);
    }

    private IEnumerable<MerkleNode> TraverseInOrder(MerkleNode node)
    {
        if (node == null) yield break;
        if (node.Left != null)
            foreach (var n in TraverseInOrder(node.Left)) yield return n;
        yield return node;
        if (node.Right != null)
            foreach (var n in TraverseInOrder(node.Right)) yield return n;
    }


}
