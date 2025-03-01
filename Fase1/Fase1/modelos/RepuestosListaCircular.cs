using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

unsafe struct NodoRepuestos
{
    public int* Id;
    public char* Repuesto;
    public char* Detalle;
    public float* Costo;
    public NodoRepuestos* Siguiente;
}

unsafe class RepuestosListaCircular
{
    private NodoRepuestos* primero;

    public int tamanio;

    public RepuestosListaCircular()
    {
        primero = null;
    }

    public void Agregar(int id, string repuesto, string detalle, float costo)
    {
        if (string.IsNullOrEmpty(repuesto) || string.IsNullOrEmpty(detalle))
        {
            throw new ArgumentException("Los campos no pueden ser nulos o vacíos.");
        }

        NodoRepuestos* nuevoNodoRepuestos = (NodoRepuestos*)Marshal.AllocHGlobal(sizeof(NodoRepuestos));
        nuevoNodoRepuestos->Id = (int*)Marshal.AllocHGlobal(sizeof(int));
        nuevoNodoRepuestos->Repuesto = (char*)Marshal.StringToHGlobalAnsi(repuesto);
        nuevoNodoRepuestos->Detalle = (char*)Marshal.StringToHGlobalAnsi(detalle);
        nuevoNodoRepuestos->Costo = (float*)Marshal.AllocHGlobal(sizeof(float));
        *nuevoNodoRepuestos->Id = id;
        *nuevoNodoRepuestos->Costo = costo;

        if (primero == null)
        {
            primero = nuevoNodoRepuestos;
            primero->Siguiente = primero;
        }
        else
        {
            NodoRepuestos* actual = primero;
            while (actual->Siguiente != primero)
            {
                actual = actual->Siguiente;
            }
            actual->Siguiente = nuevoNodoRepuestos;
            nuevoNodoRepuestos->Siguiente = primero;
        }
        tamanio++;
    }
    

    
    public int Buscar(int id)
    {
        NodoRepuestos* actual = primero;

        for (int i = 0; i < tamanio; i++)
        {
            if (*actual->Id == id)
            {
                return id;
            }
            actual = actual->Siguiente;
        }

        return -1; 
    }

    
    public void Borrar(int id)
    {
        NodoRepuestos* actual = primero;
        NodoRepuestos* previo = null;

        for (int i = 0; i < tamanio; i++)
        {
            if (*actual->Id == id)
            {
                Marshal.FreeHGlobal((IntPtr)actual->Repuesto);
                Marshal.FreeHGlobal((IntPtr)actual->Detalle);

                if (previo == null)
                {
                    primero = actual->Siguiente;
                }
                else
                {
                    previo->Siguiente = actual->Siguiente;
                }

                Marshal.FreeHGlobal((IntPtr)actual);
                tamanio--;
                return;
            }

            previo = actual;
            actual = actual->Siguiente;
        }
    }

    
     public void Imprimir()
    {
        NodoRepuestos* actual = primero;
        if (actual == null)
        {
            Console.WriteLine("La lista de repuestos está vacía.");
            return;
        }

        for(int i = 0; i < tamanio; i++)
        {
            Console.WriteLine("ID: " + *actual->Id);
            Console.WriteLine("Repuesto: " + Marshal.PtrToStringAnsi((IntPtr)actual->Repuesto));
            Console.WriteLine("Detalle: " + Marshal.PtrToStringAnsi((IntPtr)actual->Detalle));
            actual = actual->Siguiente;
        }
    } 

    public bool CabezaIsNotNull()
    {
        if (primero != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float BuscarCosto(int id)
    {
        NodoRepuestos* actual = primero;

        for (int i = 0; i < tamanio; i++)
        {
            if (*actual->Id == id)
            {
                return *actual->Costo;
            }
            actual = actual->Siguiente;
        }

        return -1.0f; 
    }

        public void Graficar()
        {
            string codigoDot = "digraph G {\nrankdir=LR;\nnode[shape=record, height=.1];\nlabel=\"Lista circular\";\n";
            NodoRepuestos* actual = primero;
            int contadorNodos = 0;

            if (actual == null)
            {
            Console.WriteLine("La lista está vacía, no se puede graficar.");
            return;
            }

            for (int i = 0; i < tamanio; i++)
            {
            string repuesto = actual->Repuesto != null ? Marshal.PtrToStringAnsi((IntPtr)actual->Repuesto) : "null";
            string detalle = actual->Detalle != null ? Marshal.PtrToStringAnsi((IntPtr)actual->Detalle) : "null";
            float costo = actual->Costo != null ? *actual->Costo : 0.0f;
            codigoDot += $"nodo{contadorNodos}[label=\"ID: {*actual->Id}\\nRepuesto: {repuesto}\\nDetalle: {detalle}\\nCosto: {costo}\"];\n";
            actual = actual->Siguiente;
            contadorNodos++;
            }

            for (int i = 0; i < tamanio - 1; i++)
            {
            codigoDot += $"nodo{i} -> nodo{i + 1};\n";
            }
            codigoDot += $"nodo{tamanio - 1} -> nodo0 [dir=both];\n";
            codigoDot += "}";

            string rutaReporte = "reportes/lista_circular.png";
            string rutaDot = "reportedot/lista_circular.dot";
            Directory.CreateDirectory(Path.GetDirectoryName(rutaDot));
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
            Console.WriteLine("Reporte generado con éxito");
            Process.Start(new ProcessStartInfo(rutaReporte) { UseShellExecute = true });
            }
            else
            {
            Console.WriteLine("Error al generar el reporte");
            }
        }
}