using Gtk;
using System.IO;
using Newtonsoft.Json.Linq;
class CargaMasiva : Window
{
    public CargaMasiva() : base("Carga Masiva")
    {
        SetDefaultSize(400, 350);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { Destroy(); };

        Fixed contenedor = new Fixed();
        contenedor.SetSizeRequest(400, 350);
        
        Label usuariosLabel = new Label("Usuarios:");
        usuariosLabel.SetSizeRequest(100, 30);
        Button usuariosBoton = new Button("Cargar");
        usuariosBoton.SetSizeRequest(200, 30);
        Label vehiculoLabel = new Label("Vehículos:");
        vehiculoLabel.SetSizeRequest(100, 30);
        Button vehiculosBoton = new Button("Cargar");
        vehiculosBoton.SetSizeRequest(200, 30);
        Label repuestosLabel = new Label("Repuestos:");
        repuestosLabel.SetSizeRequest(100, 30);
        Button repuestosBoton = new Button("Cargar");
        repuestosBoton.SetSizeRequest(200, 30);
        Label serviciosLabel = new Label("Servicios:");
        serviciosLabel.SetSizeRequest(100, 30);
        Button serviciosBoton = new Button("Cargar");

      
        if (usuariosLabel.Parent != null)
            ((Container)usuariosLabel.Parent).Remove(usuariosLabel);
        contenedor.Put(usuariosLabel, 20, 60);
        if (usuariosBoton.Parent != null)
            ((Container)usuariosBoton.Parent).Remove(usuariosBoton);
        contenedor.Put(usuariosBoton, 120, 60);
        if (vehiculoLabel.Parent != null)
            ((Container)vehiculoLabel.Parent).Remove(vehiculoLabel);
        contenedor.Put(vehiculoLabel, 20, 130);
        if (vehiculosBoton.Parent != null)
            ((Container)vehiculosBoton.Parent).Remove(vehiculosBoton);
        contenedor.Put(vehiculosBoton, 120, 130);
        if (repuestosLabel.Parent != null)
            ((Container)repuestosLabel.Parent).Remove(repuestosLabel);
        contenedor.Put(repuestosLabel, 20, 200);
        if (repuestosBoton.Parent != null)
            ((Container)repuestosBoton.Parent).Remove(repuestosBoton);
        contenedor.Put(repuestosBoton, 120, 200);
        if (serviciosLabel.Parent != null)
            ((Container)serviciosLabel.Parent).Remove(serviciosLabel);
        contenedor.Put(serviciosLabel, 20, 270);
        if (serviciosBoton.Parent != null)
            ((Container)serviciosBoton.Parent).Remove(serviciosBoton);
        contenedor.Put(serviciosBoton, 120, 270);
        
        Add(contenedor);

        usuariosBoton.Clicked += (sender, e) =>
        {
            FileChooserDialog dialogo = new FileChooserDialog("Seleccione un archivo", this, FileChooserAction.Open, "Cancelar", ResponseType.Cancel, "Abrir", ResponseType.Accept);
            dialogo.Run();
            carga_JSON_usuarios(dialogo.Filename);
            dialogo.Destroy();
        };
        vehiculosBoton.Clicked += (sender, e) =>
        {
            FileChooserDialog dialogo = new FileChooserDialog("Seleccione un archivo", this, FileChooserAction.Open, "Cancelar", ResponseType.Cancel, "Abrir", ResponseType.Accept);
            dialogo.Run();
            carga_JSON_vehiculos(dialogo.Filename);
            dialogo.Destroy();
        };
        repuestosBoton.Clicked += (sender, e) =>
        {
            FileChooserDialog dialogo = new FileChooserDialog("Seleccione un archivo", this, FileChooserAction.Open, "Cancelar", ResponseType.Cancel, "Abrir", ResponseType.Accept);
            dialogo.Run();
            cargar_JSON_repuestos(dialogo.Filename);
            dialogo.Destroy();
        };
        serviciosBoton.Clicked += (sender, e) =>
        {
            FileChooserDialog dialogo = new FileChooserDialog("Seleccione un archivo", this, FileChooserAction.Open, "Cancelar", ResponseType.Cancel, "Abrir", ResponseType.Accept);
            dialogo.Run();
            cargar_JSON_servicios(dialogo.Filename);
            dialogo.Destroy();
        };
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
            if (usuario["ID"] == null || usuario["Nombres"] == null || usuario["Apellidos"] == null || usuario["Correo"] == null || usuario["Contrasenia"] == null || usuario["Edad"] == null){
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
                string edad = usuarioT["Edad"].ToString();
                string contrasenia = usuarioT["Contrasenia"].ToString();

                Program.usuarios.AgregarBloque(new Usuario{
                    ID = id,
                    Nombres = nombres,
                    Apellidos = apellidos,
                    Correo = correo,
                    Edad = int.Parse(edad),
                    Contrasena = contrasenia
                });

            }

        Program.usuarios.MostrarCadena();


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

                Program.vehiculos.AgregarPrimero(idInt, id_usuarioInt, marca, anioInt, placa);
            }
           
        Program.vehiculos.Imprimir();

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

            Program.repuestos.insertar(int.Parse(id), repuesto, 0, detalle, float.Parse(costo));
        }
        Program.repuestos.Imprimir();
    
    }

    private void cargar_JSON_servicios(string ruta)
    {
        if (ruta == null)
        {
            MessageDialog dialogo = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "No se ha seleccionado un archivo");
            dialogo.Run();
            dialogo.Hide();
            return;
        }
        string json = File.ReadAllText(ruta);
        JArray serviciosArray = JArray.Parse(json);

        foreach (JObject servicio in serviciosArray)
        {
            if (servicio["Id"] == null || servicio["Id_repuesto"] == null || servicio["Id_vehiculo"] == null || servicio["Detalles"] == null || servicio["Costo"] == null || servicio["MetodoPago"] == null)
            {
                MessageDialog dialogo = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El archivo no tiene el formato correcto");
                dialogo.Run();
                dialogo.Hide();
                return;
            }
        }

            for (int i = 0; i < serviciosArray.Count; i++)
            {
                JToken servicioT = serviciosArray[i];
                string id = servicioT["Id"].ToString();
                string idRepuesto = servicioT["Id_repuesto"].ToString();
                string idVehiculo = servicioT["Id_vehiculo"].ToString();
                string detalles = servicioT["Detalles"].ToString();
                string costo = servicioT["Costo"].ToString();
                string metodoPago = servicioT["MetodoPago"].ToString();

                Program.servicios.Agregar(int.Parse(id), int.Parse(idRepuesto), int.Parse(idVehiculo), detalles, float.Parse(costo), metodoPago);
            }
            Program.servicios.Imprimir(Program.servicios.Raiz);
        
    }
}