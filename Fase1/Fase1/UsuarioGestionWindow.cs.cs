using Gtk;

class UsuarioGestionWindow : Window
{
    public UsuarioGestionWindow(): base("Gestión de Usuarios")
    {
        SetDefaultSize(300, 500);
        SetPosition(WindowPosition.Center);
        DeleteEvent += (o, args) => Application.Quit();

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

        

        botonActualizar.Clicked += (sender, e) =>
        {
            string nombre = entradaNombre.Text;
            string apellido = entradaApellido.Text;
            string correo = entradaCorreo.Text;
            string Idseña = entradaId.Text;


            if (nombre != "" && apellido != "" && correo != "" && Idseña != "" )
            {
                MessageDialog mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Usuario ingresado correctamente");
                mensaje.Run();
                mensaje.Hide();
            }
            else
            {
                MessageDialog mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Por favor llene todos los campos");
            }
};
        botonBuscarId.Clicked += (sender, e) =>
        {
            string id = entradaId.Text;
            if (id != "")
            {
                salidaNombre.Text = "Nombre: ";
                salidaApellido.Text = "Apellido: ";
                salidaCorreo.Text = "Correo: ";
            }
            else
            {
                MessageDialog mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Por favor llene el campo Id");
                mensaje.Run();
                mensaje.Hide();
            }
        };

        Add(contenedor);
        ShowAll();
    }
}