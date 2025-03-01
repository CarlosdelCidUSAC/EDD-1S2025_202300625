using System;
using Gtk;

class ServicioIngresoWindow : Window
{

    public ServicioIngresoWindow() : base("Ingreso de Servicio")
    {
        SetDefaultSize(400, 200);
        SetPosition(WindowPosition.Center);
        DeleteEvent += OnDeleteEvent;

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Ingrese los datos del servicio");

        Label etiquetaId = new Label("Id:");
        Entry entradaId = new Entry();

        Label etiquetaIdVehiculo = new Label("Id Vehiculo:");
        Entry entradaIdVehiculo = new Entry();

        Label etiquetaIdRepuesto = new Label("Id Repuesto:");
        Entry entradaIdRepuesto = new Entry();

        Label etiquetaDetalle = new Label("Detales:");
        Entry entradaDetalle = new Entry();

        Label etiquetaCosto = new Label("Costo:");
        Entry entradaCosto = new Entry();

        Button botonIngresar = new Button("Ingresar");

        contenedor.Put(etiquetaTitulo, 80, 20);

        contenedor.Put(etiquetaId, 20, 60);
        contenedor.Put(entradaId, 150, 60);

        contenedor.Put(etiquetaIdVehiculo, 20, 90);
        contenedor.Put(entradaIdVehiculo, 150, 90);

        contenedor.Put(etiquetaIdRepuesto, 20, 120);
        contenedor.Put(entradaIdRepuesto, 150, 120);

        contenedor.Put(etiquetaDetalle, 20, 150);
        contenedor.Put(entradaDetalle, 150, 150);

        contenedor.Put(etiquetaCosto, 20, 180);
        contenedor.Put(entradaCosto, 150, 180);

        contenedor.Put(botonIngresar, 100, 220);

        botonIngresar.Clicked += (sender, e) =>
        {
            string id = entradaId.Text;
            string idVehiculo = entradaIdVehiculo.Text;
            string idRepuesto = entradaIdRepuesto.Text;
            string detalle = entradaDetalle.Text;
            string costo = entradaCosto.Text;



            if (id != "" && idVehiculo != "" && idRepuesto != "" && detalle != "" && costo != "")
            {
                int idInt = int.Parse(id);
                int idVehiculoInt = int.Parse(idVehiculo);
                int idRepuestoInt = int.Parse(idRepuesto);

                int idTemp = Program.colaServicios.Buscar(idInt);

                float CostoFloat = float.Parse(costo);

                if (idTemp != idInt)
                {
                    Program.colaServicios.Encolar(idInt, idVehiculoInt, idRepuestoInt, detalle, CostoFloat);
                    Program.colaServicios.Imprimir();
                }
                else
                {
                    MessageDialog md = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "El servicio ya existe");
                    md.Run();
                    md.Destroy();
                }
            }
            else
            {
                MessageDialog md = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "Por favor llene todos los campos");
                md.Run();
                md.Destroy();
            }

        };

        Add(contenedor);
        ShowAll();
    }

    public void OnDeleteEvent(object sender, DeleteEventArgs args)
    {
        args.RetVal = true;
        Destroy();
    }

 

}
        
