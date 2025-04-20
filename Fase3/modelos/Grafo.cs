using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Gtk;

class SubNodo
{
    public int Valor { get; set; }
    public SubNodo? Siguiente { get; set; }

    public SubNodo(int valor)
    {
        Valor = valor;
        Siguiente = null;
    }
}

class Nodo
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public SubNodo? ListaAdyacente { get; set; }
    public Nodo? Izquierda { get; set; }
    public Nodo? Derecha { get; set; }

    public Nodo(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
        ListaAdyacente = null;
        Izquierda = null;
        Derecha = null;
    }

    public void AgregarArista(int destino)
    {

        SubNodo? actual = ListaAdyacente;
        while (actual != null)
        {
            if (actual.Valor == destino)
                return; 
            actual = actual.Siguiente;
        }
        SubNodo nuevo = new SubNodo(destino);
        if (ListaAdyacente == null)
        {
            ListaAdyacente = nuevo;
        }
        else
        {
            actual = ListaAdyacente;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevo;
        }
    }
    
    public string ObtenerCadena()
    {
        StringBuilder cadena = new StringBuilder();
        cadena.Append($"ID: {Id}, Nombre: {Nombre}");
        if (ListaAdyacente != null)
        {
            cadena.Append(", Aristas: ");
            SubNodo? actual = ListaAdyacente;
            while (actual != null)
            {
                cadena.Append($"{actual.Valor} ");
                actual = actual.Siguiente;
            }
        }
        return cadena.ToString();
    }
}

class ListaDeLista
{
    public Nodo? CabeceraVehiculo { get; set; }
    public Nodo? CabeceraRepuesto { get; set; }
    public Nodo? ColaVehiculo { get; set; }
    public Nodo? ColaRepuesto { get; set; }

    public ListaDeLista()
    {
        CabeceraRepuesto = null;
        CabeceraVehiculo = null;
        ColaRepuesto = null;
        ColaVehiculo = null;
    }

    public void AgregarNodo(int id, int valor)
    {
        Nodo? vehiculo = CabeceraVehiculo;
        while (vehiculo != null && vehiculo.Id != id)
        {
            vehiculo = vehiculo.Derecha;
        }
        if (vehiculo == null)
        {
            vehiculo = new Nodo(id, "V");
            if (CabeceraVehiculo == null)
            {
                CabeceraVehiculo = vehiculo;
                ColaVehiculo = vehiculo;
            }
            else
            {
                ColaVehiculo!.Derecha = vehiculo;
                vehiculo.Izquierda = ColaVehiculo;
                ColaVehiculo = vehiculo;
            }
        }
        vehiculo.AgregarArista(valor);

    
        Nodo? repuesto = CabeceraRepuesto;
        while (repuesto != null && repuesto.Id != valor)
        {
            repuesto = repuesto.Derecha;
        }
        if (repuesto == null)
        {
            repuesto = new Nodo(valor, "R");
            if (CabeceraRepuesto == null)
            {
                CabeceraRepuesto = repuesto;
                ColaRepuesto = repuesto;
            }
            else
            {
                ColaRepuesto!.Derecha = repuesto;
                repuesto.Izquierda = ColaRepuesto;
                ColaRepuesto = repuesto;
            }
        }
        repuesto.AgregarArista(id);
    }

    public void ImprimirLista()
    {
        Nodo? actual = CabeceraVehiculo;
        Console.WriteLine("Lista de Vehiculos:");
        while (actual != null)
        {
            Console.WriteLine($"ID: {actual.Id}");
            actual = actual.Derecha;
        }
        Nodo? actual2 = CabeceraRepuesto;
        Console.WriteLine("Lista de Repuestos:");
        while (actual2 != null)
        {
            Console.WriteLine($"ID: {actual2.Id}");
            actual2 = actual2.Derecha;
        }
    }

    public string ObtenerConexiones()
    {
        StringBuilder conexiones = new StringBuilder();
        Nodo? actual = CabeceraVehiculo;
        while (actual != null)
        {
            conexiones.Append(actual.ObtenerCadena() + "\n");
            actual = actual.Derecha;
        }
        Nodo? actual2 = CabeceraRepuesto;
        while (actual2 != null)
        {
            conexiones.Append(actual2.ObtenerCadena() + "\n");
            actual2 = actual2.Derecha;
        }
        conexiones.Append("Vehiculos y Repuestos Conectados:\n");
        return conexiones.ToString();
    }

    public void Graficar()
    {
        StringBuilder dot = new StringBuilder();
        dot.Append("graph G {\n");
        dot.Append("node [shape=circle];\n");
        dot.Append("rankdir=LR;\n");
        dot.Append("label=\"Lista de Listas\";\n");
        Nodo? actual1 = CabeceraVehiculo;
        Nodo? actual2 = CabeceraRepuesto;

        while (actual2 != null)
        {
            dot.Append($"\"R{actual2.Id}\" [label=\"{actual2.Nombre + actual2.Id}\"];\n");
            actual2 = actual2.Derecha;
        }

        actual1 = CabeceraVehiculo;
        while (actual1 != null)
        {
            dot.Append($"\"V{actual1.Id}\" [label=\"{actual1.Nombre + actual1.Id}\"];\n");
            SubNodo? adyacente1 = actual1.ListaAdyacente;
            while (adyacente1 != null)
            {
            dot.Append($"\"V{actual1.Id}\" -- \"R{adyacente1.Valor}\";\n");
            Console.WriteLine($"V{actual1.Id} -- R{adyacente1.Valor}");
            adyacente1 = adyacente1.Siguiente;
            }
            actual1 = actual1.Derecha;
        }
        dot.Append("}\n");
        try
        {
            string rutaDot = "grafo.dot";
            string rutaReporte = "grafo.png";
            File.WriteAllText(rutaDot, dot.ToString());
            Process proceso = new Process();
            proceso.StartInfo.FileName = "dot";
            proceso.StartInfo.Arguments = $"-Tpng {rutaDot} -o {rutaReporte}";
            proceso.StartInfo.RedirectStandardOutput = true;
            proceso.StartInfo.UseShellExecute = false;
            proceso.StartInfo.CreateNoWindow = true;
            proceso.Start();
            proceso.WaitForExit();

            if (File.Exists(rutaReporte))
            {
                Console.WriteLine("Reporte generado con éxito");
                Process.Start(new ProcessStartInfo(rutaReporte) { UseShellExecute = true });
            } 
            else
            {
                Console.WriteLine("Error al generar el reporte");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al graficar: {ex.Message}");
        }
    }
}