using Gtk;

class CrearServicio : Window
{
    public CrearServicio() : base("Crear Servicio")
    {
        SetDefaultSize(350, 400);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { Hide(); };

        Fixed contenedor = new Fixed();
        contenedor.SetSizeRequest(350, 400);

        Label etiquetaId = new Label("ID:");
        Entry entradaId = new Entry();
        Label etiquetaIdRepuesto = new Label("ID Repuesto:");
        Entry entradaIdRepuesto = new Entry();
        Label etiquetaIdVehiculo = new Label("ID Vehículo:");
        Entry entradaIdVehiculo = new Entry();
        Label etiquetaDetalles = new Label("Detalles:");
        Entry entradaDetalles = new Entry();
        Label etiquetaCosto = new Label("Costo:");
        Entry entradaCosto = new Entry();
        Button botonCrear = new Button("Crear");

        if (etiquetaId.Parent != null)
        {
            ((Container)etiquetaId.Parent).Remove(etiquetaId);
        }
        contenedor.Put(etiquetaId, 30, 60);
        if (entradaId.Parent != null)
        {
            ((Container)entradaId.Parent).Remove(entradaId);
        }
        contenedor.Put(entradaId, 125, 60);
        if (etiquetaIdRepuesto.Parent != null)
        {
            ((Container)etiquetaIdRepuesto.Parent).Remove(etiquetaIdRepuesto);
        }
        contenedor.Put(etiquetaIdRepuesto, 30, 100);
        if (entradaIdRepuesto.Parent != null)
        {
            ((Container)entradaIdRepuesto.Parent).Remove(entradaIdRepuesto);
        }
        contenedor.Put(entradaIdRepuesto, 125, 100);
        if (etiquetaIdVehiculo.Parent != null)
        {
            ((Container)etiquetaIdVehiculo.Parent).Remove(etiquetaIdVehiculo);
        }
        contenedor.Put(etiquetaIdVehiculo, 30, 140);
        if (entradaIdVehiculo.Parent != null)
        {
            ((Container)entradaIdVehiculo.Parent).Remove(entradaIdVehiculo);
        }
        contenedor.Put(entradaIdVehiculo, 125, 140);
        if (etiquetaDetalles.Parent != null)
        {
            ((Container)etiquetaDetalles.Parent).Remove(etiquetaDetalles);
        }
        contenedor.Put(etiquetaDetalles, 30, 180);
        if (entradaDetalles.Parent != null)
        {
            ((Container)entradaDetalles.Parent).Remove(entradaDetalles);
        }
        contenedor.Put(entradaDetalles, 125, 180);
        if (etiquetaCosto.Parent != null)
        {
            ((Container)etiquetaCosto.Parent).Remove(etiquetaCosto);
        }
        contenedor.Put(etiquetaCosto, 30, 220);
        if (entradaCosto.Parent != null)
        {
            ((Container)entradaCosto.Parent).Remove(entradaCosto);
        }
        contenedor.Put(entradaCosto, 125, 220);
        if (botonCrear.Parent != null)
        {
            ((Container)botonCrear.Parent).Remove(botonCrear);
        }
        contenedor.Put(botonCrear, 100, 260);
        contenedor.SetSizeRequest(300, 400);
        etiquetaId.SetSizeRequest(80, 30);
        entradaId.SetSizeRequest(200, 30);
        etiquetaIdRepuesto.SetSizeRequest(80, 30);
        entradaIdRepuesto.SetSizeRequest(200, 30);
        etiquetaIdVehiculo.SetSizeRequest(80, 30);
        entradaIdVehiculo.SetSizeRequest(200, 30);
        etiquetaDetalles.SetSizeRequest(80, 30);
        entradaDetalles.SetSizeRequest(200, 30);
        etiquetaCosto.SetSizeRequest(80, 30);
        entradaCosto.SetSizeRequest(200, 30);
        botonCrear.SetSizeRequest(100, 30);
        contenedor.SetSizeRequest(300, 400);

        botonCrear.Clicked += (sender, e) =>
        {
            string id = entradaId.Text;
            string idRepuesto = entradaIdRepuesto.Text;
            string idVehiculo = entradaIdVehiculo.Text;
            string detalles = entradaDetalles.Text;
            string costo = entradaCosto.Text;

            // Aquí puedes agregar la lógica para crear el servicio
            MessageDialog dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Servicio creado con ID: " + id);
            dialog.Run();
            dialog.Destroy();
        };
        Add(contenedor);
        
    }
}