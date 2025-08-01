using Gtk;

class MenuUsuario : Window
{
    public MenuUsuario() : base("Menu Usuario")
    {
        SetDefaultSize(400,300);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { Hide(); Program._login.ShowAll(); };

        Fixed contenedor = new Fixed();

        Button visVehiculo = new Button("Visualizar Vehiculos");
        Button visServicios = new Button("Visualizar Servicios");
        Button visFacturas = new Button("Visualizar Facturas");
        Button cerrarSesion = new Button("Cerrar Sesion");

        if (visVehiculo.Parent != null)
        {
            ((Container)visVehiculo.Parent).Remove(visVehiculo);
        }
        contenedor.Put(visVehiculo, 100, 60);
        if (visServicios.Parent != null)
        {
            ((Container)visServicios.Parent).Remove(visServicios);
        }
        contenedor.Put(visServicios, 100, 110);
        if (visFacturas.Parent != null)
        {
            ((Container)visFacturas.Parent).Remove(visFacturas);
        }
        contenedor.Put(visFacturas, 100, 160);
        if (cerrarSesion.Parent != null)
        {
            ((Container)cerrarSesion.Parent).Remove(cerrarSesion);
        }
        contenedor.Put(cerrarSesion, 100, 210);
        contenedor.SetSizeRequest(400, 300);
        visVehiculo.SetSizeRequest(200, 30);
        visServicios.SetSizeRequest(200, 30);
        visFacturas.SetSizeRequest(200, 30);
        cerrarSesion.SetSizeRequest(200, 30);

        visVehiculo.Clicked += (sender, e) =>
        {
            Program._visualizacionVehiculos = new VisualizacionVehiculos();
            Program._visualizacionVehiculos.ShowAll();
        };
        visServicios.Clicked += (sender, e) =>
        {
            Program._visualizacionServicios = new VisualizacionServicios();
            Program._visualizacionServicios.ShowAll();
        };
        visFacturas.Clicked += (sender, e) =>
        {
            Program._visualizacionFacturas = new VisualizacionFacturas();
            Program._visualizacionFacturas.ShowAll();
        };
        cerrarSesion.Clicked += (sender, e) =>
        {
            Program.RegistroSesiones.Add((Program.usuarioActual, Program.fechaActual, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            Program.usuarios.Backup();
            Program._login = new Login();
            Program._login.ShowAll();
            Hide();
        };
        Add(contenedor);
    }
}