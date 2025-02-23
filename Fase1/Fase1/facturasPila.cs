using System;
using System.Runtime.InteropServices;

unsafe struct NodoFactura
{
    public int Id;
    public int Id_Servicio;
    public int Id_Vehiculo;
    public char* Detalles;
    public char* Costo;
    public NodoFactura* Siguiente;
}

unsafe class FacturasPila
{
    private NodoFactura* cabeza;


    public void enpilar(int id, int id_Servicio, int id_vehiculo, string detalles, string costo)
    {
        if (string.IsNullOrEmpty(detalles) || string.IsNullOrEmpty(costo))
        {
            throw new ArgumentException("Los campos no pueden ser nulos o vacíos.");
        }

        NodoFactura* nuevoNodoFactura = (NodoFactura*)Marshal.AllocHGlobal(sizeof(NodoFactura));

        if (cabeza == null)
        {
            nuevoNodoFactura->Id = id;
            nuevoNodoFactura->Id_Servicio = id_Servicio;
            nuevoNodoFactura->Id_Vehiculo = id_vehiculo;
            nuevoNodoFactura->Detalles = (char*)Marshal.StringToHGlobalAnsi(detalles);
            nuevoNodoFactura->Costo = (char*)Marshal.StringToHGlobalAnsi(costo);
            nuevoNodoFactura->Siguiente = null;
            cabeza = nuevoNodoFactura;
        }
        else
        {
            nuevoNodoFactura->Id = id;
            nuevoNodoFactura->Id_Servicio = id_Servicio;
            nuevoNodoFactura->Id_Vehiculo = id_vehiculo;
            nuevoNodoFactura->Detalles = (char*)Marshal.StringToHGlobalAnsi(detalles);
            nuevoNodoFactura->Costo = (char*)Marshal.StringToHGlobalAnsi(costo);
            nuevoNodoFactura->Siguiente = cabeza;
            cabeza = nuevoNodoFactura;
        }

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
        Marshal.FreeHGlobal((IntPtr)actual->Costo);
        Marshal.FreeHGlobal((IntPtr)actual);

        return actual;

    }

    }

