using System;
using System.Runtime.InteropServices;

unsafe struct NodoServicio
{
    public int Id;
    public int Id_Servicio;
    public int Id_Vehiculo;
    public char* Detalles;
    public char* Costo;
    public NodoServicio* Siguiente;
}

unsafe class SeviciosCola
{
    private NodoServicio* cabeza;
    private NodoServicio* cola;
    public void Encolar(int id, int Id_Servicio, int Id_Vehiculo, string Detalles, string Costo)
    {
        if (string.IsNullOrEmpty(Detalles) || string.IsNullOrEmpty(Costo))
        {
            throw new ArgumentException("Los campos no pueden ser nulos o vacíos.");
        }

        NodoServicio* nuevoNodoServicio = (NodoServicio*)Marshal.AllocHGlobal(sizeof(NodoServicio));

        if (cabeza == null && cola == null)
        {
            cabeza = nuevoNodoServicio;
            cabeza->Id = id;
            cabeza->Id_Servicio = Id_Servicio;
            cabeza->Id_Vehiculo = Id_Vehiculo;
            cabeza->Detalles = (char*)Marshal.StringToHGlobalAnsi(Detalles);
            cabeza->Costo = (char*)Marshal.StringToHGlobalAnsi(Costo);
            cola = cabeza;
            return;
        }
        else
        {
            nuevoNodoServicio->Id = id;
            nuevoNodoServicio->Id_Servicio = Id_Servicio;
            cnuevoNodoServicio->Id_Vehiculo = Id_Vehiculo;
            nuevoNodoServicio->Detalles = (char*)Marshal.StringToHGlobalAnsi(Detalles);
            nuevoNodoServicio->Costo = (char*)Marshal.StringToHGlobalAnsi(Costo);
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
            Marshal.FreeHGlobal((IntPtr)actual->Costo);
            Marshal.FreeHGlobal((IntPtr)actual);
            actual = siguiente;
        }

        cabeza = null;
        cola = null;
    }
}