using System;
using Gtk;

class Login : Window
{
    public Login() : base("Inicio de Sesión")
    {
        SetDefaultSize(400, 200);
        SetPosition(WindowPosition.Center);
        DeleteEvent += OnDeleteEvent;

        Fixed contenedor = new Fixed();

        Label etiquetaTitulo = new Label("Ingrese sus credenciales");
        Label etiquetaUsuario = new Label("Usuario:");
        Entry entradaUsuario = new Entry();
        Label etiquetaContra = new Label("Contraseña:");
        Entry entradaContra = new Entry();
        entradaContra.Visibility = false; 
        Button botonLogin = new Button("Iniciar Sesión");
        
        if(etiquetaTitulo.Parent != null)
        {
            ((Container)etiquetaTitulo.Parent).Remove(etiquetaTitulo);
        }
        contenedor.Put(etiquetaTitulo, 150, 20);

        if(etiquetaUsuario.Parent != null)
        {
            ((Container)etiquetaUsuario.Parent).Remove(etiquetaUsuario);
        }
        contenedor.Put(etiquetaUsuario, 50, 70);

        if(entradaUsuario.Parent != null)
        {
            ((Container)entradaUsuario.Parent).Remove(entradaUsuario);
        }
        contenedor.Put(entradaUsuario, 150, 70);

        if(etiquetaContra.Parent != null)
        {
            ((Container)etiquetaContra.Parent).Remove(etiquetaContra);
        }
        contenedor.Put(etiquetaContra, 50, 120);

        if(entradaContra.Parent != null)
        {
            ((Container)entradaContra.Parent).Remove(entradaContra);
        }
        contenedor.Put(entradaContra, 150, 120);

        if(botonLogin.Parent != null)
        {
            ((Container)botonLogin.Parent).Remove(botonLogin);
        }
        contenedor.Put(botonLogin, 150, 170);

        contenedor.SetSizeRequest(400, 250);
        etiquetaTitulo.SetSizeRequest(200, 30);
        etiquetaUsuario.SetSizeRequest(80, 30);
        entradaUsuario.SetSizeRequest(200, 30);
        etiquetaContra.SetSizeRequest(80, 30);
        entradaContra.SetSizeRequest(200, 30);
        botonLogin.SetSizeRequest(100, 30);
        contenedor.SetSizeRequest(400, 250);


        botonLogin.Clicked += (sender, e) =>
        {
            string usuario = entradaUsuario.Text;
            string contraseña = entradaContra.Text;

            if (usuario == "admin@usac.com" && contraseña == "admin123")
            {
                Program._menuAdmin = new MenuAdmin();
                Program._menuAdmin.ShowAll();
                Hide();
            }
            else
            {
                MessageDialog dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Credenciales incorrectas");
                dialog.Run();
                dialog.Destroy();
            }

        };
        

        Add(contenedor);
        
    }

    public void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        a.RetVal = true;
        Application.Quit();
    }
}