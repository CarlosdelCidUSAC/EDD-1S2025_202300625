using Gtk;
using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
class NodoServicio {
    public int Id { get; set; }
    public int IdRepuesto { get; set; }
    public int IdVehiculo { get; set; }
    public string Detalle { get; set; }
    public float Costo { get; set; }
    public string MetodoPago { get; set; }
    public NodoServicio? Izquierda { get; set; }
    public NodoServicio? Derecha { get; set; }

    public NodoServicio(int id, int idRepuesto, int idVehiculo, string detalle, float costo, string metodoPago) {
        Id = id;
        IdRepuesto = idRepuesto;
        IdVehiculo = idVehiculo;
        Detalle = detalle;
        Costo = costo;
        Izquierda = null;
        Derecha = null;
        MetodoPago = metodoPago;
    }
}

class BSTServicios {
    private NodoServicio? raiz;

    public BSTServicios() {
        raiz = null;
    }

    public NodoServicio? Raiz {
        get { return raiz; }
    }
    public void Agregar(int id, int idRepuesto, int idVehiculo, string detalle, float costo, string metodoPago) {
        if (Buscar(id) != null) {
            Console.WriteLine($"Error: El ID {id} ya ha sido ingresado.");
            return;
        }
        if (Buscar(id) != null) {
            return;
        }

        NodoServicio nuevo = new(id, idRepuesto, idVehiculo, detalle, costo, metodoPago);
        if (raiz == null) {
            raiz = nuevo;
            return;
        }

         NodoServicio? actual = raiz;
        while (true) {
            if (id < actual.Id) {
                if (actual.Izquierda == null) {
                    actual.Izquierda = nuevo;
                    return;
                }
                actual = actual.Izquierda;
            } else {
                if (actual.Derecha == null) {
                    actual.Derecha = nuevo;
                    return;
                }
                actual = actual.Derecha;
            }
        }
    }


    public void Eliminar(int id) {
        if (raiz == null) {
            return;
        }
        NodoServicio? actual = raiz;
        NodoServicio padre = raiz;
        bool esHijoIzquierdo = true;
        while (actual != null && actual.Id != id) {
            padre = actual;
            if (id < actual.Id) {
                esHijoIzquierdo = true;
                actual = actual.Izquierda;
            } else {
                esHijoIzquierdo = false;
                actual = actual.Derecha;
            }
            if (actual == null) {
                return;
            }
        }
        if (actual == null) {
            return;
        }

        if (actual.Izquierda == null && actual.Derecha == null) {
            if (actual == raiz) {
                raiz = null;
            } else if (esHijoIzquierdo) {
                padre.Izquierda = null;
            } else {
                padre.Derecha = null;
            }
        } else if (actual.Derecha == null) {
            if (actual == raiz) {
                raiz = actual.Izquierda;
            } else if (esHijoIzquierdo) {
                padre.Izquierda = actual.Izquierda;
            } else {
                padre.Derecha = actual.Izquierda;
            }
        } else if (actual.Izquierda == null) {
            if (actual == raiz) {
                raiz = actual.Derecha;
            } else if (esHijoIzquierdo) {
                padre.Izquierda = actual.Derecha;
            } else {
                padre.Derecha = actual.Derecha;
            }
        } else {
            NodoServicio reemplazo = ObtenerNodoReemplazo(actual);
            if (actual == raiz) {
                raiz = reemplazo;
            } else if (esHijoIzquierdo) {
                padre.Izquierda = reemplazo;
            } else {
                padre.Derecha = reemplazo;
            }
            reemplazo.Izquierda = actual.Izquierda;
        }
    }

    private NodoServicio ObtenerNodoReemplazo(NodoServicio nodoReemplazo) {
        NodoServicio reemplazarPadre = nodoReemplazo;
        NodoServicio reemplazo = nodoReemplazo;
        NodoServicio? actual = nodoReemplazo.Derecha;
        while (actual != null) {
            reemplazarPadre = reemplazo;
            reemplazo = actual;
            actual = actual.Izquierda;
        }
        if (reemplazo != nodoReemplazo.Derecha) {
            reemplazarPadre.Izquierda = reemplazo.Derecha;
            reemplazo.Derecha = nodoReemplazo.Derecha;
        }
        return reemplazo;
    }

    public void Actualizar(int id, string repuesto, string detalle, float costo) {
        NodoServicio? actual = raiz;
        while (actual != null && actual.Id != id) {
            if (id < actual.Id) {
                actual = actual.Izquierda;
            } else {
                actual = actual.Derecha;
            }
        }
        if (actual == null) {
            return;
        }
        actual.IdRepuesto = id;
        actual.IdVehiculo = id;
        actual.Detalle = detalle;
        actual.Costo = costo;
   }

