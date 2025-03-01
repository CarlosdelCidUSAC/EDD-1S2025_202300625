using Gtk;

class GenerarFacturaWindow : Window
{
    public GenerarFacturaWindow() : base("Generar Factura")
    {

        SetDefaultSize(400, 300);
        SetPosition(WindowPosition.Center);
        DeleteEvent += OnDeleteEvent;

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Generar Factura");

        Label etiquetaId = new Label("ID:");
        Label salidaId = new Label();
        Label etiquetaId_Orden = new Label("Id_Orden:");
        Label salidaId_Orden = new Label();
        Label etiquetaTotal = new Label("Total:");
        Label salidaTotal = new Label();

        contenedor.Put(etiquetaTitulo, 100, 30);
        contenedor.Put(etiquetaId, 30, 70);
        contenedor.Put(salidaId, 150, 70);
        contenedor.Put(etiquetaId_Orden, 30, 120);
        contenedor.Put(salidaId_Orden, 150, 120);
        contenedor.Put(etiquetaTotal, 30, 170);
        contenedor.Put(salidaTotal, 150, 170);

        Add(contenedor);
        ShowAll();

        if (Program.pilaFacturas.CabezaIsNotNull())
        {
            int ID = Program.pilaFacturas.ObtenerID();
            salidaId.Text = ID.ToString();
            float total = Program.pilaFacturas.ObtenerCosto();
            salidaTotal.Text = total.ToString();
            int idOrden = Program.pilaFacturas.ObtenerIDOrden();
            salidaId_Orden.Text = idOrden.ToString();
            unsafe
            {
                Program.pilaFacturas.desenpilar();
            }

        }
        else
        {
            MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "No hay facturas generadas.");
            dialog.Run();
            dialog.Destroy();
            Hide();
        }



    }
    public void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        a.RetVal = true;
        Destroy();
    }

}
