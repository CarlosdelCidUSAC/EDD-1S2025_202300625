using Gtk;

class ActualizarRespuestosWindow : Window
{
    public ActualizarRespuestosWindow(): base("Actualizar Repuestos")
    {
        SetDefaultSize(300, 500);
        SetPosition(WindowPosition.Center);
        DeleteEvent += OnDeleteEvent;

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Actualizar Repuestos");
        Label etiquetaId = new Label("Id:");
        Entry entradaId = new Entry();
        Label etiquetaRepuesto = new Label("Repuesto:");
        Label salidaRepuesto = new Label();
        Entry entradaRepuesto = new Entry();
        Label etiquetaDetalles = new Label("Detalles:");
        Label salidaDetalles = new Label();
        Entry entradaDetalles = new Entry();
        Label etiquetaCosto = new Label("Costo:");
        Label salidaCosto = new Label();
        Entry entradaCosto = new Entry();

        

        Button botonBuscarId = new Button("Buscar por Id");
        Button botonActualizar = new Button("Actualizar");

        contenedor.Put(etiquetaTitulo, 80, 20);
        contenedor.Put(etiquetaRepuesto, 30, 60);
        contenedor.Put(entradaRepuesto, 125, 60);
        contenedor.Put(etiquetaDetalles, 30, 100);
        contenedor.Put(entradaDetalles, 125, 100);
        contenedor.Put(etiquetaCosto, 30, 140);
        contenedor.Put(entradaCosto, 125, 140);
        contenedor.Put(etiquetaId, 30, 180);
        contenedor.Put(entradaId, 125, 180);
        contenedor.Put(botonBuscarId, 100, 220);
        contenedor.Put(botonActualizar, 100, 260);
        contenedor.Put(salidaRepuesto, 30, 300);
        contenedor.Put(salidaDetalles, 30, 340);
        contenedor.Put(salidaCosto, 30, 380);


        salidaRepuesto.Visible = true;
        salidaDetalles.Visible = true;
        salidaCosto.Visible = true;

        botonActualizar.Clicked += (sender, e) =>
        {
            string Repuesto = entradaRepuesto.Text;
            string Detalles = entradaDetalles.Text;
            string Costo = entradaCosto.Text;
            string Idseña = entradaId.Text;

           
};
        botonBuscarId.Clicked += (sender, e) =>
        {

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