using Gtk;
using System;

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
        DeleteEvent += OnDeleteEvent;

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
            string folderPath = "Reportes";
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }

            string filePath = System.IO.Path.Combine(folderPath, "registroUsuarios.json");
            var usuarios = Program.registroUsuarios.Select(usuario => new 
            {
                Correo = usuario.correo,
                Entrada = usuario.entrada,
                Salida = usuario.salida
            }).ToList();

            string json = System.Text.Json.JsonSerializer.Serialize(usuarios, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });

            System.IO.File.WriteAllText(filePath, json);
            MessageDialog dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Registro de usuarios exportado exitosamente.");
            dialog.Run();
            dialog.Destroy();
        };

        Reporte.Clicked += (sender, e) =>
        {
            if(!Program.listaUsuarios.EstaVacia())
            {
                Program.listaUsuarios.Graficar();
            }
            if(!Program.listaVehiculos.EstaVacia())
            {
                Program.listaVehiculos.Graficar();
            }
            if(!Program.arbolRepuestos.EstaVacio())
            {
                Program.arbolRepuestos.Graficar();
            }
            if(!Program.arbolServicios.EstaVacio())
            {
                Program.arbolServicios.Graficar();
            }
            if(!Program.arbolFacturas.EstaVacio())
            {
                Program.arbolFacturas.Graficar();
            }
        };

        Add(contenedor);
        ShowAll();
    }

    public void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        LoginWindow login = new LoginWindow();
        login.ShowAll();
        Hide();
        a.RetVal = true;
    }
}