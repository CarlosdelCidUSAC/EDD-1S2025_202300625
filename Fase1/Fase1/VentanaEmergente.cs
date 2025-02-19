using Gtk;

    class VentanaEmergente : Window
{
    public VentanaEmergente() : base("Ventana Emergente")
    {
        SetDefaultSize(400, 200);
        SetPosition(WindowPosition.Center);
        DeleteEvent += (o, args) => Application.Quit();

        Fixed contenedor = new Fixed();

        Label etiquetaOpciones = new Label("Seleccione una opción");

        Button botonUsuario = new Button("Usuarios");
        Button botonVehiculo = new Button("Vehiculos");
        Button botonRepuesto = new Button("Repuestos");

        contenedor.Put(etiquetaOpciones, 80, 20);
        contenedor.Put(botonUsuario, 50, 100);
        contenedor.Put(botonVehiculo, 150, 100);
        contenedor.Put(botonRepuesto, 250, 100);

        botonUsuario.Clicked += (sender, e) =>
        {
            UsuarioIngresoWindow usuario = new UsuarioIngresoWindow();
            usuario.ShowAll();
            this.Hide();

        };

        botonVehiculo.Clicked += (sender, e) =>
        {
            VehiculoIngresoWindow vehiculo = new VehiculoIngresoWindow();
            vehiculo.ShowAll();
            this.Hide();

        };

        botonRepuesto.Clicked += (sender, e) =>
        {
            RepuestoIngresoWindow repuesto = new RepuestoIngresoWindow();
            repuesto.ShowAll();
            this.Hide();
        }; 
        Add(contenedor);
        ShowAll();
    }

}