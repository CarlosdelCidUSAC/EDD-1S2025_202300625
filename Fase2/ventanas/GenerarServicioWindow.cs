using Gtk;

    class GenerarServicioWindow: Window
{
    public GenerarServicioWindow() : base("Crear Servicio")
    {

        SetDefaultSize(300, 500);
        SetPosition(WindowPosition.Center);
        DeleteEvent += OnDeleteEvent;

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

            if (id == "" || Id_Repuesto == "" || Id_Vehiculo == "" || Detalles == "" || Costo == "")
            {
            MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "No se permiten campos vacíos");
            md.Run();
            md.Destroy();
            return;
            }

            if (!int.TryParse(id, out _) || !int.TryParse(Id_Repuesto, out _) || !int.TryParse(Id_Vehiculo, out _))
            {
            MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "ID, Id_Repuesto e Id_Vehiculo deben ser números enteros");
            md.Run();
            md.Destroy();
            return;
            }

            if (!float.TryParse(Costo, out _))
            {
            MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Costo debe ser un número válido");
            md.Run();
            md.Destroy();
            return;
            }

            try
            {
            Program.arbolServicios.insertar(int.Parse(id), int.Parse(Id_Repuesto), int.Parse(Id_Vehiculo), Detalles, float.Parse(Costo));
            Program.arbolFacturas.Insertar(int.Parse(id), float.Parse(Costo) + Program.arbolRepuestos.Buscar(int.Parse(Id_Repuesto)).Costo);
            MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Servicio guardado");
            md.Run();
            md.Destroy();
            entradaId.Text = "";
            entradaId_Repuesto.Text = "";
            entradaId_Vehiculo.Text = "";
            entradaDetalles.Text = "";
            entradaCosto.Text = ""; 
            }
            catch (Exception ex)
            {
            MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, $"Error al guardar el servicio: {ex.Message}");
            md.Run();
            md.Destroy();
            }
        };

            Add(contenedor);
            ShowAll();
    
    
}
    public void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        a.RetVal = true;
        Hide();
    }
}