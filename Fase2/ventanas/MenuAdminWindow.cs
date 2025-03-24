using Gtk;

class MenuAdminWindow : Window
{
    private GenerarServicioWindow _windowGenerarServicio;
    private CargaWindow _windowCarga;
    private GestionEntidadesWindow _windowGestionEntidades;
    private ActualizarRespuestosWindow _windowActualizarRepuestos;
    private VisualizacionRepuestosWindow _windowVisualizacionRepuestos;

    public void ShowWindowGenerarServicio()
    {
        _windowGenerarServicio = new GenerarServicioWindow();
        _windowGenerarServicio.Show();
    }

    public void ShowWindowCarga()
    {
        _windowCarga = new CargaWindow();
        _windowCarga.Show();
    }

    public void ShowWindowGestionEntidades()
    {
        _windowGestionEntidades = new GestionEntidadesWindow();
        _windowGestionEntidades.Show();
    }

    public void ShowWindowActualizarRepuestos()
    {
        _windowActualizarRepuestos = new ActualizarRespuestosWindow();
        _windowActualizarRepuestos.Show();
    }

    public void ShowWindowVisualizacionRepuestos()
    {
        _windowVisualizacionRepuestos = new VisualizacionRepuestosWindow();
        _windowVisualizacionRepuestos.Show();
    }

    public MenuAdminWindow() : base("Menú Administrador")
    {
        SetDefaultSize(400, 650);
        SetPosition(WindowPosition.Center);
        DeleteEvent += (o, args) => Application.Quit();

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Menú Administrador");
        Button botonCarga = new Button("Cargar Archivo");
        Button botonGestion = new Button("Gestión de Entidades");
        Button botonActRep = new Button("Actualizar repuestos");
        Button botonVisRep = new Button("Visualizar repuestos");
        Button botonGenerar = new Button("Generar servicio");
        Button botonCtrlLog = new Button("Control de Logueo");
        Button Reporte = new Button("Reporte");

        contenedor.Put(etiquetaTitulo, 80, 20);
        contenedor.Put(botonCarga, 100, 100);
        contenedor.Put(botonGestion, 100, 180);
        contenedor.Put(botonActRep, 100, 260);
        contenedor.Put(botonVisRep, 100, 340);
        contenedor.Put(botonGenerar, 100, 420);
        contenedor.Put(botonCtrlLog, 100, 500);
        contenedor.Put(Reporte, 100, 580);

        botonCarga.Clicked += (sender, e) =>
        {
            ShowWindowCarga();
        };

        botonGestion.Clicked += (sender, e) =>
        {
            ShowWindowGestionEntidades();
        };

        botonGenerar.Clicked += (sender, e) =>
        {
            ShowWindowGenerarServicio();
        };

        botonActRep.Clicked += (sender, e) =>
        {
            ShowWindowActualizarRepuestos();
        };

        botonVisRep.Clicked += (sender, e) =>
        {
            ShowWindowVisualizacionRepuestos();
        };

        botonCtrlLog.Clicked += (sender, e) =>
        {
            // Proximamente
        };

        Reporte.Clicked += (sender, e) =>
        {
            // Proximamente
        };

        Add(contenedor);
        ShowAll();
    }
}