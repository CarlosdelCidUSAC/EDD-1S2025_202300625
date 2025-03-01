using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using System.IO;

unsafe struct NodoCabecera
{
    public int* id;
    public NodoCabecera* siguiente;
    public NodoCabecera* anterior;

    public NodoCelda* acceso;
}

unsafe struct NodoCelda
{
 public char* detalles;
 public int* x;
public int* y;

 public NodoCelda* arriba;
 public NodoCelda* abajo;
 public NodoCelda* derecha;
 public NodoCelda* izquierda;
   
}  

unsafe class ListaCabecera : IDisposable
{
    private IntPtr coordenada;
    public NodoCabecera* cabeza = null;
    public NodoCabecera* cola = null;
    private IntPtr tamanio;
    private bool disposed = false;

    public ListaCabecera(string coordenada)
    {
        this.coordenada = Marshal.StringToHGlobalAnsi(coordenada);
        tamanio = Marshal.AllocHGlobal(sizeof(int));
        *(int*)tamanio = 0;
    }

    ~ListaCabecera()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (coordenada != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(coordenada);
                coordenada = IntPtr.Zero;
            }

            if (tamanio != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(tamanio);
                tamanio = IntPtr.Zero;
            }

            NodoCabecera* actual = cabeza;
            while (actual != null)
            {
                NodoCabecera* siguiente = actual->siguiente;
                Marshal.FreeHGlobal((IntPtr)actual);
                actual = siguiente;
            }

            cabeza = null;
            cola = null;
            disposed = true;
        }
    }

    public NodoCabecera* ObtenerNodoCabecera(int id)
    {
        NodoCabecera* actual = cabeza;
        while (actual != null)
        {
            if (actual->id != null && *actual->id == id)
            {
                return actual;
            }
            actual = actual->siguiente;
        }
        return null;
    }

    public void InsertarNodoCabecera(NodoCabecera* nuevo)
    {
        if (nuevo == null || nuevo->id == null)
            return;

        if (cabeza == null)
        {
            cabeza = nuevo;
            cola = nuevo;
        }
        else if (*nuevo->id < *cabeza->id)
        {
            nuevo->siguiente = cabeza;
            cabeza->anterior = nuevo;
            cabeza = nuevo;
        }
        else if (*nuevo->id > *cola->id)
        {
            cola->siguiente = nuevo;
            nuevo->anterior = cola;
            cola = nuevo;
        }
        else
        {
            NodoCabecera* actual = cabeza;
            while (actual != null)
            {
                if (*nuevo->id < *actual->id)
                {
                    nuevo->siguiente = actual;
                    nuevo->anterior = actual->anterior;
                    if (actual->anterior != null)
                        actual->anterior->siguiente = nuevo;
                    actual->anterior = nuevo;
                    break;
                }
                actual = actual->siguiente;
            }
        }
        (*(int*)tamanio)++;
    }
}


unsafe class BitacoraMatrizDispersa
{
    public ListaCabecera filas;
    public ListaCabecera columnas;

