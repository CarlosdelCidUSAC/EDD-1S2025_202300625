using Gtk;
class Program
{
    public static CargaMasiva _cargaMasiva = new();
    public static InsertarUsuario _insertarUsuario = new();
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
        _login.ShowAll();
        Application.Run();
        Facturas ??= new();
        merkle = new(Facturas);
        usuarios = Blockchain.Restaurar();

    }
}