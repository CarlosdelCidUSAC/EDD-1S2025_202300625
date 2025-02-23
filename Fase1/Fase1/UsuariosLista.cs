using System;
using System.Runtime.InteropServices;

unsafe struct NodoUsuario
{
    public int Id;
    public char* Nombre;
    public char* Apellido;
    public char* Email;
    public char* Pwd;
    public NodoUsuario* Siguiente;
}

unsafe class UsuariosLista
{
    private NodoUsuario* cabeza;

    public UsuariosLista()
    {
        cabeza = null;
    }

    public void agregarPrimero(int id, string Nombre, string Apellido, string email, string pwd)
    {
        if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellido) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pwd))
        {
            throw new ArgumentException("Los campos no pueden ser nulos o vacíos.");
        }

        NodoUsuario* nuevoNodoUsuario = (NodoUsuario*)Marshal.AllocHGlobal(sizeof(NodoUsuario));

        if (cabeza == null)
        {
            cabeza = nuevoNodoUsuario;
            cabeza->Id = id;
            cabeza->Nombre = (char*)Marshal.StringToHGlobalAnsi(Nombre);
            cabeza->Apellido = (char*)Marshal.StringToHGlobalAnsi(Apellido);
            cabeza->Email = (char*)Marshal.StringToHGlobalAnsi(email);
            cabeza->Pwd = (char*)Marshal.StringToHGlobalAnsi(pwd);
            cabeza->Siguiente = null;
            return;
        }
        else {
            NodoUsuario* actual = cabeza;
            while (actual->Siguiente != null)
            {
                actual = actual->Siguiente;
            }
            nuevoNodoUsuario->Id = id;
            nuevoNodoUsuario->Nombre = (char*)Marshal.StringToHGlobalAnsi(Nombre);
            nuevoNodoUsuario->Apellido = (char*)Marshal.StringToHGlobalAnsi(Apellido);
            nuevoNodoUsuario->Email = (char*)Marshal.StringToHGlobalAnsi(email);
            nuevoNodoUsuario->Pwd = (char*)Marshal.StringToHGlobalAnsi(pwd);
            nuevoNodoUsuario->Siguiente = null;
            actual->Siguiente = nuevoNodoUsuario;


        }

        
    }

    
    public NodoUsuario* Buscar(int id)
    {
        NodoUsuario* actual = cabeza;

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
        NodoUsuario* actual = cabeza;
        NodoUsuario* previo = null;

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

        throw new InvalidOperationException($"NodoUsuario con ID {id} no encontrado.");
    }

    public void LiberarLista()
    {
        NodoUsuario* actual = cabeza;

        while (actual != null)
        {
            NodoUsuario* Siguiente = actual->Siguiente;

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