    public BitacoraMatrizDispersa()
    {
        filas = new ListaCabecera("fila");
        columnas = new ListaCabecera("columna");
    }


public void insertar(int x, int y, string detalle)
{
    NodoCelda* nuevoNodoCelda = (NodoCelda*)Marshal.AllocHGlobal(sizeof(NodoCelda));
    try
    {
        nuevoNodoCelda->detalles = (char*)Marshal.StringToHGlobalAnsi(detalle);
        nuevoNodoCelda->x = (int*)Marshal.AllocHGlobal(sizeof(int));
        nuevoNodoCelda->y = (int*)Marshal.AllocHGlobal(sizeof(int));
        *nuevoNodoCelda->x = x;
        *nuevoNodoCelda->y = y;
        nuevoNodoCelda->derecha = null;
        nuevoNodoCelda->izquierda = null;
        nuevoNodoCelda->arriba = null;
        nuevoNodoCelda->abajo = null;

        NodoCabecera* CeldaX = filas != null ? filas.ObtenerNodoCabecera(x) : null;
        NodoCabecera* CeldaY = columnas != null ? columnas.ObtenerNodoCabecera(y) : null;

        if (CeldaX == null)
        {
            CeldaX = (NodoCabecera*)Marshal.AllocHGlobal(sizeof(NodoCabecera));
            CeldaX->id = (int*)Marshal.AllocHGlobal(sizeof(int));
            *CeldaX->id = x;
            CeldaX->acceso = null;
            CeldaX->siguiente = null;
            CeldaX->anterior = null;
            filas.InsertarNodoCabecera(CeldaX);
        }

        if (CeldaY == null)
        {
            CeldaY = (NodoCabecera*)Marshal.AllocHGlobal(sizeof(NodoCabecera));
            CeldaY->id = (int*)Marshal.AllocHGlobal(sizeof(int));
            *CeldaY->id = y;
            CeldaY->acceso = null;
            CeldaY->siguiente = null;
            CeldaY->anterior = null;
            columnas.InsertarNodoCabecera(CeldaY);
        }

        if (CeldaX->acceso == null)
        {
            CeldaX->acceso = nuevoNodoCelda;
        }
        else
        {
            NodoCelda* actual = CeldaX->acceso;
            while (actual->derecha != null && actual->y < nuevoNodoCelda->y)
            {
                actual = actual->derecha;
            }

            if (actual->y > nuevoNodoCelda->y)
            {
                nuevoNodoCelda->derecha = actual;
                nuevoNodoCelda->izquierda = actual->izquierda;
                if (actual->izquierda != null)
                    actual->izquierda->derecha = nuevoNodoCelda;
                else
                    CeldaX->acceso = nuevoNodoCelda;
                actual->izquierda = nuevoNodoCelda;
            }
            else
            {
                actual->derecha = nuevoNodoCelda;
                nuevoNodoCelda->izquierda = actual;
            }
        }

        if (CeldaY->acceso == null)
        {
            CeldaY->acceso = nuevoNodoCelda;
        }
        else
        {
            NodoCelda* actual = CeldaY->acceso;
            while (actual->abajo != null && actual->x < nuevoNodoCelda->x)
            {
                actual = actual->abajo;
            }

            if (actual->x > nuevoNodoCelda->x)
            {
                nuevoNodoCelda->abajo = actual;
                nuevoNodoCelda->arriba = actual->arriba;
                if (actual->arriba != null)
                    actual->arriba->abajo = nuevoNodoCelda;
                else
                    CeldaY->acceso = nuevoNodoCelda;
                actual->arriba = nuevoNodoCelda;
            }
            else
            {
                actual->abajo = nuevoNodoCelda;
                nuevoNodoCelda->arriba = actual;
            }
        }
    }
    catch
    {
        Marshal.FreeHGlobal((IntPtr)nuevoNodoCelda);
        throw;
    }
}


    public bool MatrizVacia()
    {
        return filas == null && columnas == null;
    
    
    }


