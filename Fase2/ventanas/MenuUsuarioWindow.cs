using Gtk;

class MenuUsuarioWindow : Window
{
    public MenuUsuarioWindow() : base("Menú Administrador")
    {
        SetDefaultSize(400, 650);
        SetPosition(WindowPosition.Center);
        DeleteEvent += (o, args) => Application.Quit();

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

        };

        botonVisSer.Clicked += (sender, e) =>
        {

        };

        botonVisFac.Clicked += (sender, e) =>
        {
            
        };

        botonCanFac.Clicked += (sender, e) =>
        {
        
        };



        Add(contenedor);
        ShowAll();
    }
}