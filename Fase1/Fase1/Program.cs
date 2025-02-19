using System;
using Gtk;

namespace Fase1
{
    class Program
{
    static void Main()
    {
        Application.Init();
        LoginWindow win = new LoginWindow();
        win.ShowAll();
        //DEsplegue de las ventanas como prueba
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
        Application.Run();
    }
}
}