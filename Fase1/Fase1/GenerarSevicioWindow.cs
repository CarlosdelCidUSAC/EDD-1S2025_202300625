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
            float CostoFloat;
            if (!float.TryParse(Costo, out CostoFloat))
            {
                MessageDialog md = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "El costo ingresado no es un número válido");
                md.Run();
                md.Destroy();
                return;
            }

            int idInt = int.Parse(id);
            int Id_RepuestoInt = int.Parse(Id_Repuesto);
            int Id_VehiculoInt = int.Parse(Id_Vehiculo);

            int idTemp = Program.colaServicios.Buscar(idInt);
            int idRepuestoTemp = Program.listaRepuestos.Buscar(Id_RepuestoInt);
            int idVehiculoTemp = Program.listaVehiculos.Buscar(Id_VehiculoInt);

            if (idTemp != idInt)
            {
                if(idRepuestoTemp == Id_RepuestoInt)
                {
                    if(idVehiculoTemp == Id_VehiculoInt)
                    {
                        Program.colaServicios.Encolar(idInt, Id_RepuestoInt, Id_VehiculoInt, Detalles, CostoFloat);
                        entradaId.Text = "";
                        entradaId_Repuesto.Text = "";
                        entradaId_Vehiculo.Text = "";
                        entradaDetalles.Text = "";
                        entradaCosto.Text = "";
                        
                        float CostoServicio = Program.listaRepuestos.BuscarCosto(Id_RepuestoInt);

                        float CostoTotal = float.Parse(Costo) + CostoServicio;
                        
                        
                        Program.pilaFacturas.enpilar(idInt, Id_RepuestoInt, Id_VehiculoInt, Detalles, CostoTotal); 
                        Program.bitacora.insertar(Id_RepuestoInt, Id_VehiculoInt, Detalles);
                    }
                    else
                    {
                        MessageDialog md = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "El vehiculo no existe");
                        md.Run();
                        md.Destroy();
                    }
                }
                else
                {
                    MessageDialog md = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "El repuesto no existe");
                    md.Run();
                    md.Destroy();
                }
            }
            else
            {
                MessageDialog md = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "El servicio ya existe");
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
        Destroy();
    }
}