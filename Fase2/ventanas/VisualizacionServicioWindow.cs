using Gtk;

class VisualizacionServicioWindow : Window
{
    public VisualizacionServicioWindow(): base("Visualizacion de Servicios")
    {

        SetDefaultSize(550,650);
        SetPosition(WindowPosition.Center);
        DeleteEvent += OnDeleteEvent;

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Visualizacion de Servicios");
        ComboBoxText comboBox = new ComboBoxText();
        comboBox.AppendText("Pre-Orden");
        comboBox.AppendText("In-Orden");
        comboBox.AppendText("Post-Orden");
        comboBox.Active = 0;

        TreeView tabla = new TreeView();

        TreeViewColumn columna1 = new TreeViewColumn { Title = "Columna 1" };
        TreeViewColumn columna2 = new TreeViewColumn { Title = "Columna 2" };
        TreeViewColumn columna3 = new TreeViewColumn { Title = "Columna 3" };
        TreeViewColumn columna4 = new TreeViewColumn { Title = "Columna 4" };
        TreeViewColumn columna5 = new TreeViewColumn { Title = "Columna 5" };

        tabla.AppendColumn(columna1);
        tabla.AppendColumn(columna2);
        tabla.AppendColumn(columna3);
        tabla.AppendColumn(columna4);
        tabla.AppendColumn(columna5);

        ListStore modelo = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
        tabla.Model = modelo;

        CellRendererText celda1 = new CellRendererText();
        CellRendererText celda2 = new CellRendererText();
        CellRendererText celda3 = new CellRendererText();
        CellRendererText celda4 = new CellRendererText();
        CellRendererText celda5 = new CellRendererText();

        columna1.PackStart(celda1, true);
        columna2.PackStart(celda2, true);
        columna3.PackStart(celda3, true);
        columna4.PackStart(celda4, true);
        columna5.PackStart(celda5, true);

        columna1.AddAttribute(celda1, "text", 0);
        columna2.AddAttribute(celda2, "text", 1);
        columna3.AddAttribute(celda3, "text", 2);
        columna4.AddAttribute(celda4, "text", 3);
        columna5.AddAttribute(celda5, "text", 4);

        modelo.AppendValues("Dato 1", "Dato 2", "Dato 3", "Dato 4", "Dato 5");
        modelo.AppendValues("Dato 6", "Dato 7", "Dato 8", "Dato 9", "Dato 10");
        modelo.AppendValues("Dato 11", "Dato 12", "Dato 13", "Dato 14", "Dato 15"); 

        contenedor.Put(tabla, 50, 130);
        contenedor.Put(etiquetaTitulo, 80, 20);
        contenedor.Put(comboBox, 80, 60);

        Button mostrarTabla = new Button("Mostrar tabla");
        contenedor.Put(mostrarTabla, 200, 60);

        mostrarTabla.Clicked += (sender, e) =>
        {
            modelo.Clear();
            if (comboBox.ActiveText == "Pre-Orden")
            {
                Program.arbolServicios.recorrerPreorden(Program.arbolServicios.Raiz, modelo);
                tabla.Model = modelo;
            }
            else if (comboBox.ActiveText == "In-Orden")
            {
                Program.arbolServicios.recorrerInorden(Program.arbolServicios.Raiz, modelo);
                tabla.Model = modelo;
            }
            else if (comboBox.ActiveText == "Post-Orden")
            {
                Program.arbolServicios.recorrerPostorden(Program.arbolServicios.Raiz, modelo);
                tabla.Model = modelo;
            }
        };

        Add(contenedor);
        ShowAll();

    }

    private void OnDeleteEvent(object sender, DeleteEventArgs args)
    {
        args.RetVal = true; 
        Hide(); 
    }
}