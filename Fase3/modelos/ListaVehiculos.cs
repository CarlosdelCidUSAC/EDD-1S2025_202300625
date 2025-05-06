using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Gtk;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text;


class NodoVehiculo {
    public int id;
    public int id_usuario;
    public string marca = string.Empty;
    public int anio;
    public string placa = string.Empty;
    public NodoVehiculo? siguiente;
    public NodoVehiculo? anterior;
}

class ListaVehiculos {

    private NodoVehiculo? cabeza;

    public NodoVehiculo? Cabeza => cabeza;

    public ListaVehiculos() {
        cabeza = null;
    }

    public bool AgregarPrimero(int id, int id_usuario, string marca, int anio, string placa) {
        if (Buscar(id) != null) {
            Console.WriteLine($"Error: Ya existe un vehículo con el ID {id}.");
            return false;
        }
        var nuevo = new NodoVehiculo {
            id = id,
            id_usuario = id_usuario,
            marca = marca,
            anio = anio,
            placa = placa,
            siguiente = cabeza,
            anterior = null
        };
        if (cabeza != null) {
            cabeza.anterior = nuevo;
        }
        cabeza = nuevo;
        return true;
    }

    public void Imprimir() {
        for (var actual = cabeza; actual != null; actual = actual.siguiente) {
            Console.WriteLine($"ID: {actual.id}");
            Console.WriteLine($"ID Usuario: {actual.id_usuario}");
            Console.WriteLine($"Marca: {actual.marca}");
            Console.WriteLine($"Modelo: {actual.anio}");
            Console.WriteLine($"Placa: {actual.placa}\n");
        }
    }

    public NodoVehiculo? Buscar(int id) {
        for (var actual = cabeza; actual != null; actual = actual.siguiente) {
            if (actual.id == id) return actual;
        }
        return null;
    }

    public void Eliminar(int id) {
        if (cabeza == null) return;
        if (cabeza.id == id) {
            cabeza = cabeza.siguiente;
            if (cabeza != null) cabeza.anterior = null;
            return;
        }
        var actual = cabeza;
        while (actual.siguiente != null) {
            if (actual.siguiente.id == id) {
                actual.siguiente = actual.siguiente.siguiente;
                if (actual.siguiente != null) actual.siguiente.anterior = actual;
                return;
            }
            actual = actual.siguiente;
        }
    }

    public ListStore mostrarTabla() {
        var modelo = new ListStore(typeof(int), typeof(int), typeof(string), typeof(int), typeof(string));
        for (var actual = cabeza; actual != null; actual = actual.siguiente) {
            modelo.AppendValues(actual.id, actual.id_usuario, actual.marca, actual.anio, actual.placa);
        }
        return modelo;
    }

    public void Graficar() {
        string codigodot = "digraph G {\nrankdir=LR;\nnode [shape=record];\nsubgraph cluster_ListaDobleEnlazada {\nlabel = \"Lista Doblemente Enlazada\";\n";
        for (var actual = cabeza; actual != null; actual = actual.siguiente) {
            string label = $"ID: {actual.id}\\nID Usuario: {actual.id_usuario}\\nMarca: {actual.marca}\\nModelo: {actual.anio}\\nPlaca: {actual.placa}";
            codigodot += $"\"{actual.id}\" [label=\"{label}\"];\n";
            if (actual.siguiente != null) {
                codigodot += $"\"{actual.id}\" -> \"{actual.siguiente.id}\";\n";
                codigodot += $"\"{actual.siguiente.id}\" -> \"{actual.id}\";\n";
            }
        }
        codigodot += "}\n}\n";

        string rutaDot = "reportedot/lista_doble.dot";
        string rutaReporte = "Reportes/lista_doble.png";
        var directory = Path.GetDirectoryName(rutaDot);
        if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);
        File.WriteAllText(rutaDot, codigodot);

