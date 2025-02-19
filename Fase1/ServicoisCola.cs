using System;
using System.Runtime.InteropServices;

unsafe struct Nodo
{
    public int Id;
    public int Id_Servicio;
    public int Id_Vehiculo;
    public char* Detalles;
    public char* Costo;
    public Nodo* Siguiente;
}

unsafe class SeviciosCola
{
    private Nodo* cabeza;
    private Nodo* cola;
    public void Encolar(int id, int Id_Servicio, int Id_Vehiculo, string Detalles, string Costo)
    {
        if (string.IsNullOrEmpty(Detalles) || string.IsNullOrEmpty(Costo))
        {
            throw new ArgumentException("Los campos no pueden ser nulos o vacíos.");
        }

        Nodo* nuevoNodo = (Nodo*)Marshal.AllocHGlobal(sizeof(Nodo));

        nuevoNodo->Id = id;
        nuevoNodo->Id_Servicio = Id_Servicio;
        nuevoNodo->Id_Vehiculo = Id_Vehiculo;
        nuevoNodo->Detalles = (char*)Marshal.StringToHGlobalAnsi(Detalles);
        nuevoNodo->Costo = (char*)Marshal.StringToHGlobalAnsi(Costo);
        nuevoNodo->Siguiente = null;

        if (cabeza == null)
        {
            cabeza = nuevoNodo;
            cola = nuevoNodo;
        }
        else
        {
            cola->Siguiente = nuevoNodo;
            cola = nuevoNodo;
        }
    }

    public Nodo* Desencolar()
    {
        if (cabeza == null)
        {

            return null;
        }

        Nodo* actual = cabeza;
        cabeza = cabeza->Siguiente;


        return actual;
    }

    public Nodo* LiberarCola()
    {
        Nodo* actual = cabeza;
        Nodo* siguiente = null;

        while (actual != null)
        {
            siguiente = actual->Siguiente;
            Marshal.FreeHGlobal((IntPtr)actual->Detalles);
            Marshal.FreeHGlobal((IntPtr)actual->Costo);
            Marshal.FreeHGlobal((IntPtr)actual);
            actual = siguiente;
        }

        cabeza = null;
        cola = null;
    }
}