using Gtk;

    class UsuarioIngresoWindow: Window
{
    public UsuarioIngresoWindow() : base("Ingreso de usuario")
    {

        SetDefaultSize(300, 500);
        SetPosition(WindowPosition.Center);
        DeleteEvent += OnDeleteEvent;

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Ingreso de usuario");
        Label etiquetaId = new Label("ID:");
        Entry entradaId = new Entry();
        Label etiquetaNombre = new Label("Nombre:");
        Entry entradaNombre = new Entry();
        Label etiquetaApellido = new Label("Apellido:");
        Entry entradaApellido = new Entry();
        Label etiquetaCorreo = new Label("Correo:");
        Entry entradaCorreo = new Entry();
        Label etiquetaTelefono = new Label("Telefono:");
        Entry entradaTelefono = new Entry();

        Button botonIngresar = new Button("Ingresar");
        contenedor.Put(etiquetaTitulo, 100, 20);
        contenedor.Put(etiquetaId, 20, 60);
        contenedor.Put(entradaId, 100, 60);
        contenedor.Put(etiquetaNombre, 20, 100);
        contenedor.Put(entradaNombre, 100, 100);
        contenedor.Put(etiquetaApellido, 20, 140);
        contenedor.Put(entradaApellido, 100, 140);
        contenedor.Put(etiquetaCorreo, 20, 180);
        contenedor.Put(entradaCorreo, 100, 180);
        contenedor.Put(etiquetaTelefono, 20, 220);
        contenedor.Put(entradaTelefono, 100, 220);
        contenedor.Put(botonIngresar, 100, 260);

        botonIngresar.Clicked += (sender, e) => {
            string id = entradaId.Text;
            string nombre = entradaNombre.Text;
            string apellido = entradaApellido.Text;
            string correo = entradaCorreo.Text;
            string telefono = entradaTelefono.Text;


            if(id != "" && nombre != "" && apellido != "" && correo != "" && telefono != "")
            {   
                int idInt = int.Parse(id);
                int idTemp = Program.listaUsuarios.Buscar(idInt);
                
                if ( idTemp != idInt)
                {
                Program.listaUsuarios.Agregar(idInt, nombre, apellido, correo, telefono);
                Program.listaUsuarios.Imprimir();
                }
                else
                {
                    MessageDialog md = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "El usuario ya existe");
                    md.Run();
                    md.Destroy();
                }
            }
            else
            {
                MessageDialog md = new MessageDialog(null, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "Por favor llene todos los campos");
                md.Run();
                md.Destroy();



            };

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