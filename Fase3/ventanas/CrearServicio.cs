using Gtk;

class CrearServicio : Window
{
    public CrearServicio() : base("Crear Servicio")
    {
        SetDefaultSize(350, 400);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { Destroy(); };

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
        Label etiquetaMetodoPago = new Label("Método de Pago:");
        Entry entradaMetodoPago = new Entry();
        Button botonCrear = new Button("Crear");

        // Ajuste de posiciones para mejor alineación y separación vertical
        if (etiquetaId.Parent != null)
        {
            ((Container)etiquetaId.Parent).Remove(etiquetaId);
        }
        contenedor.Put(etiquetaId, 30, 40);
        if (entradaId.Parent != null)
        {
            ((Container)entradaId.Parent).Remove(entradaId);
        }
        contenedor.Put(entradaId, 130, 40);

        if (etiquetaIdRepuesto.Parent != null)
        {
            ((Container)etiquetaIdRepuesto.Parent).Remove(etiquetaIdRepuesto);
        }
        contenedor.Put(etiquetaIdRepuesto, 30, 85);
        if (entradaIdRepuesto.Parent != null)
        {
            ((Container)entradaIdRepuesto.Parent).Remove(entradaIdRepuesto);
        }
        contenedor.Put(entradaIdRepuesto, 130, 85);

        if (etiquetaIdVehiculo.Parent != null)
        {
            ((Container)etiquetaIdVehiculo.Parent).Remove(etiquetaIdVehiculo);
        }
        contenedor.Put(etiquetaIdVehiculo, 30, 130);
        if (entradaIdVehiculo.Parent != null)
        {
            ((Container)entradaIdVehiculo.Parent).Remove(entradaIdVehiculo);
        }
        contenedor.Put(entradaIdVehiculo, 130, 130);

        if (etiquetaDetalles.Parent != null)
        {
            ((Container)etiquetaDetalles.Parent).Remove(etiquetaDetalles);
        }
        contenedor.Put(etiquetaDetalles, 30, 175);
        if (entradaDetalles.Parent != null)
        {
            ((Container)entradaDetalles.Parent).Remove(entradaDetalles);
        }
        contenedor.Put(entradaDetalles, 130, 175);

        if (etiquetaCosto.Parent != null)
        {
            ((Container)etiquetaCosto.Parent).Remove(etiquetaCosto);
        }
        contenedor.Put(etiquetaCosto, 30, 220);
        if (entradaCosto.Parent != null)
        {
            ((Container)entradaCosto.Parent).Remove(entradaCosto);
        }
        contenedor.Put(entradaCosto, 130, 220);

        if (etiquetaMetodoPago.Parent != null)
        {
            ((Container)etiquetaMetodoPago.Parent).Remove(etiquetaMetodoPago);
        }
        contenedor.Put(etiquetaMetodoPago, 30, 265);
        if (entradaMetodoPago.Parent != null)
        {
            ((Container)entradaMetodoPago.Parent).Remove(entradaMetodoPago);
        }
        contenedor.Put(entradaMetodoPago, 130, 265);

        if (botonCrear.Parent != null)
        {
            ((Container)botonCrear.Parent).Remove(botonCrear);
        }
        contenedor.Put(botonCrear, 110, 320);

        contenedor.SetSizeRequest(350, 400);
        etiquetaId.SetSizeRequest(90, 30);
        entradaId.SetSizeRequest(180, 30);
        etiquetaIdRepuesto.SetSizeRequest(90, 30);
        entradaIdRepuesto.SetSizeRequest(180, 30);
        etiquetaIdVehiculo.SetSizeRequest(90, 30);
        entradaIdVehiculo.SetSizeRequest(180, 30);
        etiquetaDetalles.SetSizeRequest(90, 30);
        entradaDetalles.SetSizeRequest(180, 30);
        etiquetaCosto.SetSizeRequest(90, 30);
        entradaCosto.SetSizeRequest(180, 30);
        etiquetaMetodoPago.SetSizeRequest(90, 30);
        entradaMetodoPago.SetSizeRequest(180, 30);
        botonCrear.SetSizeRequest(120, 35);

        botonCrear.Clicked += (sender, e) =>
        {
            // Validar que todos los campos estén llenos
            if (string.IsNullOrWhiteSpace(entradaId.Text) ||
                string.IsNullOrWhiteSpace(entradaIdRepuesto.Text) ||
                string.IsNullOrWhiteSpace(entradaIdVehiculo.Text) ||
                string.IsNullOrWhiteSpace(entradaDetalles.Text) ||
                string.IsNullOrWhiteSpace(entradaCosto.Text) ||
                string.IsNullOrWhiteSpace(entradaMetodoPago.Text))
            {
                MessageDialog dialogCampos = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Todos los campos son obligatorios.");
                dialogCampos.Run();
                dialogCampos.Destroy();
                return;
            }

            // Validar tipos de datos
            int id, idRepuesto, idVehiculo;
            float costo;
            if (!int.TryParse(entradaId.Text, out id))
            {
                MessageDialog dialogTipo = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El ID debe ser un número entero.");
                dialogTipo.Run();
                dialogTipo.Destroy();
                return;
            }
            if (!int.TryParse(entradaIdRepuesto.Text, out idRepuesto))
            {
                MessageDialog dialogTipo = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El ID Repuesto debe ser un número entero.");
                dialogTipo.Run();
                dialogTipo.Destroy();
                return;
            }
            if (!int.TryParse(entradaIdVehiculo.Text, out idVehiculo))
            {
                MessageDialog dialogTipo = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El ID Vehículo debe ser un número entero.");
                dialogTipo.Run();
                dialogTipo.Destroy();
                return;
            }
            if (!float.TryParse(entradaCosto.Text, out costo))
            {
                MessageDialog dialogTipo = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El costo debe ser un número válido.");
                dialogTipo.Run();
                dialogTipo.Destroy();
                return;
            }

            string detalles = entradaDetalles.Text;
            string metodoPago = entradaMetodoPago.Text;
            int idServicio = Program.servicios.Contar(Program.servicios.Raiz);

            if (Program.servicios.Buscar(id) != null)
            {
                MessageDialog dialogExistente = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Ya existe un servicio con ese ID.");
                dialogExistente.Run();
                dialogExistente.Destroy();
                return;
            }

            if (Program.repuestos.Buscar(Program.repuestos.Raiz, idRepuesto) != null)
            {
                if (Program.vehiculos.Buscar(idVehiculo) != null)
                {
                    Program.grafo.AgregarNodo(idRepuesto, idVehiculo);
                    Program.servicios.Agregar(id, idRepuesto, idVehiculo, detalles, costo, metodoPago);
                    var repuestoEncontrado = Program.repuestos.Buscar(Program.repuestos.Raiz, idRepuesto);
                    float total = costo;
                    if (repuestoEncontrado != null)
                    {
                        total += repuestoEncontrado.costo;
                    }
                    Program.merkle.AgregarFactura(new Factura
                    {
                        ID = id,
                        ID_Servicio = idServicio,
                        Total = total,
                        Fecha = DateTime.Now.ToString(),
                        MetodoPago = metodoPago
                    });
                }
                else
                {
                    MessageDialog dialog2 = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El vehiculo no existe");
                    dialog2.Run();
                    dialog2.Destroy();
                    return;
                }
            }
            else
            {
                MessageDialog dialog2 = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El repuesto no existe");
                dialog2.Run();
                dialog2.Destroy();
                return;
            }
            Program.servicios.Imprimir(Program.servicios.Raiz);
            Program.grafo.ImprimirLista();
            Program.merkle.Imprimir();
            MessageDialog dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Servicio creado con ID: " + id);
            dialog.Run();
            dialog.Destroy();
        };
        Add(contenedor);
        
    }
}