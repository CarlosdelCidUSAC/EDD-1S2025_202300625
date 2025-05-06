using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Diagnostics;

public class Usuario
{
    public string? ID { get; set; }
    public string?Nombres { get; set; }
    public string?Apellidos { get; set; }
    public string?Correo { get; set; }
    public int Edad { get; set; }
    public string? Contrasena { get; set; }
    

    public override string ToString()
    {
        return $"{ID}{Nombres}{Apellidos}{Correo}{Edad}{Contrasena}";
    }
}

public class Bloque
{
    public int Index { get; set; }
    public string? Timestamp { get; set; }
    public Usuario Data { get; set; }
    public int Nonce { get; set; }
    public string PreviousHash { get; set; }
    public string Hash { get; set; }

    public Bloque(int index, Usuario data, string previousHash)
    {
        Index = index;
        Timestamp = DateTime.Now.ToString("dd-MM-yy::HH:mm:ss");
        Data = data;
        PreviousHash = previousHash;
        Nonce = 0;
        Hash = CalcularHashConPruebaDeTrabajo();
    }

    public string CalcularHash()
    {
        string input = $"{Index}{Timestamp}{Data}{Nonce}{PreviousHash}";
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }

    private string CalcularHashConPruebaDeTrabajo()
    {
        string hash = CalcularHash();
        while (!hash.StartsWith("0000"))
        {
            Nonce++;
            hash = CalcularHash();
        }
        return hash;
    }
}

public class Blockchain
{
    public List<Bloque> Cadena { get; set; }

    public Blockchain(List<Bloque>? bloquesExistentes = null)
    {
        if (bloquesExistentes is null)
        {
            Cadena = new List<Bloque> { CrearBloqueGenesis() };
        }
        else
        {
            Cadena = bloquesExistentes;
        }
    }
    
    private static string HashSHA256(string input)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
    private Bloque CrearBloqueGenesis()
    {
        var usuarioGenesis = new Usuario
        {
            ID = "0",
            Nombres = "admin",
            Apellidos = "admin",
            Correo = "admin@usac.com",
            Edad = 0,
            Contrasena = HashSHA256("1234")
        };
        return new Bloque(0, usuarioGenesis, "0000");
    }

   public void AgregarBloque(Usuario nuevoUsuario)
    {
        if (Cadena.Any(b => b.Data.ID == nuevoUsuario.ID))
        {
            Console.WriteLine($"Error: Ya existe un usuario con el ID '{nuevoUsuario.ID}'.");
            return;
        }

        // Hasehar la contraseña del nuevo usuario
        nuevoUsuario.Contrasena = HashSHA256(nuevoUsuario.Contrasena);

        var anterior = Cadena.Last();
        var nuevoBloque = new Bloque(anterior.Index + 1, nuevoUsuario, anterior.Hash);
        Cadena.Add(nuevoBloque);
    }


    public Bloque BuscarBloque(int index)
    {
        var bloque = Cadena.FirstOrDefault(b => b.Index == index);
        if (bloque == null)
        {
            throw new InvalidOperationException($"No se encontró un bloque con el índice {index}.");
        }
        return bloque;
    }

    public void MostrarCadena()
    {
        foreach (var bloque in Cadena)
        {
            Console.WriteLine($"--- Bloque {bloque.Index} ---");
            Console.WriteLine($"Timestamp: {bloque.Timestamp}");
            Console.WriteLine($"Data: {bloque.Data}");
            Console.WriteLine($"Nonce: {bloque.Nonce}");
            Console.WriteLine($"Previous Hash: {bloque.PreviousHash}");
            Console.WriteLine($"Hash: {bloque.Hash}\n");
        }
    }

    public bool validarCredenciales(string correo, string contrasena)
    {
        string hashEntrada = HashSHA256(contrasena);
        return Cadena.Any(b =>
            b.Data.Correo == correo &&
            b.Data.Contrasena == hashEntrada
        );
    }

    public void Backup()
    {
        string filePath = "Backup/backup.json";
        var options = new JsonSerializerOptions
        {
            WriteIndented = true // para legibilidad
        };
        // Serializa la lista de bloques
        string json = JsonSerializer.Serialize(Cadena, options);
        // Asegura que la carpeta exista
        var dir = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(dir))
        {
            Directory.CreateDirectory(dir);
        }
        File.WriteAllText(filePath, json);
    }

    public static Blockchain? Restaurar()
    {
        string filePath = "Backup/backup.json";

        FileInfo fileInfo = new FileInfo(filePath);
        if (fileInfo.Length == 0){
            return null;
        }

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"No se encontró el archivo de backup: {filePath}");


        string json = File.ReadAllText(filePath);
        // Deserializa la lista de Bloque
       var bloques = JsonSerializer.Deserialize<List<Bloque>>(json) ?? new List<Bloque>();
        return new Blockchain(bloques);
    }


// Clase auxiliar para la deserialización intermedia
private class RegistroIntermedio
{
    public int Index { get; set; }
    public string? Timestamp { get; set; }
    public Usuario? Data { get; set; }
    public int Nonce { get; set; }
    public string? PreviousHash { get; set; }
    public string? Hash { get; set; }
}
    public void Graficar(){

        var dot = new StringBuilder();
        dot.AppendLine("digraph G {");
        dot.AppendLine("node [shape=record, fontsize=10];");
        dot.AppendLine("  graph [rankdir=TB];"); 
        dot.AppendLine("  subgraph cluster_0 {"); 
        dot.AppendLine("    label=\"Cadena de Bloques\";");

        foreach (var bloque in Cadena)
        {
            string data = $"ID: {bloque.Data.ID}\\nNombres: {bloque.Data.Nombres}\\nApellidos: {bloque.Data.Apellidos}\\nCorreo: {bloque.Data.Correo}\\nEdad: {bloque.Data.Edad}";
            string etiquetaNodo = $"\"Bloque: {bloque.Index}\\nTimestamp: {bloque.Timestamp}\\nNonce: {bloque.Nonce}\\nHash: {bloque.Hash}\\n{data}\"";
            dot.AppendLine($"{bloque.Index} [label={etiquetaNodo}];");
            if (bloque.Index > 0)
            {
                dot.AppendLine($"{bloque.Index - 1} -> {bloque.Index};");
            }
        }
        dot.AppendLine("}");
        dot.AppendLine("}"); // Cierra el grafo
        string rutaDot = "reportedot/Blockchain.dot";
        string rutaReporte = "Reportes/Blockchain.png";
        // Ensure the directory exists before writing the file
        var dirDot = Path.GetDirectoryName(rutaDot);
        if (!string.IsNullOrEmpty(dirDot))
        {
            Directory.CreateDirectory(dirDot);
        }
        var dirReporte = Path.GetDirectoryName(rutaReporte);
        if (!string.IsNullOrEmpty(dirReporte))
        {
            Directory.CreateDirectory(dirReporte);
        }
        File.WriteAllText(rutaDot, dot.ToString());
        var proceso = new Process();
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
}
