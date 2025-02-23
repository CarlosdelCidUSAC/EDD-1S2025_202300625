using System;
using System.Runtime.InteropServices;

unsafe struct NodoRepuestos
{
    public int Id;
    public char* Repuesto;
    public char* Detalle;
    public char* Costo;
    public NodoRepuestos* Siguiente;
}

unsafe class RepuestosListaCircular
{
    private NodoRepuestos* primero;

    public int tamanio

    RepuestosListaCircular()
    {
        primero = null;
    }

    public void agregar(int id, string repuesto, string detalle, string costo)
    {
        if (string.IsNullOrEmpty(repuesto) || string.IsNullOrEmpty(detalle) || string.IsNullOrEmpty(costo) )
        {
            throw new ArgumentException("Los campos no pueden ser nulos o vacíos.");
        }

        NodoRepuestos* nuevoNodoRepuestos = (NodoRepuestos*)Marshal.AllocHGlobal(sizeof(NodoRepuestos));
        
        if (primero == null)
        {
            primero = nuevoNodoRepuestos;
            primero->Id = id;
            primero->Repuesto = (char*)Marshal.StringToHGlobalAnsi(repuesto);
            primero->Detalle = (char*)Marshal.StringToHGlobalAnsi(detalle);
            primero->Costo = (char*)Marshal.StringToHGlobalAnsi(costo);
            ultimo = primero;
            primero->Siguiente = primero;
            tamanio++;

        }
        else 
        {
            NodoRepuestos* actual = primero;
            while (actual->Siguiente != primero)
            {
                actual = actual->Siguiente;
            }
            nuevoNodoRepuestos->Id = id;
            nuevoNodoRepuestos->Repuesto = (char*)Marshal.StringToHGlobalAnsi(repuesto);
            nuevoNodoRepuestos->Detalle = (char*)Marshal.StringToHGlobalAnsi(detalle);
            nuevoNodoRepuestos->Costo = (char*)Marshal.StringToHGlobalAnsi(costo);
            nuevoNodoRepuestos->Siguiente = primero;
            actual->Siguiente = nuevoNodoRepuestos;
            nuevoNodoRepuestos->Siguiente = primero;
            tamanio++;
        }
        
    }
    }

    
    public NodoRepuestos* Buscar(int id)
    {
        NodoRepuestos* actual = primero;

        for (int i = 0; i < tamanio; i++)
        {
            if (actual->Id == id)
            {
                return actual;
            }
            actual = actual->Siguiente;
        }

        return null; 
    }

    
    public void Borrar(int id)
    {
        NodoRepuestos* actual = primero;
        NodoRepuestos* previo = null;

        for (int i = 0; i < tamanio; i++)
        {
            if (actual->Id == id)
            {
                Marshal.FreeHGlobal((IntPtr)actual->Repuesto);
                Marshal.FreeHGlobal((IntPtr)actual->Detalle);
                Marshal.FreeHGlobal((IntPtr)actual->Costo);

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

    
/*     public void Imprimir()
    {
        NodoRepuestos* actual = cabeza;

        while (actual != null)
        {
            Console.WriteLine("Id: {0}, Repuesto: {1}, Detalle: {2}, Costo: {3}", actual->Id, new string(actual->Repuesto), new string(actual->Detalle), new string(actual->Costo));
            actual = actual->Siguiente;
        }
    } */
