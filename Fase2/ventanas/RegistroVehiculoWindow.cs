using Gtk;

    class RegistroVehiculoWindow: Window
{
    public RegistroVehiculoWindow() : base("Ingreso de Vehiculo")
    {

        SetDefaultSize(300, 500);
        SetPosition(WindowPosition.Center);
        DeleteEvent += OnDeleteEvent;

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Ingreso de Vehiculo");
        Label etiquetaId = new Label("ID:");
        Entry entradaId = new Entry();
        Label etiquetaMarca = new Label("Marca:");
        Entry entradaMarca = new Entry();
        Label etiquetaModelo = new Label("Modelo:");
        Entry entradaModelo = new Entry();
        Label etiquetaPlaca = new Label("Placa:");
        Entry entradaPlaca = new Entry();

        Button botonIngresar = new Button("Ingresar");

        contenedor.Put(etiquetaTitulo, 10, 10);
        contenedor.Put(etiquetaId, 10, 40);
        contenedor.Put(entradaId, 100, 40);
        contenedor.Put(etiquetaMarca, 10, 100);
        contenedor.Put(entradaMarca, 100, 100);
        contenedor.Put(etiquetaModelo, 10, 130);
        contenedor.Put(entradaModelo, 100, 130);
        contenedor.Put(etiquetaPlaca, 10, 160);
        contenedor.Put(entradaPlaca, 100, 160);
        contenedor.Put(botonIngresar, 10, 190);
        

        botonIngresar.Clicked += (sender, e) => {
            string id = entradaId.Text;
            string Marca = entradaMarca.Text;
            string Modelo = entradaModelo.Text;
            string Placa = entradaPlaca.Text;
 
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