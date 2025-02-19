using Gtk;

class GenerarFacturaWindow: Window
{
    public GenerarFacturaWindow(): base("Generar Factura")
    {
    
        SetDefaultSize(400, 300);
        SetPosition(WindowPosition.Center);
        DeleteEvent += (o, args) => Application.Quit();

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


        
}
}
