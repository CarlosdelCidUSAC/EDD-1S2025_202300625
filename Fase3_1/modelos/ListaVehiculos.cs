using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Gtk;
class NodoVehiculo {
    public int id;
    public int id_usuario;
    public string marca = string.Empty;
    public int anio;
    public string placa = string.Empty;
    public NodoVehiculo? siguiente;
    public NodoVehiculo? anterior;
}

class ListaVehiculos {

    private NodoVehiculo? cabeza;

    public NodoVehiculo? Cabeza {
        get { return cabeza; }
    }

    public ListaVehiculos() {
        cabeza = null;
    }

    public bool AgregarPrimero(int id, int id_usuario, string marca, int anio, string placa) {
        if (Buscar(id) != null) {
            Console.WriteLine($"Error: Ya existe un vehículo con el ID {id}.");
            return false;
        }
        NodoVehiculo? nuevo = new NodoVehiculo {
            id = id,
            id_usuario = id_usuario,
            marca = marca,
            anio = anio,
            placa = placa,
            siguiente = cabeza,
            anterior = null
        };
        if (cabeza != null) {
            cabeza.anterior = nuevo;
        }
        cabeza = nuevo;
        return true;
    }

    public void Imprimir() {
        NodoVehiculo? actual = cabeza;
        while (actual != null) {
            Console.WriteLine("ID: " + actual.id);
            Console.WriteLine("ID Usuario: " + actual.id_usuario);
            Console.WriteLine("Marca: " + actual.marca);
            Console.WriteLine("Modelo: " + actual.anio);
            Console.WriteLine("Placa: " + actual.placa);
            Console.WriteLine();
            actual = actual?.siguiente;
        }
    }

    public NodoVehiculo? Buscar(int id) {
        NodoVehiculo? actual = cabeza;
        while (actual != null) {
            if (actual.id == id) {
                return actual;
            }
            actual = actual.siguiente;
        }
        return null;
    }

    public void Eliminar(int id) {
        if (cabeza == null) {
            return;
        }
        if (cabeza.id == id) {
            cabeza = cabeza.siguiente;
            if (cabeza != null) {
                cabeza.anterior = null;
            }
            return;
        }
        NodoVehiculo? actual = cabeza;
        while (actual.siguiente != null) {
            if (actual.siguiente.id == id) {
                actual.siguiente = actual.siguiente.siguiente;
                if (actual.siguiente != null) {
                    actual.siguiente.anterior = actual;
                }
                return;
            }
            actual = actual.siguiente;
        }
    }

    public ListStore mostrarTabla() {
        ListStore modelo = new ListStore(typeof(int), typeof(int), typeof(string), typeof(int), typeof(string));
        NodoVehiculo? actual = cabeza;
        while (actual != null) {
            modelo.AppendValues(actual.id, actual.id_usuario, actual.marca, actual.anio, actual.placa);
            actual = actual.siguiente;
        }
        return modelo;
    }

    public void Graficar()
{
    string codigodot = "digraph G {\n";
    codigodot += "rankdir=LR;\n"; 
    codigodot += "node [shape=record];\n"; 
    codigodot += "subgraph cluster_ListaDobleEnlazada {\n";
    codigodot += "label = \"Lista Doblemente Enlazada\";\n";

    NodoVehiculo? actual = cabeza;
    while (actual != null)
    {
        string label = $"ID: {actual.id}\\nID Usuario: {actual.id_usuario}\\nMarca: {actual.marca}\\nModelo: {actual.anio}\\nPlaca: {actual.placa}";
        codigodot += $"\"{actual.id}\" [label=\"{label}\"];\n";
        if (actual.siguiente != null)
        {
            codigodot += $"\"{actual.id}\" -> \"{actual.siguiente.id}\";\n";
            codigodot += $"\"{actual.siguiente.id}\" -> \"{actual.id}\";\n";
        }
        actual = actual.siguiente;
    }

    codigodot += "}\n";
    codigodot += "}\n";

    string rutaDot = "reportedot/lista_doble.dot";
    string rutaReporte = "Reportes/lista_doble.png";

    var directory = Path.GetDirectoryName(rutaDot);
    if (!string.IsNullOrEmpty(directory))
    {
        Directory.CreateDirectory(directory);
    }
    File.WriteAllText(rutaDot, codigodot);

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

    public bool EstaVacia() {
        return cabeza == null;
    }
}