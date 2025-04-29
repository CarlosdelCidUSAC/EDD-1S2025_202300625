using Gtk;

class VisualizacionVehiculos : Window
{
    public VisualizacionVehiculos() : base("Visualizacion de Vehiculos")
    {
        SetDefaultSize(600, 400);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { Hide(); };

        Fixed contenedor = new Fixed();
        contenedor.SetSizeRequest(600, 400);

        TreeView tabla = new TreeView();

        TreeViewColumn columna1 = new TreeViewColumn{ Title = "ID" };
        TreeViewColumn columna2 = new TreeViewColumn{ Title = "Id-Usuario" }; 
        TreeViewColumn columna3 = new TreeViewColumn{ Title = "Marca" };  
        TreeViewColumn columna4 = new TreeViewColumn{ Title = "Modelo" };  
        TreeViewColumn columna5 = new TreeViewColumn{ Title = "Placa" };  

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

        modelo.AppendValues("1", "Toyota", "Corolla", "2020", "Rojo");
        modelo.AppendValues("2", "Honda", "Civic", "2019", "Azul"); 
        modelo.AppendValues("3","Sozuki","Vitara","2021","Verde");
        
        if(tabla.Parent != null)
        {
            ((Container)tabla.Parent).Remove(tabla);
        }
        contenedor.Put(tabla, 10, 50);
        tabla.SetSizeRequest(580, 300);


        Button botonMostrar = new Button("Mostrar Tabla");
        botonMostrar.SetSizeRequest(200, 30);
        if (botonMostrar.Parent != null)
        {
            ((Container)botonMostrar.Parent).Remove(botonMostrar);
        }
        contenedor.Put(botonMostrar, 200, 10);
        Add(contenedor);
        botonMostrar.Clicked += (sender, e) =>
        {
            modelo.Clear();
            modelo = Program.vehiculos.mostrarTabla();
            tabla.Model = modelo;
            tabla.ShowAll();
        };
        
    }

}