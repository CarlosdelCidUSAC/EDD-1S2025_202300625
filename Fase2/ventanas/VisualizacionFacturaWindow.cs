using Gtk;

class VisualizacionFacturasWindow : Window
{
    public VisualizacionFacturasWindow(): base("Visualizacion de Facturas")
    {

        SetDefaultSize(480,650);
        SetPosition(WindowPosition.Center);
        DeleteEvent += OnDeleteEvent;

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Visualizacion de Facturas");
        ComboBoxText comboBox = new ComboBoxText();
        comboBox.AppendText("Pre-Orden");
        comboBox.AppendText("In-Orden");
        comboBox.AppendText("Post-Orden");
        comboBox.Active = 0;

        TreeView tabla = new TreeView();

        TreeViewColumn columna1 = new TreeViewColumn { Title = "Columna 1" };
        TreeViewColumn columna2 = new TreeViewColumn { Title = "Columna 2" };
        TreeViewColumn columna3 = new TreeViewColumn { Title = "Columna 3" };

        tabla.AppendColumn(columna1);
        tabla.AppendColumn(columna2);
        tabla.AppendColumn(columna3);

        ListStore modelo = new ListStore(typeof(string), typeof(string), typeof(string));
        tabla.Model = modelo;

        CellRendererText celda1 = new CellRendererText();
        CellRendererText celda2 = new CellRendererText();
        CellRendererText celda3 = new CellRendererText();

        columna1.PackStart(celda1, true);
        columna2.PackStart(celda2, true);
        columna3.PackStart(celda3, true);

        columna1.AddAttribute(celda1, "text", 0);
        columna2.AddAttribute(celda2, "text", 1);
        columna3.AddAttribute(celda3, "text", 2);

        modelo.AppendValues("Dato 1", "Dato 2", "Dato 3");
        modelo.AppendValues("Dato 4", "Dato 5", "Dato 6");
        modelo.AppendValues("Dato 7", "Dato 8", "Dato 9");

        contenedor.Put(tabla, 50, 130);
        contenedor.Put(etiquetaTitulo, 80, 20);
        contenedor.Put(comboBox, 80, 60);

        Button mostrarTabla = new Button("Mostrar tabla");
        contenedor.Put(mostrarTabla, 200, 60);

        Add(contenedor);
        ShowAll();

    }

    private void OnDeleteEvent(object sender, DeleteEventArgs args)
    {
        args.RetVal = true; 
        Destroy(); 
    }
}