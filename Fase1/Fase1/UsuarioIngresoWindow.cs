using Gtk;

    class UsuarioIngresoWindow: Window
{
    public UsuarioIngresoWindow() : base("Ingreso de usuario")
    {

        SetDefaultSize(300, 500);
        SetPosition(WindowPosition.Center);
        DeleteEvent += (o, args) => Application.Quit();

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

        botonIngresar.Clicked += (sender, e) => {
            string id = entradaId.Text;
            string nombre = entradaNombre.Text;
            string apellido = entradaApellido.Text;
            string correo = entradaCorreo.Text;
            string telefono = entradaTelefono.Text;

            

            };

            Add(contenedor);
            ShowAll();
            
}
}