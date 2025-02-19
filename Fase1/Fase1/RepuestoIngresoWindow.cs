using Gtk;

    class RepuestoIngresoWindow: Window
{
    public RepuestoIngresoWindow() : base("Ingreso de Repuesto")
    {

        SetDefaultSize(300, 500);
        SetPosition(WindowPosition.Center);
        DeleteEvent += (o, args) => Application.Quit();

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


            

            };

    Add(contenedor);
    ShowAll();
}
}