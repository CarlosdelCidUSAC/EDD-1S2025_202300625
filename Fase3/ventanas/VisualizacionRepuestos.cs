using Gtk;

class VisualizacionRepuestos : Window
{
    public VisualizacionRepuestos() : base("Visualizacion de Repuestos")
    {
        SetDefaultSize(400, 300);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { Destroy();};

        Fixed contenedor = new Fixed();

        ComboBoxText comboBox = new ComboBoxText();
        comboBox.AppendText("Pre-orden");
        comboBox.AppendText("In-orden");
        comboBox.AppendText("Post-orden");
        comboBox.Active = 0; 

        Button mostrarTabla = new Button("Mostrar Tabla");
        mostrarTabla.SetSizeRequest(200, 30);

        TreeView tabla = new TreeView();
        TreeViewColumn columna1 = new TreeViewColumn{
            Title = "ID"
        };
        TreeViewColumn columna2 = new TreeViewColumn{
            Title = "Repuesto"
        };
        TreeViewColumn columna3 = new TreeViewColumn{
            Title = "Detalles"
        };
        TreeViewColumn columna4 = new TreeViewColumn{
            Title = "Costo"
        };

        tabla.AppendColumn(columna1);
        tabla.AppendColumn(columna2);
        tabla.AppendColumn(columna3);
        tabla.AppendColumn(columna4);

        ListStore modelo = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string));
        
        tabla.Model = modelo;

        CellRendererText celda1 = new CellRendererText();
        CellRendererText celda2 = new CellRendererText();
        CellRendererText celda3 = new CellRendererText();
        CellRendererText celda4 = new CellRendererText();

        columna1.PackStart(celda1, true);
        columna1.AddAttribute(celda1, "text", 0);
        columna2.PackStart(celda2, true);
        columna2.AddAttribute(celda2, "text", 1);
        columna3.PackStart(celda3, true);
        columna3.AddAttribute(celda3, "text", 2);
        columna4.PackStart(celda4, true);
        columna4.AddAttribute(celda4, "text", 3);
        
        modelo.AppendValues("1", "Repuesto A", "Detalles A", "100");
        modelo.AppendValues("2", "Repuesto B", "Detalles B", "200");
        modelo.AppendValues("3", "Repuesto C", "Detalles C", "300");
        
        if (comboBox.Parent != null)
        {
            ((Container)comboBox.Parent).Remove(comboBox);
        }
        contenedor.Put(comboBox, 100, 30);
        comboBox.SetSizeRequest(200, 30);

        if (mostrarTabla.Parent != null)
        {
            ((Container)mostrarTabla.Parent).Remove(mostrarTabla);
        }
        contenedor.Put(mostrarTabla, 100, 70);
        mostrarTabla.SetSizeRequest(200, 30);

        if (tabla.Parent != null)
        {
            ((Container)tabla.Parent).Remove(tabla);
        }
        contenedor.Put(tabla, 20, 120);
        tabla.SetSizeRequest(360, 150);

        contenedor.SetSizeRequest(400, 300);
        Add(contenedor);

        mostrarTabla.Clicked += (sender, e) =>
        {
            try
            {
                string ordenSeleccionada = comboBox.ActiveText;
                modelo.Clear();
                if (ordenSeleccionada == "Pre-orden" && Program.repuestos.Raiz != null)
                {
                    Program.repuestos.recorrerPreorden(Program.repuestos.Raiz, modelo);
                }
                else if (ordenSeleccionada == "In-orden" && Program.repuestos.Raiz != null)
                {
                    Program.repuestos.recorrerInorden(Program.repuestos.Raiz, modelo);
                }
                else if (ordenSeleccionada == "Post-orden" && Program.repuestos.Raiz != null)
                {
                    Program.repuestos.recorrerPostorden(Program.repuestos.Raiz, modelo);
                }
                tabla.Model = modelo;
            }
            catch (Exception ex)
            {
                MessageDialog dialogo = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Error: " + ex.Message);
                dialogo.Run();
                dialogo.Destroy();
            }
        };

        
    }

}