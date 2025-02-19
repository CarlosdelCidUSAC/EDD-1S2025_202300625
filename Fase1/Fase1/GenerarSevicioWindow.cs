using Gtk;

    class GenerarServicioWindow: Window
{
    public GenerarServicioWindow() : base("Crear Servicio")
    {

        SetDefaultSize(300, 500);
        SetPosition(WindowPosition.Center);
        DeleteEvent += (o, args) => Application.Quit();

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Ingreso de usuario");
        Label etiquetaId = new Label("ID:");
        Entry entradaId = new Entry();
        Label etiquetaId_Repuesto = new Label("Id_Repuesto:");
        Entry entradaId_Repuesto = new Entry();
        Label etiquetaId_Vehiculo = new Label("Id_Vehiculo:");
        Entry entradaId_Vehiculo = new Entry();
        Label etiquetaDetalles = new Label("Detalles:");
        Entry entradaDetalles = new Entry();
        Label etiquetaCosto = new Label("Costo:");
        Entry entradaCosto = new Entry();

        Button botonGuardar = new Button("Guardar");

        contenedor.Put(etiquetaTitulo, 10, 10);
        contenedor.Put(etiquetaId, 10, 40);
        contenedor.Put(entradaId, 100, 40);
        contenedor.Put(etiquetaId_Repuesto, 10, 70);
        contenedor.Put(entradaId_Repuesto, 100, 70);
        contenedor.Put(etiquetaId_Vehiculo, 10, 100);
        contenedor.Put(entradaId_Vehiculo, 100, 100);
        contenedor.Put(etiquetaDetalles, 10, 130);
        contenedor.Put(entradaDetalles, 100, 130);
        contenedor.Put(etiquetaCosto, 10, 160);
        contenedor.Put(entradaCosto, 100, 160);
        contenedor.Put(botonGuardar, 10, 190);


        botonGuardar.Clicked += (sender, e) => {
            string id = entradaId.Text;
            string Id_Repuesto = entradaId_Repuesto.Text;
            string Id_Vehiculo = entradaId_Vehiculo.Text;
            string Detalles = entradaDetalles.Text;
            string Costo = entradaCosto.Text;

            

            };
            Add(contenedor);
            ShowAll();
    
    
}
}