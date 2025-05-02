using Gtk;

class MenuUsuario : Window
{
    public MenuUsuario() : base("Menu Usuario")
    {
        SetDefaultSize(400,300);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate {  Program._login.ShowAll(); Destroy(); };

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
            if (Program._visualizacionVehiculos == null || !Program._visualizacionVehiculos.Visible)
            {
                Program._visualizacionVehiculos = new VisualizacionVehiculos();
                Program._visualizacionVehiculos.ShowAll();
            }
            else
            {
                Program._visualizacionVehiculos.Present();
            }
        };
        visServicios.Clicked += (sender, e) =>
        {
           if (Program._visualizacionServicios == null || !Program._visualizacionServicios.Visible)
            {
                Program._visualizacionServicios = new VisualizacionServicios();
                Program._visualizacionServicios.ShowAll();
            }
            else
            {
                Program._visualizacionServicios.Present();
            }
        };
        visFacturas.Clicked += (sender, e) =>
        {
            if (Program._visualizacionFacturas == null || !Program._visualizacionFacturas.Visible)
            {
                Program._visualizacionFacturas = new VisualizacionFacturas();
                Program._visualizacionFacturas.ShowAll();
            }
            else
            {
                Program._visualizacionFacturas.Present();
            }
        };
        cerrarSesion.Clicked += (sender, e) =>
        {
            Program.ExportarRegistroSesiones("Reportes/RegistroSesiones.json");
            Program.usuarios.Backup();
            
            if (Program._login == null || !Program._login.Visible)
            {
                Program._login = new Login();
                Program._login.ShowAll();
            }
            else
            {
                Program._login.Present();
            }
            Destroy();
        };
        Add(contenedor);
    }
}