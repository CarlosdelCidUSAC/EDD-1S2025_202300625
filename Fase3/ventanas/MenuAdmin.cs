using Gtk;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Diagnostics;

class MenuAdmin : Window
{
    public MenuAdmin() : base("menu Administrador")
    {
        SetDefaultSize(300, 400);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { 
            Program.usuarios.Backup();
            Program._login = new Login(); 
            Program._login.ShowAll(); 
            Hide(); // Use Hide() instead of Destroy()
        };

        Fixed contenedor = new Fixed();


        Button botonCargaMasiva = new Button("Carga Masiva");
        Button botonInsertarUsuario = new Button("Insertar Usuario");
        Button botonVisualizarRepuestos = new Button("Visualizar Repuestos");
        Button botonCrearServicio = new Button("Crear Servicio");
        Button botonReportes = new Button("Reportes");

       

        if (botonCargaMasiva.Parent != null)
        {
            ((Container)botonCargaMasiva.Parent).Remove(botonCargaMasiva);
        }
        contenedor.Put(botonCargaMasiva, 100, 60);

        if (botonInsertarUsuario.Parent != null)
        {
            ((Container)botonInsertarUsuario.Parent).Remove(botonInsertarUsuario);
        }
        contenedor.Put(botonInsertarUsuario, 100, 110);

        if (botonVisualizarRepuestos.Parent != null)
        {
            ((Container)botonVisualizarRepuestos.Parent).Remove(botonVisualizarRepuestos);
        }
        contenedor.Put(botonVisualizarRepuestos, 100, 160);

        if (botonCrearServicio.Parent != null)
        {
            ((Container)botonCrearServicio.Parent).Remove(botonCrearServicio);
        }
        contenedor.Put(botonCrearServicio, 100, 210);
        if (botonReportes.Parent != null)
        {
            ((Container)botonReportes.Parent).Remove(botonReportes);
        }
        contenedor.Put(botonReportes, 100, 260);
        contenedor.SetSizeRequest(400, 400);
        botonCargaMasiva.SetSizeRequest(200, 30);
        botonInsertarUsuario.SetSizeRequest(200, 30);
        botonVisualizarRepuestos.SetSizeRequest(200, 30);
        botonCrearServicio.SetSizeRequest(200, 30);
        botonReportes.SetSizeRequest(200, 30);
        
        botonCargaMasiva.Clicked += (sender, e) =>
        {
            Program._cargaMasiva = new CargaMasiva();
            Program._cargaMasiva.ShowAll();
        };
        botonInsertarUsuario.Clicked += (sender, e) =>
        {
            if (Program._insertarUsuario == null || !Program._insertarUsuario.Visible)
            {
                Program._insertarUsuario = new InsertarUsuario();
                Program._insertarUsuario.ShowAll();
            }
            else
            {
                Program._insertarUsuario.Present();
            }
        };
        botonVisualizarRepuestos.Clicked += (sender, e) =>
        {
            Program._visualizacionRepuestos = new VisualizacionRepuestos();
            Program._visualizacionRepuestos.ShowAll();
        };
        botonCrearServicio.Clicked += (sender, e) =>
        {
            if (Program._crearServicio == null || !Program._crearServicio.Visible)
            {
                Program._crearServicio = new CrearServicio();
                Program._crearServicio.ShowAll();
            }
            else
            {
                Program._crearServicio.Present();
            }
        };
        botonReportes.Clicked += (sender, e) =>
        {

            if (Program.usuarios.Cadena.Count > 0){
                Program.usuarios.Graficar();
            }
            if (Program.repuestos.Raiz != null){
                Program.repuestos.Graficar();
            }
            if (Program.vehiculos.Cabeza != null){
                Program.vehiculos.Graficar();
            }
            if(Program.servicios.Raiz != null){
                Program.servicios.Graficar();
            }
            if (!Program.grafo.EstaVacia()){
                Program.grafo.Graficar();
            }
            if (!Program.merkle.EstaVacia()){
                Program.merkle.Graficar();
            }
            using (MessageDialog dialogo = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Reportes generados"))
            {
                dialogo.Run();
                dialogo.Destroy();
            }
        };
        Add(contenedor);
    
        
    }
}