using Gtk;

class MenuAdminWindow : Window
{
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
        contenedor.Put(botonActRep,100, 260);
        contenedor.Put(botonVisRep,100, 340);
        contenedor.Put(botonGenerar, 100, 420);
        contenedor.Put(botonCtrlLog, 100, 500);
        contenedor.Put(Reporte, 100, 580);
        

        botonCarga.Clicked += (sender, e) =>
        {

        };

        botonGestion.Clicked += (sender, e) =>
        {

        };

        botonGenerar.Clicked += (sender, e) =>
        {

        };

        botonActRep.Clicked += (srender, e) =>
        {

        };

        botonCtrlLog.Clicked += (srender, e) =>
        {

        };

        botonVisRep.Clicked += (srender, e) =>
        {

        };

        Reporte.Clicked += (srender, e) =>
        {

        };

        Add(contenedor);
        ShowAll();
    }
}