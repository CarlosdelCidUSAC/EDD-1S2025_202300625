using System;
using Gtk;

class Program
{
    public static listaUsuarios listaUsuarios = new listaUsuarios();
    public static ListaVehiculos listaVehiculos = new ListaVehiculos();
    public static ArbolRepuestos arbolRepuestos = new ArbolRepuestos();
    public static AVLServicios arbolServicios = new AVLServicios();
    public static ArbolBFacturas arbolFacturas = new ArbolBFacturas();
    public static DateTime fechaActual = DateTime.Now;

    public static int idUsuarioActual = 0;
    public static List<(string correo, DateTime entrada, DateTime salida)> registroUsuarios = new List<(string, DateTime, DateTime)>();
        static void Main()
    {
        Application.Init();
        LoginWindow win = new LoginWindow();
        Application.Run();
    }


}