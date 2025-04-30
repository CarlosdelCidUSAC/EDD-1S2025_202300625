using Gtk;

class VisualizacionFacturas : Window
{
    public VisualizacionFacturas() : base("Visualizacion de Facturas")
    {
        SetDefaultSize(300, 200);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { Hide(); };

        Fixed contenedor = new Fixed();

        TreeView tabla = new TreeView();

        TreeViewColumn columna1 = new TreeViewColumn { Title = "ID" };
        TreeViewColumn columna2 = new TreeViewColumn { Title = "Orden" };
        TreeViewColumn columna3 = new TreeViewColumn { Title = "Costo" };

        tabla.AppendColumn(columna1);
        tabla.AppendColumn(columna2);
        tabla.AppendColumn(columna3);

        ListStore modelo = new ListStore(typeof(string), typeof(string), typeof(string));
        tabla.Model = modelo;

        CellRendererText celda1 = new CellRendererText();
        CellRendererText celda2 = new CellRendererText();
        CellRendererText celda3 = new CellRendererText();

        columna1.PackStart(celda1, true);
        columna1.AddAttribute(celda1, "text", 0);
        columna2.PackStart(celda2, true);
        columna2.AddAttribute(celda2, "text", 1);
        columna3.PackStart(celda3, true);
        columna3.AddAttribute(celda3, "text", 2);

        if (Program.merkle != null && Program.merkle.Root != null)
        {
            modelo.Clear();
            modelo = Program.merkle.MostrarTabla();
            tabla.Model = modelo;
        }
        else
        {
            modelo.AppendValues("No hay facturas disponibles", "", "");
        }

        if (tabla.Parent != null)
        {
            ((Container)tabla.Parent).Remove(tabla);
        }
        
        contenedor.Put(tabla, 10, 50);
        tabla.SetSizeRequest(280, 140);


        Add(contenedor);
        
    }
}