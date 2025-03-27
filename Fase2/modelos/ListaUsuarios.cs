using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
class NodoUsuario {
    public int id { get; set; }
    public string nombre { get; set; }
    public string apellido { get; set; }
    public string correo { get; set; }
    public int edad { get; set; }
    public string contrasenia { get; set; }
    public NodoUsuario? siguiente { get; set; }
};

class listaUsuarios {

    private NodoUsuario? cabeza;

    public listaUsuarios() {
        cabeza = null;
    }

    public void Agregar(int id, string nombre, string apellido, string correo, int edad, string contrasenia) {
        NodoUsuario actual = cabeza;
        while (actual != null) {
            if (actual.id == id) {
                Console.WriteLine("Error: El ID ya ha sido ingresado.");
                return;
            }
            if (actual.correo == correo) {
                Console.WriteLine("Error: El correo ya ha sido ingresado.");
                return;
            }
            actual = actual.siguiente;
        }

        NodoUsuario nuevo = new NodoUsuario();
        nuevo.id = id;
        nuevo.nombre = nombre;
        nuevo.apellido = apellido;
        nuevo.correo = correo;
        nuevo.edad = edad;
        nuevo.contrasenia = contrasenia;
        nuevo.siguiente = null;

        if (cabeza == null) {
            cabeza = nuevo;
        } else {
            actual = cabeza;
            while (actual.siguiente != null) {
                actual = actual.siguiente;
            }
            actual.siguiente = nuevo;
        }
    }

    public void Eliminar(int id) {
        if (cabeza == null) {
            return;
        }
        if (cabeza.id == id) {
            cabeza = cabeza.siguiente;
            return;
        }
        NodoUsuario actual = cabeza;
        while (actual.siguiente != null) {
            if (actual.siguiente.id == id) {
                actual.siguiente = actual.siguiente.siguiente;
                return;
            }
            actual = actual.siguiente;
        }
    }

    public NodoUsuario? Buscar(int id) {
        NodoUsuario actual = cabeza;
        while (actual != null) {
            if (actual.id == id) {
                return actual;
            }
            actual = actual.siguiente;
        }
        return null;
    }

    public int ObtenerId(string correo) {
        NodoUsuario actual = cabeza;
        while (actual != null) {
            if (actual.correo == correo) {
                return actual.id;
            }
            actual = actual.siguiente;
        }
        return -1;
    }

    public bool ValidarLogin(string correo, string contrasenia) {
        NodoUsuario actual = cabeza;
        while (actual != null) {
            if (actual.correo == correo && actual.contrasenia == contrasenia) {
                return true;
            }
            actual = actual.siguiente;
        }
        return false;
    }

    public void Imprimir() {
        NodoUsuario actual = cabeza;
        while (actual != null) {
            Console.WriteLine("ID: " + actual.id);
            Console.WriteLine("Nombre: " + actual.nombre);
            Console.WriteLine("Apellido: " + actual.apellido);
            Console.WriteLine("Correo: " + actual.correo);
            Console.WriteLine("Edad: " + actual.edad);
            Console.WriteLine("Contrasenia: " + actual.contrasenia);
            Console.WriteLine();
            actual = actual.siguiente;
        }
    }

    public void Graficar()
    {
        string rutaDot = "reportedot/lista_simple.dot";
        string rutaReporte = "reportes/lista_simple.png";
        string codigoDot = "digraph G {\n  rankdir=LR;\n  node [shape=record, height=.1];\n";
        codigoDot += "  label=\"Lista Simple\";\n  labelloc=top;\n  fontsize=20;\n";

        NodoUsuario actual = cabeza;
        int contadorNodos = 0;

        while (actual != null)
        {
            string nombre = actual.nombre;
            string apellido = actual.apellido;
            string email = actual.correo;
            string edad = actual.edad.ToString();

            codigoDot += $"node{contadorNodos} [label=\"{{ID: {actual.id}\\nNombre: {nombre}\\nApellido: {apellido}\\nEmail: {email}\\nPwd: {edad}}}\"]\n";
            contadorNodos++;
            actual = actual.siguiente;
        }

        actual = cabeza;
        contadorNodos = 0;

        while (actual != null && actual.siguiente != null)
        {
            codigoDot += $"node{contadorNodos} . node{contadorNodos + 1};\n";
            contadorNodos++;
            actual = actual.siguiente;
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

    public bool EstaVacia() {
        return cabeza == null;
    }


}