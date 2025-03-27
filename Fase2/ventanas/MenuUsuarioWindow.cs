using Gtk;

class MenuUsuarioWindow : Window
{
    public NodoUsuario? usuario = Program.listaUsuarios.Buscar(Program.idUsuarioActual);
    private RegistroVehiculoWindow _windowRegistroVehiculo;
    private VisualizacionServicioWindow _windowVisualizacionServicio;
    private VisualizacionFacturasWindow _windowVisualizacionFacturas;
    private CancelarFacturaWindow _windowCancelarFactura;

    public void ShowWindowRegistroVehiculo()
    {
        _windowRegistroVehiculo = new RegistroVehiculoWindow();
        _windowRegistroVehiculo.Show();
    }

    public void ShowWindowVisualizacionServicio()
    {
        _windowVisualizacionServicio = new VisualizacionServicioWindow();
        _windowVisualizacionServicio.Show();
    }

    public void ShowWindowVisualizacionFacturas()
    {
        _windowVisualizacionFacturas = new VisualizacionFacturasWindow();
        _windowVisualizacionFacturas.Show();
    }

    public void ShowWindowCancelarFactura()
    {
        _windowCancelarFactura = new CancelarFacturaWindow();
        _windowCancelarFactura.Show();
    }
    public MenuUsuarioWindow() : base("Menú Administrador")
    {
        SetDefaultSize(400, 650);
        SetPosition(WindowPosition.Center);
        DeleteEvent += OnDeleteEvent;

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Menú Administrador");
        Button botonInsertar = new Button("Insertar vehiculo");
        Button botonVisSer = new Button("Visualizacion de servicios");
        Button botonVisFac = new Button("Visualizacion de facturas");
        Button botonCanFac = new Button("Cancelar factura");


        contenedor.Put(etiquetaTitulo, 80, 20);
        contenedor.Put(botonInsertar, 100, 100);
        contenedor.Put(botonVisSer, 100, 180);
        contenedor.Put(botonVisFac,100, 260);
        contenedor.Put(botonCanFac,100, 340);

        

        botonInsertar.Clicked += (sender, e) =>
        {
           ShowWindowRegistroVehiculo();
        };

        botonVisSer.Clicked += (sender, e) =>
        {
            ShowWindowVisualizacionServicio();
        };

        botonVisFac.Clicked += (sender, e) =>
        {
            ShowWindowVisualizacionFacturas();
        };

        botonCanFac.Clicked += (sender, e) =>
        {
            ShowWindowCancelarFactura();
        };


        Add(contenedor);
        ShowAll();
    }

    public void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        a.RetVal = true;
        LoginWindow login = new LoginWindow();
        login.ShowAll();  
        if (usuario != null)
        {
            Program.registroUsuarios.Add((usuario.correo, Program.fechaActual, DateTime.Now));
        }
        Destroy();

    }
}