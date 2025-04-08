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
    static void Main(string[] args)
    {
        Application.Init();
        _login.ShowAll();
        Application.Run();

    }
}