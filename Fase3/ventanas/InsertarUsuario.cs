using Gtk;

class InsertarUsuario : Window
{
    public InsertarUsuario() : base("Insertar usuario")
    {
        SetDefaultSize(300, 400);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { Hide(); };

        Fixed contenedor = new Fixed();
        contenedor.SetSizeRequest(300, 400);

        Label etiquetaId = new Label("ID:");
        Entry entradaId = new Entry();
        Label etiquetaNombre = new Label("Nombre:");
        Entry entradaNombre = new Entry();
        Label etiquetaApellido = new Label("Apellido:");
        Entry entradaApellido = new Entry();
        Label eriquetaCorreo = new Label("Correo:");
        Entry entradaCorreo = new Entry();
        Label etiquetaEdad = new Label("Edad:");
        Entry entradaEdad = new Entry();
        Label etiquetaContrasenia = new Label("Contraseña:");
        Entry entradaContrasenia = new Entry();
        Button botonInsertar = new Button("Insertar");
        Button botonBuscar = new Button("Buscar");
        
        if (etiquetaId.Parent != null)
        {
            ((Container)etiquetaId.Parent).Remove(etiquetaId);
        }
        contenedor.Put(etiquetaId, 30, 60);
        if (entradaId.Parent != null)
        {
            ((Container)entradaId.Parent).Remove(entradaId);
        }
        contenedor.Put(entradaId, 125, 60);
        if (etiquetaNombre.Parent != null)
        {
            ((Container)etiquetaNombre.Parent).Remove(etiquetaNombre);
        }
        contenedor.Put(etiquetaNombre, 30, 100);
        if (entradaNombre.Parent != null)
        {
            ((Container)entradaNombre.Parent).Remove(entradaNombre);
        }
        contenedor.Put(entradaNombre, 125, 100);
        if (etiquetaApellido.Parent != null)
        {
            ((Container)etiquetaApellido.Parent).Remove(etiquetaApellido);
        }
        contenedor.Put(etiquetaApellido, 30, 140);
        if (entradaApellido.Parent != null)
        {
            ((Container)entradaApellido.Parent).Remove(entradaApellido);
        }
        contenedor.Put(entradaApellido, 125, 140);
        if (eriquetaCorreo.Parent != null)
        {
            ((Container)eriquetaCorreo.Parent).Remove(eriquetaCorreo);
        }
        contenedor.Put(eriquetaCorreo, 30, 180);
        if (entradaCorreo.Parent != null)
        {
            ((Container)entradaCorreo.Parent).Remove(entradaCorreo);
        }
        contenedor.Put(entradaCorreo, 125, 180);
        if (etiquetaEdad.Parent != null)
        {
            ((Container)etiquetaEdad.Parent).Remove(etiquetaEdad);
        }
        contenedor.Put(etiquetaEdad, 30, 220);
        if (entradaEdad.Parent != null)
        {
            ((Container)entradaEdad.Parent).Remove(entradaEdad);
        }
        contenedor.Put(entradaEdad, 125, 220);
        if (etiquetaContrasenia.Parent != null)
        {
            ((Container)etiquetaContrasenia.Parent).Remove(etiquetaContrasenia);
        }
        contenedor.Put(etiquetaContrasenia, 30, 260);
        if (entradaContrasenia.Parent != null)
        {
            ((Container)entradaContrasenia.Parent).Remove(entradaContrasenia);
        }
        contenedor.Put(entradaContrasenia, 125, 260);
        if (botonInsertar.Parent != null)
        {
            ((Container)botonInsertar.Parent).Remove(botonInsertar);
        }
        contenedor.Put(botonInsertar, 40, 320);
        if (botonBuscar.Parent != null)
        {
            ((Container)botonBuscar.Parent).Remove(botonBuscar);
        }
        contenedor.Put(botonBuscar, 160, 320);
     
        etiquetaId.SetSizeRequest(100, 30);
        entradaId.SetSizeRequest(150, 30);
        etiquetaNombre.SetSizeRequest(100, 30);
        entradaNombre.SetSizeRequest(150, 30);
        etiquetaApellido.SetSizeRequest(100, 30);
        entradaApellido.SetSizeRequest(150, 30);
        eriquetaCorreo.SetSizeRequest(100, 30);
        entradaCorreo.SetSizeRequest(150, 30);
        etiquetaEdad.SetSizeRequest(100, 30);
        entradaEdad.SetSizeRequest(150, 30);
        etiquetaContrasenia.SetSizeRequest(100, 30);
        entradaContrasenia.SetSizeRequest(150, 30);
        botonInsertar.SetSizeRequest(100, 40);
        botonBuscar.SetSizeRequest(100, 40);

        botonBuscar.Clicked += (sender, e) =>
        {
            string id = entradaId.Text;
            // Aquí puedes agregar la lógica para buscar el usuario en la base de datos
            MessageDialog dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Usuario encontrado");
            dialog.Run();
            dialog.Destroy();
        };

        botonInsertar.Clicked += (sender, e) =>
        {
            string id = entradaId.Text;
            string nombre = entradaNombre.Text;
            string apellido = entradaApellido.Text;
            string correo = entradaCorreo.Text;
            string edad = entradaEdad.Text;
            string contrasenia = entradaContrasenia.Text;

            // Aquí puedes agregar la lógica para insertar el usuario en la base de datos
            MessageDialog dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Usuario insertado correctamente");
            dialog.Run();
            dialog.Destroy();
        };

        Add(contenedor);
        
    }
}