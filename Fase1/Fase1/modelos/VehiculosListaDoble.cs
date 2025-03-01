using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

unsafe struct NodoVehiculo
{
    public int* Id;
    public int* Id_Usuario;
    public char* Marca;
    public int* Anio;
    public char* Placa;
    public NodoVehiculo* Siguiente;
    public NodoVehiculo* Anterior;  
}

unsafe class VehiculoListaDoble
{

    private NodoVehiculo* cabeza;
    private NodoVehiculo* cola;

    public VehiculoListaDoble()
    {
        cabeza = null;
        cola = null;
    }
    
    public void AgregarPrimero(int id, int idUsuario, string marca, int anio, string placa)
    {
        if (string.IsNullOrEmpty(marca) || string.IsNullOrEmpty(placa))
        {
            throw new ArgumentException("Los campos no pueden ser nulos o vacíos.");
        }

        NodoVehiculo* nuevoNodoVehiculo = (NodoVehiculo*)Marshal.AllocHGlobal(sizeof(NodoVehiculo));
        nuevoNodoVehiculo->Id = (int*)Marshal.AllocHGlobal(sizeof(int));
        nuevoNodoVehiculo->Id_Usuario = (int*)Marshal.AllocHGlobal(sizeof(int));
        nuevoNodoVehiculo->Anio = (int*)Marshal.AllocHGlobal(sizeof(int));
        *nuevoNodoVehiculo->Id = id;
        *nuevoNodoVehiculo->Id_Usuario = idUsuario;
        nuevoNodoVehiculo->Marca = (char*)Marshal.StringToHGlobalAnsi(marca);
        *nuevoNodoVehiculo->Anio = anio;
        nuevoNodoVehiculo->Placa = (char*)Marshal.StringToHGlobalAnsi(placa);

        if (cabeza == null)
        {
            cabeza = nuevoNodoVehiculo;
            cabeza->Siguiente = null;
            cabeza->Anterior = null;
            cola = cabeza;
        }
        else
        {
            nuevoNodoVehiculo->Siguiente = cabeza;
            cabeza->Anterior = nuevoNodoVehiculo;
            cabeza = nuevoNodoVehiculo;
            cabeza->Anterior = null;
        }
    }

    

    public void Borrar(int id)
    {

        NodoVehiculo* actual = cabeza;
        while(actual != null)
        {
            if (*actual->Id == id)
            {
                if (actual->Anterior != null)
                {
                    actual->Anterior->Siguiente = actual->Siguiente;
                }
                else
                {
                    cabeza = actual->Siguiente;
                }

                if (actual->Siguiente != null)
                {
                    actual->Siguiente->Anterior = actual->Anterior;
                }
                else
                {
                    cola = actual->Anterior;
                }

                Marshal.FreeHGlobal((IntPtr)actual->Marca);
                Marshal.FreeHGlobal((IntPtr)actual->Placa);
                Marshal.FreeHGlobal((IntPtr)actual);
                return;
            }
        }
    }

    public void LiberarListaDoble()
    {
        NodoVehiculo* actual = cabeza;
        while (actual != null)
        {
            NodoVehiculo* temp = actual;
            actual = actual->Siguiente;
            Marshal.FreeHGlobal((IntPtr)temp->Marca);
            Marshal.FreeHGlobal((IntPtr)temp->Placa);
            Marshal.FreeHGlobal((IntPtr)temp);
        }
        cabeza = null;
        cola = null;
    }
    

    public void Imprimir()
    {
        NodoVehiculo* actual = cabeza;
        if (actual == null)
        {
            Console.WriteLine("La lista está vacía.");
            return;
        }
        while (actual != null)
        {
            Console.WriteLine($"ID: {*actual->Id}");
            Console.WriteLine($"ID Usuario: {*actual->Id_Usuario}");
            Console.WriteLine($"Marca: {Marshal.PtrToStringAnsi((IntPtr)actual->Marca)}");
            Console.WriteLine($"Año: {*actual->Anio}");
            Console.WriteLine($"Placa: {Marshal.PtrToStringAnsi((IntPtr)actual->Placa)}");
            actual = actual->Siguiente;
        }
    }
    
    public int Buscar(int id)
    {
        NodoVehiculo* actual = cabeza;
        while (actual != null)
        {
            if (*actual->Id == id)
            {
                return *actual->Id;
            }
            actual = actual->Siguiente;
        }
        return -1;
    }

    public bool CabezaIsNotNull()
    {
        if (cabeza != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

public void Graficar()
{
    string codigodot = "digraph G {\n";
    codigodot += "rankdir=LR;\n"; 
    codigodot += "node [shape=record];\n"; 
    codigodot += "subgraph cluster_ListaDobleEnlazada {\n";
    codigodot += "label = \"Lista Doblemente Enlazada\";\n";

    NodoVehiculo* actual = cabeza;
    while (actual != null)
    {
        string label = $"ID: {*actual->Id}\\nID Usuario: {*actual->Id_Usuario}\\nMarca: {Marshal.PtrToStringAnsi((IntPtr)actual->Marca)}\\nModelo: {*actual->Anio}\\nPlaca: {Marshal.PtrToStringAnsi((IntPtr)actual->Placa)}";
        codigodot += $"\"{*actual->Id}\" [label=\"{label}\"];\n";
        if (actual->Siguiente != null)
        {
            codigodot += $"\"{*actual->Id}\" -> \"{*actual->Siguiente->Id}\";\n";
            codigodot += $"\"{*actual->Siguiente->Id}\" -> \"{*actual->Id}\";\n";
        }
        actual = actual->Siguiente;
    }

    codigodot += "}\n";
    codigodot += "}\n";

    string rutaDot = "reportedot/lista_doble.dot";
    string rutaReporte = "reportes/lista_doble.png";

    Directory.CreateDirectory(Path.GetDirectoryName(rutaDot));
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


}