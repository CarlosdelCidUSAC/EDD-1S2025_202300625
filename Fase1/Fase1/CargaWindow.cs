using Gtk;

class CargaWindow : Window
{
    public CargaWindow() : base("Carga de Archivos")
    {
        SetDefaultSize(400, 200);
        SetPosition(WindowPosition.Center);
        DeleteEvent += (o, args) => Application.Quit();

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Cargue un archivo");
        Button botonCargar = new Button("Cargar");

        ComboBoxText comboBox = new ComboBoxText();
        comboBox.AppendText("Usuarios");
        comboBox.AppendText("Vehiculos");
        comboBox.AppendText("Repuestos");
        comboBox.Active = 0; 
        contenedor.Put(comboBox, 80, 60);


        contenedor.Put(etiquetaTitulo, 80, 20);
        contenedor.Put(botonCargar, 100, 140);

        botonCargar.Clicked += (sender, e) =>
        {
            if (comboBox.ActiveText == "Usuarios"){
                FileChooserDialog dialogo = new FileChooserDialog("Seleccione un archivo", this, FileChooserAction.Open, "Cancelar", ResponseType.Cancel, "Abrir", ResponseType.Accept);
                dialogo.Run();
                dialogo.Hide();
            }
            else if (comboBox.ActiveText == "Vehiculos"){
                FileChooserDialog dialogo = new FileChooserDialog("Seleccione un archivo", this, FileChooserAction.Open, "Cancelar", ResponseType.Cancel, "Abrir", ResponseType.Accept);
                dialogo.Run();
                dialogo.Hide();
            }
            else if (comboBox.ActiveText == "Repuestos"){
                FileChooserDialog dialogo = new FileChooserDialog("Seleccione un archivo", this, FileChooserAction.Open, "Cancelar", ResponseType.Cancel, "Abrir", ResponseType.Accept);
                dialogo.Run();
                dialogo.Hide();
            }


        };

        Add(contenedor);
        ShowAll();
    }

}