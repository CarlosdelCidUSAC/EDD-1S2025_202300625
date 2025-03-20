using Gtk;

class UsuarioGestionWindow : Window
{
    public UsuarioGestionWindow(): base("Gestión de Usuarios")
    {
        SetDefaultSize(300, 500);
        SetPosition(WindowPosition.Center);
        DeleteEvent += OnDeleteEvent;

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Gestión de Usuarios");
        Label etiquetaId = new Label("Id:");
        Entry entradaId = new Entry();
        Label etiquetaNombre = new Label("Nombre:");
        Label salidaNombre = new Label();
        Entry entradaNombre = new Entry();
        Label etiquetaApellido = new Label("Apellido:");
        Label salidaApellido = new Label();
        Entry entradaApellido = new Entry();
        Label etiquetaCorreo = new Label("Correo:");
        Label salidaCorreo = new Label();
        Entry entradaCorreo = new Entry();

        

        Button botonBorrar = new Button("Borrar");
        Button botonBuscarId = new Button("Buscar por Id");
        Button botonActualizar = new Button("Actualizar");

        contenedor.Put(etiquetaTitulo, 80, 20);
        contenedor.Put(etiquetaNombre, 30, 60);
        contenedor.Put(entradaNombre, 125, 60);
        contenedor.Put(etiquetaApellido, 30, 100);
        contenedor.Put(entradaApellido, 125, 100);
        contenedor.Put(etiquetaCorreo, 30, 140);
        contenedor.Put(entradaCorreo, 125, 140);
        contenedor.Put(etiquetaId, 30, 180);
        contenedor.Put(entradaId, 125, 180);
        contenedor.Put(botonBuscarId, 100, 220);
        contenedor.Put(botonActualizar, 100, 260);
        contenedor.Put(salidaNombre, 30, 300);
        contenedor.Put(salidaApellido, 30, 340);
        contenedor.Put(salidaCorreo, 30, 380);
        contenedor.Put(botonBorrar, 100, 420);


        salidaNombre.Visible = true;
        salidaApellido.Visible = true;
        salidaCorreo.Visible = true;

        botonBorrar.Clicked += (sender, e) =>
        {
            
        };

        botonActualizar.Clicked += (sender, e) =>
        {
            string nombre = entradaNombre.Text;
            string apellido = entradaApellido.Text;
            string correo = entradaCorreo.Text;
            string Idseña = entradaId.Text;

           // Program.listaUsuarios.Actualizar(int.Parse(Idseña), nombre, apellido, correo);
            MessageDialog mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Usuario actualizado");
            mensaje.Run();
            mensaje.Hide();

            salidaApellido.Visible = true;
            salidaCorreo.Visible = true;
            salidaNombre.Visible = true;
};
        botonBuscarId.Clicked += (sender, e) =>
        {
            
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