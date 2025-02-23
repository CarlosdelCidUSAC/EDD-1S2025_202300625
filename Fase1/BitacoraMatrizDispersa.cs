using System;
using System.Runtime.InteropServices;

unsafe struct NodoCabecera
{
    public int id;
    public NodoCabecera siguiente;
    public NodoCabecera anterior;

    public NodoCelda acceso;
}

unsafe struct NodoCelda
{
 public int id;
 public int id_repuesto;
 public char* detalles;

 public int x;
public int y;

 public NodoCelda arriba;
 public NodoCelda abajo;
 public NodoCelda derecha;
 public NodoCelda izquierda;
   
}  

unsafe class listaCabecera
{
    private char* coordenada;
    NodoCabecera* cabeza = (NodoCabecera*)Marshal.AllocHGlobal(sizeof(NodoCabecera));
    NodoCabecera* cola = (NodoCabecera*)Marshal.AllocHGlobal(sizeof(NodoCabecera));
    private int tamanio;

    public listaCabecera(char* coordenada)
    {
        this.coordenada = coordenada;
        cabeza = null;
        cola = null;
        tamanio = 0;
    }
    {
        cabeza = null;
        cola = null;
        tamanio = 0;
    }

    public insertarNodoCabecera(NodoCelda* nuevo)

    {
        if (cabeza == null && cola == null)
        {
            cabeza = nuevo;
            cola = nuevo;
        }
        else
        {
            if (nuevo->id < cabeza->id)
            {
                nuevo->siguiente = cabeza;
                cabeza->anterior = nuevo;
                cabeza = nuevo;
            }
            else if (nuevo->id > cola->id)
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
                    if (nuevo->id < actual->id)
                    {
                        nuevo->siguiente = actual;
                        nuevo->anterior = actual->anterior;
                        actual->anterior->siguiente = nuevo;
                        actual->anterior = nuevo;
                        break;
                    }
                    else if (nuevo->id > actual.id)
                    {
                        actual = actual->siguiente;
                    }
                    else
                    {
                        break;
                    }
                    this.tamanio++;
                }
            }
        }
    }

    public NodoCabecera obtenerNodoCabecera(int id)
    {
        NodoCabecera* actual = cabeza;
        while (actual != null)
        {
            if (actual->id == id)
            {
                return actual;
            }
            actual = actual->siguiente;
        }
        return null;
    }

}

unsafe class MatrizDispersa
{
    private listaCabecera filas = new listaCabecera("Fila");
    private listaCabecera columnas = new listaCabecera("Columna");

    public void insertar(int x, int y, int id, int id_repuesto, string detalle)
    {
        nuevoNodoCelda = (NodoCelda*)Marshal.AllocHGlobal(sizeof(NodoCelda));
        nuevoNodoCelda->id = id;
        nuevoNodoCelda->id_repuesto = id_repuesto;
        nuevoNodoCelda->detalles = (char*)Marshal.StringToHGlobalAnsi(detalle);
        nuevoNodoCelda->x = x;
        nuevoNodoCelda->y = y;

        NodoCabecera* CeldaX = filas.obtenerNodoCabecera(x);
        NodoCabecera* CeldaY = columnas.obtenerNodoCabecera(y);

        if (CeldaX == null)
        {
            CeldaX = (NodoCabecera*)Marshal.AllocHGlobal(sizeof(NodoCabecera));
            CeldaX->id = x;
            filas.insertarNodoCabecera(CeldaX);
        }

        if (CeldaY == null)
        {
            CeldaY = (NodoCabecera*)Marshal.AllocHGlobal(sizeof(NodoCabecera));
            CeldaY->id = y;
            columnas.insertarNodoCabecera(CeldaY);
        }

        if(CeldaX.acceso == null)
        {
            CeldaX = nuevoNodoCelda;
        }
        else
        {
            if (nuevoNodoCelda.y < CeldaX.acceso.y)
            {
                nuevoNodoCelda->derecha = CeldaX.acceso;
                CeldaX.acceso->izquierda = nuevoNodoCelda;
                CeldaX.acceso = nuevoNodoCelda;
            }
            else if (nuevoNodoCelda.y > CeldaX.acceso.y)
            {
                CeldaX.acceso->derecha = nuevoNodoCelda;
                nuevoNodoCelda->izquierda = CeldaX.acceso;
                CeldaX.acceso = nuevoNodoCelda;
            }
            else
            {
                NodoCelda* actual = CeldaX.acceso;
                while (actual != null)
                {
                    if (nuevoNodoCelda.y < actual.y)
                    {
                        nuevoNodoCelda->derecha = actual;
                        nuevoNodoCelda->izquierda = actual->izquierda;
                        actual->izquierda->derecha = nuevoNodoCelda;
                        actual->izquierda = nuevoNodoCelda;
                        break;
                    }
                    else if (nuevoNodoCelda.y > actual.y)
                    {
                        actual = actual->derecha;
                    }
                    else
                    {
                        break;
                    }
                }   
                else if (nuevoNodoCelda.x == actual.x && nuevoNodoCelda.y == actual.y)
                {
                    actual->id_repuesto = nuevoNodoCelda.id_repuesto;
                    actual->detalles = nuevoNodoCelda.detalles;
                    break;
                }
                else
                {
                   if(actual->derecha == null)
                   {
                       actual->derecha = nuevoNodoCelda;
                       nuevoNodoCelda->izquierda = actual;
                       break;
                   }
                     else
                     {
                          actual = actual->derecha;
                     }
                }
        }

        if (CeldaY.acceso == null)
        {
            CeldaY.acceso = nuevoNodoCelda;
        }
        else
        {
            if (nuevoNodoCelda.x < CeldaY.acceso.x)
            {
                nuevoNodoCelda->abajo = CeldaY.acceso;
                CeldaY.acceso->arriba = nuevoNodoCelda;
                CeldaY.acceso = nuevoNodoCelda;
            }

            else
            {
                NodoCelda* actual2 = CeldaY.acceso;
                while (actual2 != null)
                {
                    if (nuevoNodoCelda.x < actual2.x)
                    {
                        nuevoNodoCelda->abajo = actual2;
                        nuevoNodoCelda->arriba = actual2->arriba;
                        actual2->arriba->abajo = nuevoNodoCelda;
                        actual2->arriba = nuevoNodoCelda;
                        break;
                    }
                    else if (nuevoNodoCelda.x > actual2.x)
                    {
                        actual2 = actual2->abajo;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (nuevoNodoCelda.x == actual2.x && nuevoNodoCelda.y == actual2.y)
                {
                    actual2->id_repuesto = nuevoNodoCelda.id_repuesto;
                    actual2->detalles = nuevoNodoCelda.detalles;
                    break;
                }
                else
                {
                    if (actual2->abajo == null)
                    {
                        actual2->abajo = nuevoNodoCelda;
                        nuevoNodoCelda->arriba = actual2;
                        break;
                    }
                    else
                    {
                        actual2 = actual2->abajo;
                    }
                }
                
            }
        }
    }


    
}
