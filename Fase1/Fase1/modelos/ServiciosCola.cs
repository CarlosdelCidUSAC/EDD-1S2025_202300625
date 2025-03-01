using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

unsafe struct NodoServicio
{
    public int* Id;
    public int* Id_Servicio;
    public int* Id_Vehiculo;
    public char* Detalles;
    public float* Costo;
    public NodoServicio* Siguiente;
}

unsafe class SeviciosCola
{
    public NodoServicio* cabeza;
    private NodoServicio* cola;
    public void Encolar(int id, int Id_Servicio, int Id_Vehiculo, string Detalles, float Costo)
    {
        if (string.IsNullOrEmpty(Detalles))
        {
            throw new ArgumentException("Los campos no pueden ser nulos o vacíos.");
        }

        NodoServicio* nuevoNodoServicio = (NodoServicio*)Marshal.AllocHGlobal(sizeof(NodoServicio));
        nuevoNodoServicio->Id = (int*)Marshal.AllocHGlobal(sizeof(int));
        *nuevoNodoServicio->Id = id;
        nuevoNodoServicio->Id_Servicio = (int*)Marshal.AllocHGlobal(sizeof(int));
        *nuevoNodoServicio->Id_Servicio = Id_Servicio;
        nuevoNodoServicio->Id_Vehiculo = (int*)Marshal.AllocHGlobal(sizeof(int));
        *nuevoNodoServicio->Id_Vehiculo = Id_Vehiculo;
        nuevoNodoServicio->Detalles = (char*)Marshal.StringToHGlobalAnsi(Detalles);
        nuevoNodoServicio->Costo = (float*)Marshal.AllocHGlobal(sizeof(float));
        *nuevoNodoServicio->Costo = Costo;
        nuevoNodoServicio->Siguiente = null;

        if (cabeza == null)
        {
            cabeza = nuevoNodoServicio;
            cola = nuevoNodoServicio;
        }
        else
        {
            cola->Siguiente = nuevoNodoServicio;
            cola = nuevoNodoServicio;
        }
    }

    public NodoServicio* Desencolar()
    {
        if (cabeza == null)
        {
            return null;
        }

        NodoServicio* actual = cola;
        cola = cola->Siguiente;

        if (cabeza == null)
        {
            cola = null;
        }

        Marshal.FreeHGlobal((IntPtr)actual->Detalles);
        Marshal.FreeHGlobal((IntPtr)actual->Costo);
        Marshal.FreeHGlobal((IntPtr)actual);

        return actual;
    }

    public NodoServicio* LiberarCola()
    {
        NodoServicio* actual = cabeza;
        NodoServicio* siguiente = null;

        while (actual != null)
        {
            siguiente = actual->Siguiente;
            Marshal.FreeHGlobal((IntPtr)actual->Detalles);
            Marshal.FreeHGlobal((IntPtr)actual);
            actual = siguiente;
        }

        cabeza = null;
        cola = null;
        return null;
    }
    public int Buscar(int id)
    {
        NodoServicio* actual = cabeza;

        while (actual != null)
        {
            if (actual != null && *actual->Id == id)
            {
                return id;
            }
            actual = actual->Siguiente;
        }

        return -1;
    }

    public void Imprimir()
    {
        NodoServicio* actual = cabeza;
        if (actual == null)
        {
            Console.WriteLine("La cola está vacía.");
            return;
        }

        while (actual != null)
        {
            Console.WriteLine($"ID: {*actual->Id}");
            Console.WriteLine($"ID Servicio: {*actual->Id_Servicio}");
            Console.WriteLine($"ID Vehículo: {*actual->Id_Vehiculo}");
            Console.WriteLine($"Detalles: {Marshal.PtrToStringAnsi((IntPtr)actual->Detalles)}");
            Console.WriteLine($"Costo: {*actual->Costo}");
            Console.WriteLine();
            actual = actual->Siguiente;
        }
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
        string rutaDot = "reportedot/cola.dot";
        string rutaReporte = "reportes/cola.png";
        string codigoDot = "digraph G {\n  rankdir=LR;\n  node [shape=record, height=.1];\n  label=\"Cola\";\n  labelloc=\"t\";\n";

        NodoServicio* actual = cabeza;
        int contadorNodos = 0;

        while (actual != null)
        {
            string Id = "ID: " + (*actual->Id).ToString();
            string Id_Servicio = "ID Servicio: " + (*actual->Id_Servicio).ToString();
            string Id_Vehiculo = "ID Vehículo: " + (*actual->Id_Vehiculo).ToString();
            string Detalles = "Detalles: " + (actual->Detalles != null ? Marshal.PtrToStringAnsi((IntPtr)actual->Detalles) : "null");
            string Costo = "Costo: " + (*actual->Costo).ToString();
            codigoDot += $"node{contadorNodos} [label=\"{{{Id}\\n{Id_Servicio}\\n{Id_Vehiculo}\\n{Detalles}\\n{Costo}}}\"]\n";
            contadorNodos++;
            actual = actual->Siguiente;
        }

        actual = cabeza;
        contadorNodos = 0;

        while (actual != null && actual->Siguiente != null)
        {
            codigoDot += $"node{contadorNodos} -> node{contadorNodos + 1};\n";
            contadorNodos++;
            actual = actual->Siguiente;
        }

        codigoDot += "}";

        File.WriteAllText(rutaDot, codigoDot);

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
            Console.WriteLine("Reporte generado con �xito");
            Process.Start(new ProcessStartInfo(rutaReporte) { UseShellExecute = true });
        }
        else
        {
            Console.WriteLine("Error al generar el reporte");
        }
    }

}