    public void Graficar()
    {
        try
        {
            StringBuilder codigodot = new StringBuilder();
            codigodot.Append("digraph G {\n");
            codigodot.Append("graph [pad=\"0.5\", nodesep=\"1\", ranksep=\"1\"];\n");
            codigodot.Append("label=\"Matriz Dispersa\"\n");
            codigodot.Append("node [shape=box, height=0.8];\n");

            NodoCabecera* filaActual = filas.cabeza;
            StringBuilder idFila = new StringBuilder();
            StringBuilder conexionesFilas = new StringBuilder();
            StringBuilder nodosInteriores = new StringBuilder();
            StringBuilder direccionInteriores = new StringBuilder();
            while (filaActual != null)
            {
                bool primero = true;
                if (filaActual->acceso == null)
                {
                    filaActual = filaActual->siguiente;
                    continue;
                }
                NodoCelda* actual = filaActual->acceso;
                idFila.Append($"\tFila{*filaActual->id}[style=\"filled\" label=\"Fila ID: {*filaActual->id}\" fillcolor=\"white\" group=0];\n");

                if (filaActual->siguiente != null)
                {
                    conexionesFilas.Append($"\tFila{*filaActual->id} -> Fila{*filaActual->siguiente->id};\n");
                }
                direccionInteriores.Append($"\t{{ rank = same; Fila{*filaActual->id}; ");

                while (actual != null)
                {
                    nodosInteriores.Append($"\tNodoF{*actual->x}_C{*actual->y}[style=\"filled\" label=\"Detalle: {Marshal.PtrToStringAnsi((IntPtr)actual->detalles)}\" fillcolor=\"white\" group={*actual->y}];\n");
                    direccionInteriores.Append($"NodoF{*actual->x}_C{*actual->y}; ");

                    if (primero)
                    {
                        nodosInteriores.Append($"\tFila{*actual->x} -> NodoF{*actual->x}_C{*actual->y}[dir=\"\"];\n");
                        if (actual->derecha != null)
                        {
                            nodosInteriores.Append($"\tNodoF{*actual->x}_C{*actual->y} -> NodoF{*actual->x}_C{*actual->derecha->y}[dir=\"both\"];\n");
                        }
                        primero = false;
                    }
                    else
                    {
                        if (actual->derecha != null)
                        {
                            nodosInteriores.Append($"\tNodoF{*actual->x}_C{*actual->y} -> NodoF{*actual->x}_C{*actual->derecha->y}[dir=\"both\"];\n");
                        }
                    }
                    actual = actual->derecha;
                }
                filaActual = filaActual->siguiente;
                direccionInteriores.Append("};\n");
            }
            codigodot.Append(idFila);
            codigodot.Append("edge[dir=\"both\"];\n");
            codigodot.Append(conexionesFilas);
            codigodot.Append("edge[dir=\"both\"];\n");

            NodoCabecera* columnaActual = columnas.cabeza;
            StringBuilder idColumna = new StringBuilder();
            StringBuilder conexionesColumnas = new StringBuilder();
            StringBuilder direccionesInteriores2 = new StringBuilder();
            direccionesInteriores2.Append("\t{ rank = same; ");

            while (columnaActual != null)
            {
                bool primero = true;
                NodoCelda* actual = columnaActual->acceso;
                idColumna.Append($"\tColumna{*columnaActual->id}[style=\"filled\" label=\"Columna ID: {*columnaActual->id}\" fillcolor=\"white\" group=0];\n");
                direccionesInteriores2.Append($"Columna{*columnaActual->id}; ");

                if (columnaActual->siguiente != null)
                {
                    conexionesColumnas.Append($"\tColumna{*columnaActual->id} -> Columna{*columnaActual->siguiente->acceso->y};\n");
                }
                while (actual != null)
                {
                    if (primero)
                    {
                        codigodot.Append($"\tColumna{*actual->y} -> NodoF{*actual->x}_C{*actual->y}[dir=\"\"];\n");
                        if (actual->abajo != null)
                        {
                            codigodot.Append($"\tNodoF{*actual->x}_C{*actual->y} -> NodoF{*actual->abajo->x}_C{*actual->y}[dir=\"both\"];\n");
                        }
                        primero = false;
                    }
                    else
                    {
                        if (actual->abajo != null)
                        {
                            codigodot.Append($"\tNodoF{*actual->x}_C{*actual->y} -> NodoF{*actual->abajo->x}_C{*actual->y}[dir=\"both\"];\n");
                        }
                    }
                    actual = actual->abajo;
                }
                columnaActual = columnaActual->siguiente;
            }
            codigodot.Append(idColumna);
            codigodot.Append(conexionesColumnas + "\n");
            codigodot.Append(direccionesInteriores2 + "};\n");
            codigodot.Append(nodosInteriores);
            codigodot.Append(direccionInteriores);
            codigodot.Append("}\n");

            string rutaDot = "reportedot/matriz.dot";
            string rutaReporte = "reportes/matriz.png";
            string codigoDot = codigodot.ToString();

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
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error: {ex.Message}");
        }
    }
    
}
