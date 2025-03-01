using Gtk;

    class RepuestoIngresoWindow: Window
{
    public RepuestoIngresoWindow() : base("Ingreso de Repuesto")
    {

        SetDefaultSize(300, 500);
        SetPosition(WindowPosition.Center);
        DeleteEvent += OnDeleteEvent;

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Ingreso de Repuesto");
        Label etiquetaId = new Label("ID:");
        Entry entradaId = new Entry();
        Label etiquetaRepuesto = new Label("Repuesto:");
        Entry entradaRepuesto = new Entry();
        Label etiquetaDetalles = new Label("Detalles:");
        Entry entradaDetalles = new Entry();
        Label etiquetaCosto = new Label("Costo:");
        Entry entradaCosto = new Entry();


        Button botonIngresar = new Button("Ingresar");

        contenedor.Put(etiquetaTitulo, 10, 10);
        contenedor.Put(etiquetaId, 10, 40);
        contenedor.Put(entradaId, 100, 40);
        contenedor.Put(etiquetaRepuesto, 10, 70);
        contenedor.Put(entradaRepuesto, 100, 70);
        contenedor.Put(etiquetaDetalles, 10, 100);
        contenedor.Put(entradaDetalles, 100, 100);
        contenedor.Put(etiquetaCosto, 10, 130);
        contenedor.Put(entradaCosto, 100, 130);
        contenedor.Put(botonIngresar, 10, 160);
        

        botonIngresar.Clicked += (sender, e) => {
            string id = entradaId.Text;
            string Repuesto = entradaRepuesto.Text;
            string Detalles = entradaDetalles.Text;
            string Costo = entradaCosto.Text;

            if(id != "" && Repuesto != "" && Detalles != "" && Costo != "")
            {   
                int idInt = int.Parse(id);

                int idTemp = Program.listaRepuestos.Buscar(idInt);

                float CostoFloat = float.Parse(Costo);
                
                if(idTemp != idInt)
                {


                Program.listaRepuestos.Agregar(idInt, Repuesto, Detalles, CostoFloat);
                Program.listaRepuestos.Imprimir();
                }
                else
                {
                    MessageDialog md = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "El repuesto ya existe");
                    md.Run();
                    md.Destroy();
                }
            }
            else
            {
                MessageDialog md = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "Faltan datos");
                md.Run();
                md.Destroy();
            

            };
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