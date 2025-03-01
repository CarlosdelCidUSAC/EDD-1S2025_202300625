using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

unsafe struct NodoUsuario
{
    public int* Id;
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

    public void Agregar(int id, string Nombre, string Apellido, string email, string pwd)
    {
        if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellido) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pwd))
        {
            throw new ArgumentException("Los campos no pueden ser nulos o vacíos.");
        }

        NodoUsuario* nuevoNodoUsuario = (NodoUsuario*)Marshal.AllocHGlobal(sizeof(NodoUsuario));
        nuevoNodoUsuario->Id = (int*)Marshal.AllocHGlobal(sizeof(int));
        *nuevoNodoUsuario->Id = id;
        nuevoNodoUsuario->Nombre = (char*)Marshal.StringToHGlobalAnsi(Nombre);
        nuevoNodoUsuario->Apellido = (char*)Marshal.StringToHGlobalAnsi(Apellido);
        nuevoNodoUsuario->Email = (char*)Marshal.StringToHGlobalAnsi(email);
        nuevoNodoUsuario->Pwd = (char*)Marshal.StringToHGlobalAnsi(pwd);
        nuevoNodoUsuario->Siguiente = null;

        if (cabeza == null)
        {
            cabeza = nuevoNodoUsuario;
        }
        else
        {
            NodoUsuario* actual = cabeza;
            while (actual->Siguiente != null)
            {
                actual = actual->Siguiente;
            }
            actual->Siguiente = nuevoNodoUsuario;
        }
    }

    
    public int Buscar(int id)
    {
        NodoUsuario* actual = cabeza;

        while (actual != null)
        {
            if (*actual->Id == id)
            {
                return *actual->Id;
            }
            actual = actual->Siguiente;
        }

        return -1; 
    }

    public string BuscarNombre(int id)
    {
        NodoUsuario* actual = cabeza;

        while (actual != null)
        {
            if (*actual->Id == id)
            {
                return Marshal.PtrToStringAnsi((IntPtr)actual->Nombre);
            }
            actual = actual->Siguiente;
        }

        return null;
    }

    public string BuscarApellido(int id)
    {
        NodoUsuario* actual = cabeza;

        while (actual != null)
        {
            if (*actual->Id == id)
            {
                return Marshal.PtrToStringAnsi((IntPtr)actual->Apellido);
            }
            actual = actual->Siguiente;
        }

        return null;
    }

    public string BuscarCorreo(int id)
    {
        NodoUsuario* actual = cabeza;

        while (actual != null)
        {
            if (*actual->Id == id)
            {
                return Marshal.PtrToStringAnsi((IntPtr)actual->Email);
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
            if (*actual->Id == id)
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

    public void Imprimir()
    {
        NodoUsuario* actual = cabeza;

        while (actual != null)
        {
            Console.WriteLine($"ID: {*actual->Id}");
            Console.WriteLine($"Nombre: {Marshal.PtrToStringAnsi((IntPtr)actual->Nombre)}");
            Console.WriteLine($"Apellido: {Marshal.PtrToStringAnsi((IntPtr)actual->Apellido)}");
            Console.WriteLine($"Email: {Marshal.PtrToStringAnsi((IntPtr)actual->Email)}");
            Console.WriteLine($"Pwd: {Marshal.PtrToStringAnsi((IntPtr)actual->Pwd)}");
            Console.WriteLine();
            actual = actual->Siguiente;
        }
    }

    public void Actualizar(int id, string Nombre, string Apellido, string email)
    {
        NodoUsuario* actual = cabeza;

        while (actual != null)
        {
            if (*actual->Id == id)
            {

                if (!string.IsNullOrEmpty(Nombre)|| Nombre != "")
                {
                    Marshal.FreeHGlobal((IntPtr)actual->Nombre);
                    actual->Nombre = (char*)Marshal.StringToHGlobalAnsi(Nombre);
                }
                if (!string.IsNullOrEmpty(Apellido) || Apellido != "")
                {
                    Marshal.FreeHGlobal((IntPtr)actual->Apellido);
                    actual->Apellido = (char*)Marshal.StringToHGlobalAnsi(Apellido);
                }
                if (!string.IsNullOrEmpty(email) || email != "")
                {
                    Marshal.FreeHGlobal((IntPtr)actual->Email);
                    actual->Email = (char*)Marshal.StringToHGlobalAnsi(email);
                }
            }

            actual = actual->Siguiente;
        }

        
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

    public void Graficar()
    {
        string rutaDot = "reportedot/lista_simple.dot";
        string rutaReporte = "reportes/lista_simple.png";
        string codigoDot = "digraph G {\n  rankdir=LR;\n  node [shape=record, height=.1];\n";
        codigoDot += "  label=\"Lista Simple\";\n  labelloc=top;\n  fontsize=20;\n";

        NodoUsuario* actual = cabeza;
        int contadorNodos = 0;

        while (actual != null)
        {
            string nombre = Marshal.PtrToStringAnsi((IntPtr)actual->Nombre);
            string apellido = Marshal.PtrToStringAnsi((IntPtr)actual->Apellido);
            string email = Marshal.PtrToStringAnsi((IntPtr)actual->Email);
            string pwd = Marshal.PtrToStringAnsi((IntPtr)actual->Pwd);

            codigoDot += $"node{contadorNodos} [label=\"{{ID: {*actual->Id}\\nNombre: {nombre}\\nApellido: {apellido}\\nEmail: {email}\\nPwd: {pwd}}}\"]\n";
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

