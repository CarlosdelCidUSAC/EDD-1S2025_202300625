using Gtk;
using System;
using System.IO;
using System.Diagnostics;
using System.Text;
class NodoRepuesto {
    public int id { get; set; }
    public string Repuesto { get; set; }
    public string detalle { get; set; }
    public float costo { get; set; }
    public NodoRepuesto? Izquierda { get; set; }
    public NodoRepuesto? Derecha { get; set; }
    public int peso { get; set; }

    public NodoRepuesto(int id, string Repuesto, string detalle, float costo) {
        this.id = id;
        this.Repuesto = Repuesto;
        this.detalle = detalle;
        this.costo = costo;
        Izquierda = null;
        Derecha = null;
        peso = 0;
    }
}

class AVLRepuestos {

    private NodoRepuesto? raiz;
    string conexiones = "";
    string nodos = "";

    public AVLRepuestos() {
        raiz = null;
    }

    public NodoRepuesto? Raiz {
        get { return raiz; }
    }
    public void insertar(int id, string Repuesto, int idVehiculo, string detalle, float costo) {
        if (Buscar(raiz, id) != null) {
            Console.WriteLine($"Error: El ID {id} ya ha sido ingresado.");
            return;
        }
        raiz = insertarRecursivo(raiz, id, Repuesto, idVehiculo, detalle, costo);
    }

    private NodoRepuesto? Buscar(NodoRepuesto? nodo, int id) {
        if (nodo == null) {
            return null;
        }
        if (id == nodo.id) {
            return nodo;
        }
        if (id < nodo.id) {
            return Buscar(nodo.Izquierda, id);
        } else {
            return Buscar(nodo.Derecha, id);
        }
    }

    public int ObtenerPeso(NodoRepuesto? nodo) {
        if (nodo == null) {
            return -1;
        }
        return nodo.peso;
    }

    public int Maximo(int a, int b) {
        return a > b ? a : b;
    }

    public NodoRepuesto insertarRecursivo(NodoRepuesto? nodo, int id, string Repuesto, int idVehiculo, string detalle, float costo) {
    
        if (nodo == null){
            nodo = new NodoRepuesto(id, Repuesto, detalle, costo);
        }
        else if (id < nodo.id){
            nodo.Izquierda = insertarRecursivo(nodo.Izquierda, id , Repuesto, idVehiculo, detalle, costo);

            if(ObtenerPeso(nodo.Izquierda) - ObtenerPeso(nodo.Derecha)==2){
                if(id < nodo.Izquierda.id){
                    nodo = RotacionSimpleConHijoIzquierdo(nodo);
                }
                else{
                    nodo = RotacionDobleConHijoIzquierdo(nodo);
                }                                              

            }

        }

        else if (id > nodo.id){
            nodo.Derecha = insertarRecursivo(nodo.Derecha, id, Repuesto, idVehiculo, detalle, costo);

            if(ObtenerPeso(nodo.Derecha) - ObtenerPeso(nodo.Izquierda)==2){
                if(id > nodo.Derecha.id){
                    nodo = RotacionSimpleConHijoDerecho(nodo);
                }
                else{
                    nodo = RotacionDobleConHijoDerecho(nodo);
                }                                              

            }
        }
        else {
            Console.WriteLine("Duplicado");
        }

        nodo.peso = Maximo(ObtenerPeso(nodo.Izquierda), ObtenerPeso(nodo.Derecha)) + 1;
        return nodo;
    }

    public NodoRepuesto RotacionSimpleConHijoIzquierdo(NodoRepuesto k2){
        NodoRepuesto k1 = k2.Izquierda;
        k2.Izquierda = k1.Derecha;
        k1.Derecha = k2;
        k2.peso = Maximo(ObtenerPeso(k2.Izquierda), ObtenerPeso(k2.Derecha)) + 1;
        k1.peso = Maximo(ObtenerPeso(k1.Izquierda), k2.peso) + 1;
        return k1;
    }

    public NodoRepuesto RotacionSimpleConHijoDerecho(NodoRepuesto k1){
        NodoRepuesto k2 = k1.Derecha;
        k1.Derecha = k2.Izquierda;
        k2.Izquierda = k1;
        k1.peso = Maximo(ObtenerPeso(k1.Izquierda), ObtenerPeso(k1.Derecha)) + 1;
        k2.peso = Maximo(ObtenerPeso(k2.Derecha), k1.peso) + 1;
        return k2;
    }

