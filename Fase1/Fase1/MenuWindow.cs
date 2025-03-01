using Gtk;

class MenuWindow : Window
{
    public MenuWindow() : base("Menú Principal")
    {
        SetDefaultSize(400, 600);
        SetPosition(WindowPosition.Center);
        DeleteEvent += (o, args) => Application.Quit();

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Menú Principal");
        Button botonCarga = new Button("Cargar Archivo");
        Button botonIngreso = new Button("Ingreso individual");
        Button botonGestion = new Button("Gestión de usuarios");
        Button botonGenerar = new Button("Generar servicio");
        Button botonCancelarFactura = new Button("Cancelar factura");
        Button Reporte = new Button("Reporte");

        contenedor.Put(etiquetaTitulo, 80, 20);
        contenedor.Put(botonCarga, 100, 100);
        contenedor.Put(botonIngreso, 100, 180);
        contenedor.Put(botonGestion, 100, 260);
        contenedor.Put(botonGenerar, 100, 340);
        contenedor.Put(botonCancelarFactura, 100, 420);
        contenedor.Put(Reporte, 100, 500);
        

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
            UsuarioGestionWindow gestion = new UsuarioGestionWindow();
            gestion.ShowAll();
        };

        botonGenerar.Clicked += (sender, e) =>
        {
            GenerarServicioWindow servicio = new GenerarServicioWindow();
            servicio.ShowAll();
        };

        botonCancelarFactura.Clicked += (sender, e) =>
        {
        
            GenerarFacturaWindow factura = new GenerarFacturaWindow();
            factura.ShowAll();
        };

        Reporte.Clicked += (sender, e) =>
        {
            try
            {
            if(Program.listaUsuarios.CabezaIsNotNull())
            {
                Program.listaUsuarios.Graficar();
            }
            if(Program.listaVehiculos.CabezaIsNotNull())
            {
                Program.listaVehiculos.Graficar();
            }
            if(Program.listaRepuestos.CabezaIsNotNull())
            {
                Program.listaRepuestos.Graficar();
            }
            if(Program.colaServicios.CabezaIsNotNull())
            {
                Program.colaServicios.Graficar();
            }
            if(Program.pilaFacturas.CabezaIsNotNull())
            {
                Program.pilaFacturas.Graficar();
            }
            if(!Program.bitacora.MatrizVacia())
            {
                Program.bitacora.Graficar();
            }
            }
            catch (Exception ex)
            {
            Console.WriteLine("An error occurred: " + ex.Message);
            }
        };


        Add(contenedor);
        ShowAll();
    }
}