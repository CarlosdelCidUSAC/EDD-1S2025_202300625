using Gtk;

class CancelarFacturaWindow : Window
{
    public CancelarFacturaWindow(): base("Cancelar Facutrua")
    {
        SetDefaultSize(300, 500);
        SetPosition(WindowPosition.Center);
        DeleteEvent += OnDeleteEvent;

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Cancelar Facutrua");
        Label etiquetaId = new Label("Id:");
        Entry entradaId = new Entry();
        Label etiquetaOrden = new Label("Orden:");
        Label salidaOrden = new Label();
        Entry entradaOrden = new Entry();
        Label etiquetaTotal = new Label("Total:");
        Label salidaTotal = new Label();
        Entry entradaTotal = new Entry();


        

        Button botonBuscarId = new Button("Buscar por Id");
        Button botonPagar = new Button("Pagar");

        contenedor.Put(etiquetaTitulo, 80, 20);
        contenedor.Put(etiquetaOrden, 30, 60);
        contenedor.Put(entradaOrden, 125, 60);
        contenedor.Put(etiquetaTotal, 30, 100);
        contenedor.Put(entradaTotal, 125, 100);
        contenedor.Put(etiquetaId, 30, 180);
        contenedor.Put(entradaId, 125, 180);
        contenedor.Put(botonBuscarId, 100, 220);
        contenedor.Put(botonPagar, 100, 260);
        contenedor.Put(salidaOrden, 30, 300);
        contenedor.Put(salidaTotal, 30, 340);


        salidaOrden.Visible = true;
        salidaTotal.Visible = true;

        botonPagar.Clicked += (sender, e) =>
        {
            string Orden = entradaOrden.Text;
            string Total = entradaTotal.Text;
            string Idseña = entradaId.Text;

           
};
        botonBuscarId.Clicked += (sender, e) =>
        {
            int Id = int.Parse(entradaId.Text);

            if (Program.arbolFacturas.Buscar(Id) != null)
            {
                salidaOrden.Text = Program.arbolFacturas.Buscar(Id).IdOrden.ToString();
                salidaTotal.Text = Program.arbolFacturas.Buscar(Id).Total.ToString();
            }
            else
            {
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "No se encontro la factura");
                dialog.Run();
                dialog.Destroy();
            }
        };

        botonPagar.Clicked += (sender, e) =>
        {
            int Id = int.Parse(entradaId.Text);

            if (Program.arbolFacturas.Buscar(Id) != null)
            {
                Program.arbolFacturas.Eliminar(Id);
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Factura eliminada");
                dialog.Run();
                dialog.Destroy();
            }
            else
            {
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "No se encontro la factura");
                dialog.Run();
                dialog.Destroy();
            }
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