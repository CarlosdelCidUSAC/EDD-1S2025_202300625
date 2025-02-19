using Gtk;

class MenuWindow : Window
{
    public MenuWindow() : base("Menú Principal")
    {
        SetDefaultSize(400, 500);
        SetPosition(WindowPosition.Center);
        DeleteEvent += (o, args) => Application.Quit();

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Menú Principal");
        Button botonCarga = new Button("Cargar Archivo");
        Button botonIngreso = new Button("Ingreso individual");
        Button botonGestion = new Button("Gestión de usuarios");
        Button botonGenerar = new Button("Generar servicio");
        Button botonCancelarFactura = new Button("Cancelar factura");

        contenedor.Put(etiquetaTitulo, 80, 20);
        contenedor.Put(botonCarga, 100, 100);
        contenedor.Put(botonIngreso, 100, 180);
        contenedor.Put(botonGestion, 100, 260);
        contenedor.Put(botonGenerar, 100, 340);
        contenedor.Put(botonCancelarFactura, 100, 420);
        

        botonCarga.Clicked += (sender, e) =>
        {
            CargaWindow carga = new CargaWindow();
            carga.ShowAll();
        };

        botonIngreso.Clicked += (sender, e) =>
        {
            VentanaEmergente ventana = new VentanaEmergente();
            ventana.ShowAll();
        };

        botonGestion.Clicked += (sender, e) =>
        {
           
        };

        botonGenerar.Clicked += (sender, e) =>
        {
          
        };

        botonCancelarFactura.Clicked += (sender, e) =>
        {
           
        };


        Add(contenedor);
        ShowAll();
    }
}