   public NodoServicio? Buscar(int id){
         NodoServicio? actual = raiz;
         while (actual != null && actual.Id != id) {
              if (id < actual.Id) {
                actual = actual.Izquierda;
              } else {
                actual = actual.Derecha;
              }
         }
         if (actual == null) {
             return null;
         }
         return actual;
   }

   public int Contar(NodoServicio? nodo){
         if (nodo == null) {
             return 0;
         }
         return 1 + Contar(nodo.Izquierda) + Contar(nodo.Derecha);
   }

    public void Imprimir(NodoServicio? nodo){
          if (nodo == null) {
                return;
          }
          Imprimir(nodo.Izquierda);
          Console.WriteLine("ID: " + nodo.Id);
          Console.WriteLine("Repuesto: " + nodo.IdRepuesto);
          Console.WriteLine("ID Vehículo: " + nodo.IdVehiculo);
          Console.WriteLine("Detalle: " + nodo.Detalle);
          Console.WriteLine("Costo: " + nodo.Costo);
          Console.WriteLine();
          Imprimir(nodo.Derecha);
     }

     public ListStore recorrerPreorden(NodoServicio nodo, ListStore modelo){
          if (nodo != null) {
                modelo.AppendValues(nodo.Id, nodo.IdRepuesto, nodo.IdVehiculo, nodo.Detalle, nodo.Costo);
                if (nodo.Izquierda != null)
                    recorrerPreorden(nodo.Izquierda, modelo);
                if (nodo.Derecha != null)
                    recorrerPreorden(nodo.Derecha, modelo);
          }
            return modelo;
     }

     public ListStore recorrerInorden(NodoServicio nodo, ListStore modelo){
          if (nodo != null) {
                if (nodo.Izquierda != null)
                    recorrerInorden(nodo.Izquierda, modelo);
                modelo.AppendValues(nodo.Id, nodo.IdRepuesto, nodo.IdVehiculo, nodo.Detalle, nodo.Costo);
                if (nodo.Derecha != null)
                    recorrerInorden(nodo.Derecha, modelo);
          }
            return modelo;
     }

        public ListStore recorrerPostorden(NodoServicio nodo, ListStore modelo){
            if (nodo != null) {
                    if (nodo.Izquierda != null)
                        recorrerPostorden(nodo.Izquierda, modelo);
                    if (nodo.Derecha != null)
                        recorrerPostorden(nodo.Derecha, modelo);
                    modelo.AppendValues(nodo.Id, nodo.IdRepuesto, nodo.IdVehiculo, nodo.Detalle, nodo.Costo);
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
        
        string rutaDot = "reportedot/BSL.dot";
        string rutaReporte = "Reportes/BSL.png";
        dot.AppendLine("}");
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

    private void GraficarRecursivo(NodoServicio nodo, StringBuilder dot)
    {
        if (nodo != null)
        {
            string etiquetaNodo = $"\"ID: {nodo.Id} \\n Repuesto: {nodo.IdRepuesto} \\n Vehículo: {nodo.IdVehiculo} \\n Detalle: {nodo.Detalle} \\n Costo: {nodo.Costo}\"";
            if (nodo.Izquierda != null)
            {
                string etiquetaIzquierda = $"\"ID: {nodo.Izquierda.Id} \\n Repuesto: {nodo.Izquierda.IdRepuesto} \\n Vehículo: {nodo.Izquierda.IdVehiculo} \\n Detalle: {nodo.Izquierda.Detalle} \\n Costo: {nodo.Izquierda.Costo}\"";
                dot.AppendLine($"{etiquetaNodo} -> {etiquetaIzquierda};");
                GraficarRecursivo(nodo.Izquierda, dot);
            }
            if (nodo.Derecha != null)
            {
                string etiquetaDerecha = $"\"ID: {nodo.Derecha.Id} \\n Repuesto: {nodo.Derecha.IdRepuesto} \\n Vehículo: {nodo.Derecha.IdVehiculo} \\n Detalle: {nodo.Derecha.Detalle} \\n Costo: {nodo.Derecha.Costo}\"";
                dot.AppendLine($"{etiquetaNodo} -> {etiquetaDerecha};");
                GraficarRecursivo(nodo.Derecha, dot);
            }
        }
    }

    public bool EstaVacio() {
        return raiz == null;
    }
}

