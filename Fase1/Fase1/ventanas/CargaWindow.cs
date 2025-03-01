using Gtk;
using System.IO;
using Newtonsoft.Json.Linq;


class CargaWindow : Window

{

    public CargaWindow() : base("Carga de Archivos")
    {
        SetDefaultSize(400, 200);
        SetPosition(WindowPosition.Center);
        DeleteEvent += OnDeleteEvent;

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
                carga_JSON_usuarios(dialogo.Filename);
                dialogo.Hide();
            }
            else if (comboBox.ActiveText == "Vehiculos"){
                FileChooserDialog dialogo = new FileChooserDialog("Seleccione un archivo", this, FileChooserAction.Open, "Cancelar", ResponseType.Cancel, "Abrir", ResponseType.Accept);
                dialogo.Run();
                carga_JSON_vehiculos(dialogo.Filename);
                dialogo.Hide();
            }
            else if (comboBox.ActiveText == "Repuestos"){
                FileChooserDialog dialogo = new FileChooserDialog("Seleccione un archivo", this, FileChooserAction.Open, "Cancelar", ResponseType.Cancel, "Abrir", ResponseType.Accept);
                dialogo.Run();
                cargar_JSON_repuestos(dialogo.Filename);
                dialogo.Hide();
            }

            


        };

        Add(contenedor);
        ShowAll();
    }

    private void OnDeleteEvent(object sender, DeleteEventArgs args)
    {
        args.RetVal = true; 
        Destroy(); 
    }

    private void carga_JSON_usuarios(string ruta){
        if(ruta == null){
            MessageDialog dialogo = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "No se ha seleccionado un archivo");
            dialogo.Run();
            dialogo.Hide();
            return;
        }
        string json = File.ReadAllText(ruta);
        JArray usuariosArray = JArray.Parse(json);

        foreach (JObject usuario in usuariosArray)
        {
            if (usuario["ID"] == null || usuario["Nombres"] == null || usuario["Apellidos"] == null || usuario["Correo"] == null || usuario["Contrasenia"] == null){
                MessageDialog dialogo = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El archivo no tiene el formato correcto");
                dialogo.Run();
                dialogo.Hide();
                return;
            }

           
        }
         for (int i = 0; i < usuariosArray.Count; i++)
            {
                JToken usuarioT = usuariosArray[i];
                string id = usuarioT["ID"].ToString();
                string nombres = usuarioT["Nombres"].ToString();
                string apellidos = usuarioT["Apellidos"].ToString();
                string correo = usuarioT["Correo"].ToString();
                string contrasenia = usuarioT["Contrasenia"].ToString();

                int idInt = int.Parse(id);
                Program.listaUsuarios.Agregar(idInt, nombres, apellidos, correo, contrasenia);


            }
            Program.listaUsuarios.Imprimir();


    }
    private void carga_JSON_vehiculos(string ruta)
    {
        if (ruta == null)
        {
            MessageDialog dialogo = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "No se ha seleccionado un archivo");
            dialogo.Run();
            dialogo.Hide();
            return;
        }
        string json = File.ReadAllText(ruta);
        JArray VehiculosArray = JArray.Parse(json);

        foreach (JObject vehiculo in VehiculosArray)
        {
            if (vehiculo["ID"]== null|| vehiculo["ID_Usuario"] == null || vehiculo["Marca"] == null || vehiculo["Modelo"] == null || vehiculo["Placa"]== null)
            {
                MessageDialog dialogo = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El archivo no tiene el formato correcto");
                dialogo.Run();
                dialogo.Hide();
                return;
            }
        }
        for (int i = 0; i < VehiculosArray.Count; i++)
            {
                JToken vehiculoT = VehiculosArray[i];
                string id = vehiculoT["ID"].ToString();
                string id_usuario = vehiculoT["ID_Usuario"].ToString();
                string marca = vehiculoT["Marca"].ToString();
                string modelo = vehiculoT["Modelo"].ToString();
                string placa = vehiculoT["Placa"].ToString();

                int idInt = int.Parse(id);
                int id_usuarioInt = int.Parse(id_usuario);
                int anioInt = int.Parse(modelo);
                Program.listaVehiculos.AgregarPrimero(idInt, id_usuarioInt, marca, anioInt, placa);
            }
           
        Program.listaVehiculos.Imprimir();


    }

    public void cargar_JSON_repuestos(string ruta)
    {
        if (ruta == null)
        {
            MessageDialog dialogo = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "No se ha seleccionado un archivo");
            dialogo.Run();
            dialogo.Hide();
            return;
        }
        string json = File.ReadAllText(ruta);
        JArray repuestosArray = JArray.Parse(json);

        foreach (JObject repuesto in repuestosArray)
        {
            if (repuesto["ID"] == null || repuesto["Repuesto"] == null || repuesto["Detalles"] == null || repuesto["Costo"] == null)
            {
                MessageDialog dialogo = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El archivo no tiene el formato correcto");
                dialogo.Run();
                dialogo.Hide();
                return;
            }
        }
        for (int i = 0; i < repuestosArray.Count; i++)
        {
            JToken repuestoT = repuestosArray[i];
            string id = repuestoT["ID"].ToString();
            string repuesto = repuestoT["Repuesto"].ToString();
            string detalle = repuestoT["Detalles"].ToString();
            string costo = repuestoT["Costo"].ToString();

            float costoFloat = float.Parse(costo);

            int idInt = int.Parse(id);
            Program.listaRepuestos.Agregar(idInt, repuesto, detalle, costoFloat);
        }
        Program.listaRepuestos.Imprimir();
    
    }
}

