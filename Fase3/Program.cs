using System;
using Gtk;
class Program
{
    public static CargaMasiva _cargaMasiva = new CargaMasiva();
    public static InsertarUsuario _insertarUsuario = new InsertarUsuario();
    public static CrearServicio _crearServicio = new CrearServicio();
    public static VisualizacionRepuestos _visualizacionRepuestos = new VisualizacionRepuestos();
    public static VisualizacionVehiculos _visualizacionVehiculos = new VisualizacionVehiculos();
    public static VisualizacionServicios _visualizacionServicios = new VisualizacionServicios();
    public static VisualizacionFacturas _visualizacionFacturas = new VisualizacionFacturas();

    public static MenuAdmin _menuAdmin = new MenuAdmin();
    public static MenuUsuario _menuUsuario = new MenuUsuario();
    public static Login _login = new Login();

    public static Blockchain usuarios = new Blockchain();
    public static AVLRepuestos repuestos = new AVLRepuestos();
    public static ListaVehiculos vehiculos = new ListaVehiculos();
    public static BSTServicios servicios = new BSTServicios();
    public static ListaDeLista grafo = new ListaDeLista();
    public static List<Factura> Facturas = new List<Factura>();
    public static MerkleTree merkle = new MerkleTree(Facturas);
    public static string usuarioActual = "";
    public static string fechaActual = "";
    public static List<(string Usuario, string FechaEntrada, string FechaSalida)> RegistroSesiones = new List<(string, string, string)>();

    
    static void Main(string[] args)
    {
        if (Facturas == null)
        {
            Facturas = new List<Factura>();
        }
        merkle = new MerkleTree(Facturas);
        // usuarios = Blockchain.Restaurar();
        Application.Init();
        _login.ShowAll();
        Application.Run();

    }
}