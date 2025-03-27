using System;
using Gtk;

class LoginWindow : Window
{
    public LoginWindow() : base("Inicio de Sesión")
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
        
        contenedor.Put(etiquetaTitulo, 80, 20);
        contenedor.Put(etiquetaUsuario, 30, 60);
        contenedor.Put(entradaUsuario, 125, 60);
        contenedor.Put(etiquetaContra, 30, 100);
        contenedor.Put(entradaContra, 125, 100);
        contenedor.Put(botonLogin, 100, 140);

        botonLogin.Clicked += (sender, e) =>
        {
            string usuario = entradaUsuario.Text;
            string contraseña = entradaContra.Text;
            
            if (usuario == "admin@usac.com" && contraseña == "admin123")
            {
                MenuAdminWindow carga = new MenuAdminWindow();
                carga.ShowAll();
                Hide();
                Program.idUsuarioActual = 0;
            }
            else if(Program.listaUsuarios.ValidarLogin(usuario, contraseña))
            {
                MenuUsuarioWindow carga = new MenuUsuarioWindow();
                carga.ShowAll();
                Hide();
                Program.idUsuarioActual = Program.listaUsuarios.ObtenerId(usuario);

            }
            else
            {
                MessageDialog mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Credenciales incorrectas");
                mensaje.Run();
                mensaje.Hide();
            }
        };
        

        Add(contenedor);
        ShowAll();
    }

    public void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        a.RetVal = true;
        Application.Quit();
    }
}