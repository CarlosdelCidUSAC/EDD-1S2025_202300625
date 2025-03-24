using Gtk;

class GestionEntidadesWindow : Window
{
    public GestionEntidadesWindow(): base("Gestión de Usuarios")
    {
        SetDefaultSize(300, 500);
        SetPosition(WindowPosition.Center);
        DeleteEvent += OnDeleteEvent;

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Gestión de Usuarios");
        Label etiquetaId = new Label("Id:");
        Entry entradaId = new Entry();
        Label etiqueta1 = new Label("");
        Label salida1 = new Label();
        Label etiqueta2 = new Label("");
        Label salida2 = new Label();
        Label etiqueta3 = new Label("");
        Label salida3 = new Label();
        Label etiqueta4 = new Label("");
        Label salida4 = new Label();

        

        Button botonBorrarUsuario = new Button("Borrar Usuario");
        Button botonBorrarVehiculo = new Button("Borrar Vehículo");
        Button botonBuscarUsuarioId = new Button("Buscar usuario por Id");
        Button botonBuscarVehiculoId = new Button("Buscar vehículo por Id");

        contenedor.Put(etiquetaTitulo, 80, 20);
        contenedor.Put(etiqueta1, 30, 60);
        contenedor.Put(salida1, 125, 60);
        contenedor.Put(etiqueta2, 30, 100);
        contenedor.Put(salida2, 125, 100);
        contenedor.Put(etiqueta3, 30, 140);
        contenedor.Put(salida3, 125, 140);
        contenedor.Put(etiqueta4, 30, 180);
        contenedor.Put(salida4, 125, 180);
        contenedor.Put(etiquetaId, 30, 220);
        contenedor.Put(entradaId, 125, 220);
        contenedor.Put(botonBuscarUsuarioId, 30, 280);
        contenedor.Put(botonBuscarVehiculoId, 30, 320);
        contenedor.Put(botonBorrarUsuario, 30, 380);
        contenedor.Put(botonBorrarVehiculo, 30, 420);


        botonBuscarUsuarioId.Clicked += (sender, args) =>
        {
            salida1.Text = "Usuario encontrado";
            salida2.Text = "Nombre: Juan";
            salida3.Text = "Apellido: Perez";
        };

        botonBuscarVehiculoId.Clicked += (sender, args) =>
        {
            salida1.Text = "Vehículo encontrado";
            salida2.Text = "Marca: Ford";
            salida3.Text = "Modelo: Fiesta";
        };

        botonBorrarUsuario.Clicked += (sender, args) =>
        {
            salida1.Text = "Usuario borrado";
            salida2.Text = "";
            salida3.Text = "";
        };

        botonBorrarVehiculo.Clicked += (sender, args) =>
        {
            salida1.Text = "Vehículo borrado";
            salida2.Text = "";
            salida3.Text = "";
        };

       

        Add(contenedor);
        ShowAll();
    }

    public void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        a.RetVal = true;
        Destroy();
    }
}