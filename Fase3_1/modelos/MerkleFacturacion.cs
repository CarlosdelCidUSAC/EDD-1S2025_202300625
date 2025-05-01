using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Gtk;

public class Factura
{
    public int ID { get; set; }
    public int ID_Servicio { get; set; }
    public float Total { get; set; }
    public string Fecha { get; set; }
    public string MetodoPago { get; set; }

    public override string ToString()
    {
        return $"{ID}-{ID_Servicio}-{Total}-{Fecha}-{MetodoPago}";
    }
}

public class MerkleNode
{
    public MerkleNode Left { get; set; }
    public MerkleNode Right { get; set; }
    public string Hash { get; set; }
    public Factura Data { get; set; }

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
    public MerkleNode Root { get; private set; }

    public MerkleTree(List<Factura> facturas)
    {
        List<MerkleNode> nodes = new List<MerkleNode>();
        foreach (var factura in facturas)
        {
            string hash = MerkleNode.ComputeHash(factura.ToString());
            nodes.Add(new MerkleNode(hash, factura));
        }

        while (nodes.Count > 1)
        {
            List<MerkleNode> newLevel = new List<MerkleNode>();
            for (int i = 0; i < nodes.Count; i += 2)
            {
                if (i + 1 < nodes.Count)
                    newLevel.Add(new MerkleNode(nodes[i], nodes[i + 1]));
                else
                    newLevel.Add(new MerkleNode(nodes[i], nodes[i])); // Duplicar si hay nodo impar
            }
            nodes = newLevel;
        }

        Root = nodes.Count > 0 ? nodes[0] : null;
    }

    public string GetRootHash()
    {
        return Root != null ? Root.Hash : string.Empty;
    }

    public void AgregarFactura(Factura factura)
    {
        if (Root == null)
        {
            Root = new MerkleNode(MerkleNode.ComputeHash(factura.ToString()), factura);
            return;
        }

        string hash = MerkleNode.ComputeHash(factura.ToString());
        MerkleNode newNode = new MerkleNode(hash, factura);
        List<MerkleNode> nodes = new List<MerkleNode> { Root, newNode };

        while (nodes.Count > 1)
        {
            List<MerkleNode> newLevel = new List<MerkleNode>();
            for (int i = 0; i < nodes.Count; i += 2)
            {
                if (i + 1 < nodes.Count)
                    newLevel.Add(new MerkleNode(nodes[i], nodes[i + 1]));
                else
                    newLevel.Add(new MerkleNode(nodes[i], nodes[i]));
            }
            nodes = newLevel;
        }

        Root = nodes[0];
    }

    public void Imprimir()
    {
        ImprimirRecursivo(Root);
    }

    private void ImprimirRecursivo(MerkleNode nodo)
    {
        if (nodo == null)
            return;

        ImprimirRecursivo(nodo.Left);
        Console.WriteLine(nodo.Hash);
        ImprimirRecursivo(nodo.Right);
    }

    public ListStore MostrarTabla()
    {
        ListStore modelo = new ListStore(typeof(string));
        ImprimirRecursivo(Root, modelo);
        return modelo;
    }

    private void ImprimirRecursivo(MerkleNode nodo, ListStore modelo)
    {
        if (nodo == null)
            return;

        ImprimirRecursivo(nodo.Left, modelo);
        modelo.AppendValues(nodo.Hash);
        ImprimirRecursivo(nodo.Right, modelo);
    }

    public void Graficar()
    {
        StringBuilder dot = new StringBuilder();
        dot.AppendLine("digraph G {");
        dot.AppendLine("  node [shape=record, fontsize=10];");
        dot.AppendLine("  graph [rankdir=TB];");
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

        File.WriteAllText(rutaDot, dot.ToString());

        try
        {
            Process proceso = new Process();
            proceso.StartInfo.FileName = "dot";
            proceso.StartInfo.Arguments = $"-Tpng \"{rutaDot}\" -o \"{rutaReporte}\"";
            proceso.StartInfo.RedirectStandardOutput = true;
            proceso.StartInfo.RedirectStandardError = true;
            proceso.StartInfo.UseShellExecute = false;
            proceso.StartInfo.CreateNoWindow = true;
            proceso.Start();
            proceso.WaitForExit();

            if (proceso.ExitCode != 0)
            {
                Console.WriteLine("[ERROR] Error al generar el gráfico:");
                Console.WriteLine(proceso.StandardError.ReadToEnd());
            }
            else
            {
                Console.WriteLine("[OK] Gráfico generado en " + rutaReporte);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("[ERROR] No se pudo ejecutar Graphviz: " + ex.Message);
        }
    }

    private void GraficarRecursivo(MerkleNode nodo, StringBuilder dot)
    {
        if (nodo == null)
            return;

        string nodoId = "n" + nodo.Hash.Substring(0, 8);

        if (nodo.Data != null)
        {
            string label = nodo.Hash + "\\n" +
            $"ID:{nodo.Data.ID}, " +
            $"Serv:{nodo.Data.ID_Servicio}, " +
            $"Total:{nodo.Data.Total}, " +
            $"Fecha:{nodo.Data.Fecha}, " +
            $"Pago:{nodo.Data.MetodoPago}";

            label = label.Replace("\"", "\\\""); // Escapar comillas dobles si hay

            dot.AppendLine($"    {nodoId} [label=\"{label}\"];");
        }
        else
        {
            dot.AppendLine($"    {nodoId} [label=\"{nodo.Hash}\"];");
        }

        if (nodo.Left != null)
        {
            string leftId = "n" + nodo.Left.Hash.Substring(0, 8);
            dot.AppendLine($"    {nodoId} -> {leftId};");
            GraficarRecursivo(nodo.Left, dot);
        }
        if (nodo.Right != null)
        {
            string rightId = "n" + nodo.Right.Hash.Substring(0, 8);
            dot.AppendLine($"    {nodoId} -> {rightId};");
            GraficarRecursivo(nodo.Right, dot);
        }
    }
}
