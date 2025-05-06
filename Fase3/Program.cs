using Gtk;
using System.Text.Json;
using System.Text.Json.Serialization;
public class SesionRegistro
{
    [JsonPropertyName("usuario")]
    public string? Usuario { get; set; }

    [JsonPropertyName("fechaEntrada")]
    public string? FechaEntrada { get; set; }

    [JsonPropertyName("fechaSalida")]
    public string? FechaSalida { get; set; }
}
class Program
{
    public static CargaMasiva _cargaMasiva = new();
    public static InsertarUsuario? _insertarUsuario = null;
    public static CrearServicio _crearServicio = new();
    public static VisualizacionRepuestos _visualizacionRepuestos = new();
    public static VisualizacionVehiculos _visualizacionVehiculos = new();
    public static VisualizacionServicios _visualizacionServicios = new();
    public static VisualizacionFacturas _visualizacionFacturas = new();

    public static MenuAdmin _menuAdmin = new();
    public static MenuUsuario _menuUsuario = new();
    public static Login _login = new();

    public static Blockchain usuarios = new();
    public static AVLRepuestos repuestos = new();
    public static ListaVehiculos vehiculos = new();
    public static BSTServicios servicios = new();
    public static ListaDeLista grafo = new();
    public static List<Factura> Facturas = new();
    public static MerkleTree merkle = new(Facturas);
    public static string usuarioActual = "";
    public static string fechaActual = "";
    public static List<(string Usuario, string FechaEntrada, string FechaSalida)> RegistroSesiones = new();

    
    static void Main()
    {
        Application.Init();
        Facturas ??= new();
        merkle = new(Facturas);
        usuarios = Blockchain.Restaurar() ?? new Blockchain();
        if(usuarios.ValidarCadena())
        {   
            MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Cadena de bloques restaurada correctamente.");
            dialog.Title = "Restauración de cadena de bloques";
            dialog.Run();
            dialog.Destroy();
            Console.WriteLine("Cadena de bloques restaurada correctamente.");
        }
        else
        {
            MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Error al restaurar la cadena de bloques.");
            dialog.Title = "Error de restauración";
            dialog.Run();
            dialog.Destroy();
            Console.WriteLine("Error al restaurar la cadena de bloques.");
            usuarios = new Blockchain();
        }
        vehiculos.Restaurar();
        repuestos.Restaurar();
        _login.ShowAll();
        try
        {
            Application.Run();
        }
        finally
        {
            Application.Quit();
        }
    }
    
    public static void ExportarRegistroSesiones(string rutaArchivo)
    {

        var listaSesiones = RegistroSesiones
            .Select(t => new SesionRegistro
            {
                Usuario     = t.Usuario,
                FechaEntrada = t.FechaEntrada,
                FechaSalida  = t.FechaSalida
            })
            .ToList();

        var opciones = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(listaSesiones, opciones);

        var directorio = Path.GetDirectoryName(rutaArchivo);
        if (!string.IsNullOrEmpty(directorio))
            Directory.CreateDirectory(directorio);

        File.WriteAllText(rutaArchivo, json);
    }
}


