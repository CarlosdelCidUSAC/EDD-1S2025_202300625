using System;
using System.Text;
using System.Security.Cryptography;

public class Usuario
{
    public string ID { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Correo { get; set; }
    public int Edad { get; set; }
    public string Contrasena { get; set; }

    public override string ToString()
    {
        return $"{ID}{Nombres}{Apellidos}{Correo}{Edad}{Contrasena}";
    }
}

public class Bloque
{
    public int Index { get; set; }
    public string Timestamp { get; set; }
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

    public Blockchain()
    {
        Cadena = new List<Bloque>();
        Cadena.Add(CrearBloqueGenesis());
    }

    private Bloque CrearBloqueGenesis()
    {
        Usuario usuarioGenesis = new Usuario
        {
            ID = "0",
            Nombres = "Genesis",
            Apellidos = "Block",
            Correo = "genesis@blockchain.com",
            Edad = 0,
            Contrasena = "root"
        };
        return new Bloque(0, usuarioGenesis, "0000");
    }

    public void AgregarBloque(Usuario nuevoUsuario)
    {
        Bloque anterior = Cadena.Last();
        Bloque nuevoBloque = new Bloque(anterior.Index + 1, nuevoUsuario, anterior.Hash);
        Cadena.Add(nuevoBloque);
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
}