        var proceso = new Process {
            StartInfo = new ProcessStartInfo {
                FileName = "dot",
                Arguments = $"-Tpng {rutaDot} -o {rutaReporte}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        proceso.Start();
        proceso.WaitForExit();

        if (File.Exists(rutaReporte)) {
            Console.WriteLine("Reporte generado con éxito");
            Process.Start(new ProcessStartInfo(rutaReporte) { UseShellExecute = true });
        } else {
            Console.WriteLine("Error al generar el reporte");
        }
    }

    public bool EstaVacia() => cabeza == null;

    public bool CrearBackup() {
        try {
            string backupDir = "Backup";
            Directory.CreateDirectory(backupDir);

            string vehiculosEddPath = Path.Combine(backupDir, "Vehiculos.edd");
            string treeFile = Path.Combine(backupDir, "huffman_vehiculo.json");
            string paddingFile = Path.Combine(backupDir, "huffman_vehiculo.padding");

            // Serializar la lista de vehículos a JSON
            var vehiculos = new List<VehiculoDTO>();
            for (var actual = cabeza; actual != null; actual = actual.siguiente) {
                vehiculos.Add(new VehiculoDTO {
                    id = actual.id,
                    id_usuario = actual.id_usuario,
                    marca = actual.marca,
                    anio = actual.anio,
                    placa = actual.placa
                });
            }
            string vehiculosData = JsonSerializer.Serialize(vehiculos, new JsonSerializerOptions { WriteIndented = true });
            byte[] datosBytes = Encoding.UTF8.GetBytes(vehiculosData);

            Console.WriteLine("JSON a comprimir:\n" + vehiculosData);
            Console.WriteLine($"Longitud JSON: {vehiculosData.Length} caracteres");

            // Usar la nueva implementación de Huffman
            var (vehiculosComprimido, vehiculosRaiz, padding) = CompresionHuffman.Comprimir(datosBytes);

            File.WriteAllBytes(vehiculosEddPath, vehiculosComprimido);
            File.WriteAllText(treeFile, JsonSerializer.Serialize(vehiculosRaiz));
            File.WriteAllText(paddingFile, padding.ToString());

            Console.WriteLine($"Backup comprimido de vehículos generado en: {vehiculosEddPath}");
            return true;
        } catch (Exception ex) {
            Console.WriteLine($"Error al crear el backup de vehículos: {ex.Message}");
            return false;
        }
    }



    // Método para limpiar la lista antes de restaurar
    public void Limpiar()
    {
        cabeza = null;
    }

    public void Restaurar() {
        string backupDir = "Backup";
        string vehiculosEddPath = Path.Combine(backupDir, "Vehiculos.edd");
        string treeFile = Path.Combine(backupDir, "huffman_vehiculo.json");
        string paddingFile = Path.Combine(backupDir, "huffman_vehiculo.padding");
       
        if (!File.Exists(vehiculosEddPath) || !File.Exists(treeFile) || !File.Exists(paddingFile)) {
            Console.WriteLine("Error: No se encontró el archivo de backup de vehículos.");
            return;
        }
        try {

            byte[] vehiculosComprimido = File.ReadAllBytes(vehiculosEddPath);
            var vehiculosRaiz = JsonSerializer.Deserialize<NodoHuffman>(File.ReadAllText(treeFile));
            int padding = int.Parse(File.ReadAllText(paddingFile));

            byte[] datosDescomprimidos = CompresionHuffman.Descomprimir(vehiculosComprimido, vehiculosRaiz, padding);
            string vehiculosDescomprimido = Encoding.UTF8.GetString(datosDescomprimidos);

            // Descomprimir el JSON

            NodoHuffman raiz = JsonSerializer.Deserialize<NodoHuffman>(File.ReadAllText(treeFile));
            byte[] comprimido = File.ReadAllBytes(vehiculosEddPath);
            string jsonDescomprimido = Encoding.UTF8.GetString(CompresionHuffman.Descomprimir(comprimido, raiz, padding));

            var vehiculos = JsonSerializer.Deserialize<List<VehiculoDTO>>(jsonDescomprimido);
            if (vehiculos == null) {
                Console.WriteLine("Error al deserializar los vehículos.");
                return;
            }

            foreach (var vehiculo in vehiculos) {
                AgregarPrimero(vehiculo.id, vehiculo.id_usuario, vehiculo.marca, vehiculo.anio, vehiculo.placa);
            }
            Console.WriteLine("Vehículos restaurados con éxito.");
            Imprimir();
        } catch (Exception ex) {
            Console.WriteLine($"Error al restaurar vehículos: {ex.Message}");
        }
    }

    private class VehiculoDTO {
        public int id { get; set; }
        public int id_usuario { get; set; }
        public string marca { get; set; } = string.Empty;
        public int anio { get; set; }
        public string placa { get; set; } = string.Empty;
    }
}
