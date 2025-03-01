using System;
using Gtk;

 


    class Program
{
    public static UsuariosLista listaUsuarios = new UsuariosLista();
    public static VehiculoListaDoble listaVehiculos = new VehiculoListaDoble();
    public static RepuestosListaCircular listaRepuestos = new RepuestosListaCircular();
    public static SeviciosCola colaServicios = new SeviciosCola();
    public static FacturasPila pilaFacturas = new FacturasPila();

    public static BitacoraMatrizDispersa bitacora = new BitacoraMatrizDispersa();
    static void Main()
    {

        Application.Init();
        LoginWindow win = new LoginWindow();
        win.ShowAll();

        MenuWindow menu = new MenuWindow();
        menu.ShowAll();
        /* 
        CargaWindow carga = new CargaWindow();
        carga.ShowAll();
        UsuarioGestionWindow usuario = new UsuarioGestionWindow();
        usuario.ShowAll();
        VehiculoIngresoWindow vehiculo = new VehiculoIngresoWindow();
        vehiculo.ShowAll();
        RepuestoIngresoWindow repuesto = new RepuestoIngresoWindow();
        repuesto.ShowAll();
        UsuarioGestionWindow usuarioG = new UsuarioGestionWindow();
        usuarioG.ShowAll();
        GenerarServicioWindow servicio = new GenerarServicioWindow();
        servicio.ShowAll();
        GenerarFacturaWindow factura = new GenerarFacturaWindow(); 
        factura.ShowAll();
        VentanaEmergente ventana = new VentanaEmergente();
        ventana.ShowAll();
         */
         Application.Run();
    }
}
