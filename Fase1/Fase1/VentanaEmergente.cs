using Gtk;

    class VentanaEmergente : Window
{
    public VentanaEmergente() : base("Ventana Emergente")
    {
        SetDefaultSize(450, 200);
        SetPosition(WindowPosition.Center);
        DeleteEvent += OnDeleteEvent;

        Fixed contenedor = new Fixed();

        Label etiquetaOpciones = new Label("Seleccione una opción");

        Button botonUsuario = new Button("Usuarios");
        Button botonVehiculo = new Button("Vehiculos");
        Button botonRepuesto = new Button("Repuestos");
        Button botonSevicio = new Button("Servicios");

        contenedor.Put(etiquetaOpciones, 80, 20);
        contenedor.Put(botonUsuario, 50, 100);
        contenedor.Put(botonVehiculo, 150, 100);
        contenedor.Put(botonRepuesto, 250, 100);
        contenedor.Put(botonSevicio, 350, 100);

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

        botonSevicio.Clicked += (sender, e) =>
        {
            ServicioIngresoWindow servicio = new ServicioIngresoWindow();
            servicio.ShowAll();
            this.Hide();
        };
        Add(contenedor);
        ShowAll();
    }

    private void OnDeleteEvent(object sender, DeleteEventArgs args)
    {
        args.RetVal = true;
        Hide();
    }

}