    public NodoRepuesto RotacionDobleConHijoIzquierdo(NodoRepuesto k3){
        k3.Izquierda = RotacionSimpleConHijoDerecho(k3.Izquierda);
        return RotacionSimpleConHijoIzquierdo(k3);
    }

    public NodoRepuesto RotacionDobleConHijoDerecho(NodoRepuesto k1){
        k1.Derecha = RotacionSimpleConHijoIzquierdo(k1.Derecha);
        return RotacionSimpleConHijoDerecho(k1);
    }

    public ListStore recorrerPreorden(NodoRepuesto nodo, ListStore modelo){
        if (nodo != null) {
            modelo.AppendValues(nodo.id, nodo.Repuesto, nodo.detalle, nodo.costo);
            recorrerPreorden(nodo.Izquierda, modelo);
            recorrerPreorden(nodo.Derecha, modelo);
        }
        return modelo;
    }

    public ListStore recorrerInorden(NodoRepuesto nodo, ListStore modelo){
        if (nodo != null) {
            recorrerInorden(nodo.Izquierda, modelo);
            modelo.AppendValues(nodo.id, nodo.Repuesto, nodo.detalle, nodo.costo);
            recorrerInorden(nodo.Derecha, modelo);
        }
        return modelo;
    }

    public ListStore recorrerPostorden(NodoRepuesto nodo, ListStore modelo){
        if (nodo != null) {
            recorrerPostorden(nodo.Izquierda, modelo);
            recorrerPostorden(nodo.Derecha, modelo);
            modelo.AppendValues(nodo.id, nodo.Repuesto, nodo.detalle, nodo.costo);
        }
        return modelo;
    }

        public void Graficar()
    {
        StringBuilder dot = new StringBuilder();
        dot.AppendLine("digraph G {");
        dot.AppendLine("node [shape=record, fontsize=10];");

        if (raiz != null)
        {
            GraficarRecursivo(raiz, dot);
        }

        dot.AppendLine("}");
        string rutaDot = "reportedot/AVL.dot";
        string rutaReporte = "Reportes/AVL.png";
        File.WriteAllText(rutaDot, dot.ToString());
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

    private void GraficarRecursivo(NodoRepuesto nodo, StringBuilder dot)
    {
        if (nodo != null)
        {
            string etiquetaNodo = $"\"ID: {nodo.id} \\n Repuesto: {nodo.Repuesto} \\n Detalle: {nodo.detalle} \\n Costo: {nodo.costo}\"";
            if (nodo.Izquierda != null)
            {
                string etiquetaIzquierda = $"\"ID: {nodo.Izquierda.id} \\n Repuesto: {nodo.Izquierda.Repuesto} \\n Detalle: {nodo.Izquierda.detalle} \\n Costo: {nodo.Izquierda.costo}\"";
                dot.AppendLine($"{etiquetaNodo} -> {etiquetaIzquierda};");
                GraficarRecursivo(nodo.Izquierda, dot);
            }
            if (nodo.Derecha != null)
            {
                string etiquetaDerecha = $"\"ID: {nodo.Derecha.id} \\n Repuesto: {nodo.Derecha.Repuesto} \\n Detalle: {nodo.Derecha.detalle} \\n Costo: {nodo.Derecha.costo}\"";
                dot.AppendLine($"{etiquetaNodo} -> {etiquetaDerecha};");
                GraficarRecursivo(nodo.Derecha, dot);
            }
        }
    }

        public bool EstaVacio() {
            return raiz == null;
        }

    public void Imprimir() {
        if (raiz == null) {
            Console.WriteLine("El árbol está vacío.");
        } else {
            ImprimirRecursivo(raiz);
        }
    
    }
    public void ImprimirRecursivo(NodoRepuesto nodo) {
        if (nodo != null) {
            ImprimirRecursivo(nodo.Izquierda);
            Console.WriteLine($"ID: {nodo.id}, Repuesto: {nodo.Repuesto}, Detalle: {nodo.detalle}, Costo: {nodo.costo}");
            ImprimirRecursivo(nodo.Derecha);
        }
    }
}
