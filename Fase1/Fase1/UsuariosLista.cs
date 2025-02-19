using System;
using System.Runtime.InteropServices;

unsafe struct Nodo
{
    public int Id;
    public char* Nombre;
    public char* Apellido;
    public char* Email;
    public char* Pwd;
    public Nodo* Siguiente;
}

unsafe class UsuariosLista
{
    private Nodo* cabeza;

    public void agregarPrimero(int id, string Nombre, string Apellido, string email, string pwd)
    {
        if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellido) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pwd))
        {
            throw new ArgumentException("Los campos no pueden ser nulos o vacíos.");
        }

        Nodo* nuevoNodo = (Nodo*)Marshal.AllocHGlobal(sizeof(Nodo));

        nuevoNodo->Id = id;
        nuevoNodo->Nombre = (char*)Marshal.StringToHGlobalAnsi(Nombre);
        nuevoNodo->Apellido = (char*)Marshal.StringToHGlobalAnsi(Apellido);
        nuevoNodo->Email = (char*)Marshal.StringToHGlobalAnsi(email);
        nuevoNodo->Pwd = (char*)Marshal.StringToHGlobalAnsi(pwd);
        nuevoNodo->Siguiente = cabeza;

        cabeza = nuevoNodo;
    }

    
    public Nodo* Buscar(int id)
    {
        Nodo* actual = cabeza;

        while (actual != null)
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
        Nodo* actual = cabeza;
        Nodo* previo = null;

        while (actual != null)
        {
            if (actual->Id == id)
            {
                Marshal.FreeHGlobal((IntPtr)actual->Nombre);
                Marshal.FreeHGlobal((IntPtr)actual->Apellido);
                Marshal.FreeHGlobal((IntPtr)actual->Email);
                Marshal.FreeHGlobal((IntPtr)actual->Pwd);

                if (previo == null)
                {
                    cabeza = actual->Siguiente;
                }
                else
                {
                    previo->Siguiente = actual->Siguiente;
                }

                Marshal.FreeHGlobal((IntPtr)actual);
                return;
            }

            previo = actual;
            actual = actual->Siguiente;
        }

        throw new InvalidOperationException($"Nodo con ID {id} no encontrado.");
    }

    public void LiberarLista()
    {
        Nodo* actual = cabeza;

        while (actual != null)
        {
            Nodo* Siguiente = actual->Siguiente;

            Marshal.FreeHGlobal((IntPtr)actual->Nombre);
            Marshal.FreeHGlobal((IntPtr)actual->Apellido);
            Marshal.FreeHGlobal((IntPtr)actual->Email);
            Marshal.FreeHGlobal((IntPtr)actual->Pwd);

            Marshal.FreeHGlobal((IntPtr)actual);

            actual = Siguiente;
        }

        cabeza = null;
    }

}

