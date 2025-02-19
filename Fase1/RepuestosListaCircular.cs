using System;
using System.Runtime.InteropServices;

unsafe struct Nodo
{
    public int Id;
    public char* Repuesto;
    public char* Detalle;
    public char* Costo;
    public Nodo* Siguiente;
}

unsafe class RepuestosListaCircular
{
    private Nodo* primero;
    private Nodo* ultimo;

    public int tamanio

    public void agregar(int id, string repuesto, string detalle, string costo)
    {
        if (string.IsNullOrEmpty(repuesto) || string.IsNullOrEmpty(detalle) || string.IsNullOrEmpty(costo) )
        {
            throw new ArgumentException("Los campos no pueden ser nulos o vacíos.");
        }

        Nodo* nuevoNodo = (Nodo*)Marshal.AllocHGlobal(sizeof(Nodo));

        nuevoNodo->Id = id;
        nuevoNodo->Repuesto = (char*)Marshal.StringToHGlobalAnsi(repuesto);
        nuevoNodo->Detalle = (char*)Marshal.StringToHGlobalAnsi(detalle);
        nuevoNodo->Costo = (char*)Marshal.StringToHGlobalAnsi(costo);
        nuevoNodo->Siguiente = primero;

        if (primero == null)
        {
            primero = nuevoNodo;
            ultimo = nuevoNodo;
            primero->Siguiente = primero;
            tamanio = 1;
        }
        else
        {
            primero->Siguiente = nuevoNodo;
            nuevoNodo->Siguiente = ultimo;
            primero = nuevoNodo;
            tamanio++;
        }
        

        primero = nuevoNodo;

        
    }
    }

    
    public Nodo* Buscar(int id)
    {
        Nodo* actual = primero;

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
        Nodo* actual = primero;
        Nodo* previo = null;

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
        Nodo* actual = cabeza;

        while (actual != null)
        {
            Console.WriteLine("Id: {0}, Repuesto: {1}, Detalle: {2}, Costo: {3}", actual->Id, new string(actual->Repuesto), new string(actual->Detalle), new string(actual->Costo));
            actual = actual->Siguiente;
        }
    } */
