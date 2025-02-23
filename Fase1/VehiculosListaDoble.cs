using System;
using System.Runtime.InteropServices;

unsafe struct NodoVehiculo
{
    public int Id;
    public int Id_Usuario;
    public char* Marca;
    public char* Modelo;
    public char* Placa;
    public NodoVehiculo* Siguiente;
    public NodoVehiculo* Anterior;  
}

unsafe class VehiculoListaDoble
{

    private NodoVehiculo* cabeza;
    private NodoVehiculo* cola

    VehiculoListaDoble()
    {
        cabeza = null;
        cola = null;
    }
    
    public void agregarPrimero(int id, int Id_Usuario, string Marca, string Modelo, string Placa)
    {
        if (string.IsNullOrEmpty(Marca) || string.IsNullOrEmpty(Modelo) || string.IsNullOrEmpty(Placa))
        {
            throw new ArgumentException("Los campos no pueden ser nulos o vacíos.");
        }

        NodoVehiculo* nuevoNodoVehiculo = (NodoVehiculo*)Marshal.AllocHGlobal(sizeof(NodoVehiculo));

        if (cabeza == null)
        {
            cabeza = nuevoNodoVehiculo;
            cabeza->Id = id;
            cabeza->Id_Usuario = Id_Usuario;
            cabeza->Marca = (char*)Marshal.StringToHGlobalAnsi(Marca);
            cabeza->Modelo = (char*)Marshal.StringToHGlobalAnsi(Modelo);
            cabeza->Placa = (char*)Marshal.StringToHGlobalAnsi(Placa);
            cola = cabeza;

            return;
        }
        else
        {
            nuevoNodoVehiculo->Id = id;
            nuevoNodoVehiculo->Id_Usuario = Id_Usuario;
            nuevoNodoVehiculo->Marca = (char*)Marshal.StringToHGlobalAnsi(Marca);
            nuevoNodoVehiculo->Modelo = (char*)Marshal.StringToHGlobalAnsi(Modelo);
            cola->Siguiente = nuevoNodoVehiculo;
            nuevoNodoVehiculo->Anterior = cola;
            cola = nuevoNodoVehiculo;

        }

    }

    public void Borrar(int id)
    {

        NodoVehiculo* actual = cabeza;
        while(actual != null)
        {
            if (actual->Id == id)
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
                Marshal.FreeHGlobal((IntPtr)actual->Modelo);
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
            Marshal.FreeHGlobal((IntPtr)temp->Modelo);
            Marshal.FreeHGlobal((IntPtr)temp->Placa);
            Marshal.FreeHGlobal((IntPtr)temp);
        }
        cabeza = null;
        cola = null;
    }

}