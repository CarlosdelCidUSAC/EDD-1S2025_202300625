using Gtk;
using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
class NodoRepuesto {
    public int Id { get; set; }
    public string Repuesto { get; set; }
    public string Detalle { get; set; }
    public float Costo { get; set; }
    public NodoRepuesto? Izquierda { get; set; }
    public NodoRepuesto? Derecha { get; set; }

    public NodoRepuesto(int id, string repuesto, string detalle, float costo) {
        Id = id;
        Repuesto = repuesto;
        Detalle = detalle;
        Costo = costo;
        Izquierda = null;
        Derecha = null;
    }
}

class ArbolRepuestos {
    private NodoRepuesto? raiz;

    public ArbolRepuestos() {
        raiz = null;
    }

    public NodoRepuesto Raiz {
        get { return raiz; }
    }
    public void Agregar(int id, string repuesto, string detalle, float costo) {
        if (Buscar(id) != null) {
            return;
        }

        NodoRepuesto nuevo = new(id, repuesto, detalle, costo);
        if (raiz == null) {
            raiz = nuevo;
            return;
        }

        NodoRepuesto actual = raiz;
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
        NodoRepuesto actual = raiz;
        NodoRepuesto padre = raiz;
        bool esHijoIzquierdo = true;
        while (actual.Id != id) {
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
            NodoRepuesto reemplazo = ObtenerNodoReemplazo(actual);
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

    private NodoRepuesto ObtenerNodoReemplazo(NodoRepuesto nodoReemplazo) {
        NodoRepuesto reemplazarPadre = nodoReemplazo;
        NodoRepuesto reemplazo = nodoReemplazo;
        NodoRepuesto actual = nodoReemplazo.Derecha;
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
        NodoRepuesto actual = raiz;
        while (actual.Id != id) {
            if (id < actual.Id) {
                actual = actual.Izquierda;
            } else {
                actual = actual.Derecha;
            }
            if (actual == null) {
                return;
            }
        }
        actual.Repuesto = repuesto;
        actual.Detalle = detalle;
        actual.Costo = costo;
   }

   public NodoRepuesto Buscar(int id){
         NodoRepuesto actual = raiz;
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

    public void Imprimir(NodoRepuesto nodo){
          if (nodo != null) {
                Imprimir(nodo.Izquierda);
                Console.WriteLine("ID: " + nodo.Id);
                Console.WriteLine("Repuesto: " + nodo.Repuesto);
                Console.WriteLine("Detalle: " + nodo.Detalle);
                Console.WriteLine("Costo: " + nodo.Costo);
                Console.WriteLine();
                Imprimir(nodo.Derecha);
          }
     }

     public ListStore recorrerPreorden(NodoRepuesto nodo, ListStore modelo){
          if (nodo != null) {
                modelo.AppendValues(nodo.Id, nodo.Repuesto, nodo.Detalle, nodo.Costo);
                recorrerPreorden(nodo.Izquierda, modelo);
                recorrerPreorden(nodo.Derecha, modelo);
          }
            return modelo;
     }

     public ListStore recorrerInorden(NodoRepuesto nodo, ListStore modelo){
          if (nodo != null) {
                recorrerInorden(nodo.Izquierda, modelo);
                modelo.AppendValues(nodo.Id, nodo.Repuesto, nodo.Detalle, nodo.Costo);
                recorrerInorden(nodo.Derecha, modelo);
          }
            return modelo;
     }

        public ListStore recorrerPostorden(NodoRepuesto nodo, ListStore modelo){
            if (nodo != null) {
                    recorrerPostorden(nodo.Izquierda, modelo);
                    recorrerPostorden(nodo.Derecha, modelo);
                    modelo.AppendValues(nodo.Id, nodo.Repuesto, nodo.Detalle, nodo.Costo);
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

    private void GraficarRecursivo(NodoRepuesto nodo, StringBuilder dot)
    {
        if (nodo != null)
        {
            string etiquetaNodo = $"\"ID: {nodo.Id} \\n Repuesto: {nodo.Repuesto} \\n Detalle: {nodo.Detalle} \\n Costo: {nodo.Costo}\"";
            if (nodo.Izquierda != null)
            {
                string etiquetaIzquierda = $"\"ID: {nodo.Izquierda.Id} \\n Repuesto: {nodo.Izquierda.Repuesto} \\n Detalle: {nodo.Izquierda.Detalle} \\n Costo: {nodo.Izquierda.Costo}\"";
                dot.AppendLine($"{etiquetaNodo} -> {etiquetaIzquierda};");
                GraficarRecursivo(nodo.Izquierda, dot);
            }
            if (nodo.Derecha != null)
            {
                string etiquetaDerecha = $"\"ID: {nodo.Derecha.Id} \\n Repuesto: {nodo.Derecha.Repuesto} \\n Detalle: {nodo.Derecha.Detalle} \\n Costo: {nodo.Derecha.Costo}\"";
                dot.AppendLine($"{etiquetaNodo} -> {etiquetaDerecha};");
                GraficarRecursivo(nodo.Derecha, dot);
            }
        }
    }

    public bool EstaVacio() {
        return raiz == null;
    }
}

