using Gtk;

class CargaMasiva : Window
{
    public CargaMasiva() : base("Carga Masiva")
    {
        SetDefaultSize(400, 350);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { Hide(); };

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
        
        Add(contenedor);

        usuariosBoton.Clicked += (sender, e) =>
        {
            MessageDialog dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Carga de usuarios iniciada");
            dialog.Run();
            dialog.Destroy();
        };
        vehiculosBoton.Clicked += (sender, e) =>
        {
            MessageDialog dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Carga de vehículos iniciada");
            dialog.Run();
            dialog.Destroy();
        };
        repuestosBoton.Clicked += (sender, e) =>
        {
            MessageDialog dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Carga de repuestos iniciada");
            dialog.Run();
            dialog.Destroy();
        };
    }
}