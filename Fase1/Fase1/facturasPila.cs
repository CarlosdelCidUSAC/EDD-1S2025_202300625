using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

unsafe struct NodoFactura
{
    public int* Id;
    public int* Id_Servicio;
    public int* Id_Vehiculo;
    public char* Detalles;
    public float* Costo;
    public int* id_Orden;
    public NodoFactura* Siguiente;
}

unsafe class FacturasPila
{
    private NodoFactura* cabeza;

    public FacturasPila()
    {
        cabeza = null;
    }


    public void enpilar(int id, int id_Servicio, int id_vehiculo, string detalles, float costo)
    {
        if (string.IsNullOrEmpty(detalles))
        {
            throw new ArgumentException("Los campos no pueden ser nulos o vacíos.");
        }

        NodoFactura* nuevoNodoFactura = (NodoFactura*)Marshal.AllocHGlobal(sizeof(NodoFactura));

        nuevoNodoFactura->Id = (int*)Marshal.AllocHGlobal(sizeof(int));
        nuevoNodoFactura->Id_Servicio = (int*)Marshal.AllocHGlobal(sizeof(int));
        nuevoNodoFactura->Id_Vehiculo = (int*)Marshal.AllocHGlobal(sizeof(int));
        nuevoNodoFactura->Costo = (float*)Marshal.AllocHGlobal(sizeof(float));
        nuevoNodoFactura->id_Orden = (int*)Marshal.AllocHGlobal(sizeof(int));

        *nuevoNodoFactura->Id = id;
        *nuevoNodoFactura->Id_Servicio = id_Servicio;
        *nuevoNodoFactura->Id_Vehiculo = id_vehiculo;
        nuevoNodoFactura->Detalles = (char*)Marshal.StringToHGlobalAnsi(detalles);
        *nuevoNodoFactura->Costo = costo;

        if (cabeza == null)
        {
            *nuevoNodoFactura->id_Orden = 1;
            nuevoNodoFactura->Siguiente = null;
        }
        else
        {
            *nuevoNodoFactura->id_Orden = *cabeza->id_Orden + 1;
            nuevoNodoFactura->Siguiente = cabeza;
        }

        cabeza = nuevoNodoFactura;
    }

    public NodoFactura* desenpilar()
    {
        if (cabeza == null)
        {
            return null;
        }

        NodoFactura* actual = cabeza;
        cabeza = cabeza->Siguiente;

        Marshal.FreeHGlobal((IntPtr)actual->Detalles);
        Marshal.FreeHGlobal((IntPtr)actual);

        return actual;

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

public int ObtenerID()
{
    if (cabeza != null)
    {
        return *cabeza->Id;
    }
    else
    {
        return -1;
    }
}

public int ObtenerIdServicio()
{
    if (cabeza != null)
    {
        return *cabeza->Id_Servicio;
    }
    else
    {
        return -1;
    }


}

public int ObtenerIDOrden(){
    if (cabeza != null)
    {
        return *cabeza->id_Orden;
    }
    else
    {
        return -1;
    }
}

public float ObtenerCosto()
{
    if (cabeza != null)
    {
        return *cabeza->Costo;
    }
    else
    {
        return -1;
    }
}

    public void Graficar()
    {
        string rutaDot = "reportedot/pila.dot";
        string rutaReporte = "reportes/pila.png";
        string codigoDot = "digraph G {\n  rankdir=TB;\n  node [shape=record, height=.1];\n  label=\"Pila\";\n  labelloc=\"t\";\n";

        NodoFactura* actual = cabeza;
        int contadorNodos = 0;

        while (actual != null)
        {
            string Id = "ID: " + (*actual->Id).ToString();
            string Id_Orden = "ID Orden: " + (*actual->id_Orden).ToString();
            string Costo = "Costo: " + (*actual->Costo).ToString();
            codigoDot += $"node{contadorNodos} [label=\"{Id}\\n{Id_Orden}\\n{Costo}\"];\n";
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
