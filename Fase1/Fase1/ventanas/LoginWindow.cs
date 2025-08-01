using Gtk;

class LoginWindow : Window
{
    public LoginWindow() : base("Inicio de Sesión")
    {
        SetDefaultSize(400, 200);
        SetPosition(WindowPosition.Center);
        DeleteEvent += (o, args) => Application.Quit();

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
            
            if (usuario == "root@gmail.com" && contraseña == "root123")
            {
                MenuWindow carga = new MenuWindow();
                carga.ShowAll();
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

}
