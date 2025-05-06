using Gtk;
using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Runtime.InteropServices;
using Gtk;

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

    public AVLRepuestos() {
        raiz = null;
    }
    

    public NodoRepuesto? Raiz {
        get { return raiz; }
    }
    public void insertar(int id, string Repuesto, int idVehiculo, string detalle, float costo) {
        if (Buscar(raiz, id) != null) {
            Console.WriteLine($"Error: El ID {id} ya ha sido ingresado.");
            Application.Init();
            MessageDialog dialog = new MessageDialog(
                null,
                DialogFlags.Modal,
                MessageType.Error,
                ButtonsType.Ok,
                $"El ID {id} ya ha sido ingresado."
            );
            dialog.Run();
            dialog.Destroy();
            Application.Quit();

            return;
        }
        raiz = insertarRecursivo(raiz, id, Repuesto, idVehiculo, detalle, costo);
    }

    public NodoRepuesto? Buscar(NodoRepuesto? nodo, int id) {
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
        if (k2.Izquierda == null)
            throw new InvalidOperationException("No se puede realizar la rotación: el hijo izquierdo es nulo.");
        NodoRepuesto k1 = k2.Izquierda;
        k2.Izquierda = k1.Derecha;
        k1.Derecha = k2;
        k2.peso = Maximo(ObtenerPeso(k2.Izquierda), ObtenerPeso(k2.Derecha)) + 1;
        k1.peso = Maximo(ObtenerPeso(k1.Izquierda), k2.peso) + 1;
        return k1;
    }

    public NodoRepuesto RotacionSimpleConHijoDerecho(NodoRepuesto k1){
        if (k1.Derecha == null)
            throw new InvalidOperationException("No se puede realizar la rotación: el hijo derecho es nulo.");
        NodoRepuesto k2 = k1.Derecha;
        k1.Derecha = k2.Izquierda;
        k2.Izquierda = k1;
        k1.peso = Maximo(ObtenerPeso(k1.Izquierda), ObtenerPeso(k1.Derecha)) + 1;
        k2.peso = Maximo(ObtenerPeso(k2.Derecha), k1.peso) + 1;
        return k2;
    }

    public NodoRepuesto RotacionDobleConHijoIzquierdo(NodoRepuesto k3){
        if (k3.Izquierda == null)
            throw new InvalidOperationException("No se puede realizar la rotación doble: el hijo izquierdo es nulo.");
        k3.Izquierda = RotacionSimpleConHijoDerecho(k3.Izquierda);
        return RotacionSimpleConHijoIzquierdo(k3);
    }

    public NodoRepuesto RotacionDobleConHijoDerecho(NodoRepuesto k1){
        if (k1.Derecha == null)
            throw new InvalidOperationException("No se puede realizar la rotación doble: el hijo derecho es nulo.");
        k1.Derecha = RotacionSimpleConHijoIzquierdo(k1.Derecha);
        return RotacionSimpleConHijoDerecho(k1);
    }

    public ListStore recorrerPreorden(NodoRepuesto nodo, ListStore modelo){
        if (nodo != null) {
            modelo.AppendValues(nodo.id, nodo.Repuesto, nodo.detalle, nodo.costo);
            recorrerPreorden(nodo.Izquierda!, modelo);
            recorrerPreorden(nodo.Derecha!, modelo);
        }
        return modelo;
    }

    public ListStore recorrerInorden(NodoRepuesto nodo, ListStore modelo){
        if (nodo != null) {
            recorrerInorden(nodo.Izquierda!, modelo);
            modelo.AppendValues(nodo.id, nodo.Repuesto, nodo.detalle, nodo.costo);
            recorrerInorden(nodo.Derecha!, modelo);
        }
        return modelo;
    }

    public ListStore recorrerPostorden(NodoRepuesto nodo, ListStore modelo){
        if (nodo != null) {
            recorrerPostorden(nodo.Izquierda!, modelo);
            recorrerPostorden(nodo.Derecha!, modelo);
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
            if (nodo.Izquierda != null)
                ImprimirRecursivo(nodo.Izquierda);
            Console.WriteLine($"ID: {nodo.id}, Repuesto: {nodo.Repuesto}, Detalle: {nodo.detalle}, Costo: {nodo.costo}");
            if (nodo.Derecha != null)
                ImprimirRecursivo(nodo.Derecha);
        }
    }

    public void CrearBackup() {
        string backupDir = "Backup";
        Directory.CreateDirectory(backupDir);

        string repuestosEddPath = Path.Combine(backupDir, "Repuestos.edd");
        string treeFileRepuestos = Path.Combine(backupDir, "huffman_repuesto.json");
        string paddingFile = Path.Combine(backupDir, "huffman_repuesto.padding");

        // Serializar todos los repuestos a texto plano (JSON)
        var lista = new List<object>();
        void AgregarRepuestos(NodoRepuesto? nodo) {
            if (nodo == null) return;
            lista.Add(new {
                id = nodo.id,
                Repuesto = nodo.Repuesto,
                detalle = nodo.detalle,
                costo = nodo.costo
            });
            AgregarRepuestos(nodo.Izquierda);
            AgregarRepuestos(nodo.Derecha);
        }
        AgregarRepuestos(raiz);
        string repuestosData = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
        byte[] datosBytes = Encoding.UTF8.GetBytes(repuestosData);

        // Comprimir usando Huffman
        var (repuestosComprimido, repuestosRaiz, padding) = CompresionHuffman.Comprimir(datosBytes);

        File.WriteAllBytes(repuestosEddPath, repuestosComprimido);
        File.WriteAllText(treeFileRepuestos, JsonSerializer.Serialize(repuestosRaiz));
        File.WriteAllText(paddingFile, padding.ToString());

        Console.WriteLine($"Backup comprimido de repuestos generado en: {repuestosEddPath}");
    }

    public void Restaurar() {
        string backupDir = "Backup";
        string repuestosEddPath = Path.Combine(backupDir, "Repuestos.edd");
        string treeFileRepuestos = Path.Combine(backupDir, "huffman_repuesto.json");
        string paddingFile = Path.Combine(backupDir, "huffman_repuesto.padding");

        if (!File.Exists(repuestosEddPath) || !File.Exists(treeFileRepuestos) || !File.Exists(paddingFile)) {
            Console.WriteLine("No se encontró el archivo de respaldo.");
            return;
        }

        byte[] datosComprimidos = File.ReadAllBytes(repuestosEddPath);
        string treeJson = File.ReadAllText(treeFileRepuestos);
        int padding = int.Parse(File.ReadAllText(paddingFile));

        // Descomprimir usando Huffman
        NodoHuffman raizHuffman = JsonSerializer.Deserialize<NodoHuffman>(treeJson);
        byte[] datosDescomprimidos = CompresionHuffman.Descomprimir(datosComprimidos, raizHuffman, padding);
        string repuestosData = Encoding.UTF8.GetString(datosDescomprimidos);
        var lista = JsonSerializer.Deserialize<List<NodoRepuesto>>(repuestosData);
        if (lista == null) {
            Console.WriteLine("Error al deserializar los datos de repuestos.");
            return;
        }

        foreach (var repuesto in lista) {
            insertar(repuesto.id, repuesto.Repuesto, 0, repuesto.detalle, repuesto.costo);
        }
        Console.WriteLine("Restauración de repuestos completada.");

    }
}

