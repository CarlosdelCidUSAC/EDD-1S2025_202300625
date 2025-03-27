using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using Gtk;

public class Contenido
{
    public int Id { get; set; }
    public int IdOrden {get; set; }
    public float Total { get; set; }

    public Contenido(int id, int idOrden, float total)
    {
        this.Id = id;
        this.IdOrden = idOrden;
        this.Total = total;
    }
}

public class NodoFactura
{
    private const int O = 5;
    private const int MC = O - 1;
    private const int mC = (O / 2) - 1;

    public List<Contenido> Contenidos { get; set; }
    public List<NodoFactura> Hijos { get; set; }
    public bool EsHoja { get; set; }

    public NodoFactura()
    {
        Contenidos = new List<Contenido>();
        Hijos = new List<NodoFactura>();
        EsHoja = true;
    }

    public bool Estalleno()
    {
        return Contenidos.Count >= MC;
    }

    public bool MinClaves()
    {
        return Contenidos.Count >= mC;
    }
} 
class ArbolBFacturas
{
    private NodoFactura raiz;
    private const int O = 5;
    private const int MC = O - 1;
    private const int mC = (O / 2) - 1;

    public NodoFactura Raiz
    {
        get { return raiz; }
    }
    public ArbolBFacturas()
    {
        raiz = new NodoFactura();
    }

    public void Insertar(int id, float total)
    {
        Contenido nuevo = new Contenido(id, nuevoId() , total);

        if(raiz.Estalleno())
        {
            NodoFactura nuevoNodo = new NodoFactura();
            nuevoNodo.Hijos.Add(raiz);
            nuevoNodo.EsHoja = false;
            SepararNodo(nuevoNodo, 0);
            raiz = nuevoNodo;
        }


        InsertarNoLleno(raiz, nuevo);
    }

    public void SepararNodo(NodoFactura padre, int posicion)
    {
        NodoFactura NodoLleno = padre.Hijos[posicion];
        NodoFactura nuevoNodo = new NodoFactura();
        nuevoNodo.EsHoja = NodoLleno.EsHoja;

        Contenido contenidoMedio = NodoLleno.Contenidos[mC];

        for (int i = mC + 1; i < NodoLleno.Contenidos.Count; i++)
        {
            nuevoNodo.Contenidos.Add(NodoLleno.Contenidos[i]);
        }

        if (!NodoLleno.EsHoja)
        {
            for (int i = mC + 1; i < NodoLleno.Hijos.Count; i++)
            {
                nuevoNodo.Hijos.Add(NodoLleno.Hijos[i]);
            }
            NodoLleno.Hijos.RemoveRange(mC + 1, NodoLleno.Hijos.Count - (mC + 1));
        }

        NodoLleno.Contenidos.RemoveRange(mC, NodoLleno.Contenidos.Count - mC);

        padre.Hijos.Insert(posicion + 1, nuevoNodo);

        int j = 0;
        while (j < padre.Contenidos.Count && padre.Contenidos[j].Id < contenidoMedio.Id)
        {
            j++;
        }
        padre.Contenidos.Insert(j, contenidoMedio);
    }

    public void InsertarNoLleno(NodoFactura nodo, Contenido contenido)
    {
        if(nodo.EsHoja)
        {
            int i = 0;
            while(i < nodo.Contenidos.Count && nodo.Contenidos[i].Id < contenido.Id)
            {
                i++;
            }
            nodo.Contenidos.Insert(i, contenido);
        }
        else
        {
            int i = 0;
            while(i < nodo.Contenidos.Count && nodo.Contenidos[i].Id < contenido.Id)
            {
                i++;
            }
            if(nodo.Hijos[i].Estalleno())
            {
                SepararNodo(nodo, i);
            }
            else
            {
                InsertarNoLleno(nodo.Hijos[i], contenido);
            }
        }
    }
    public int nuevoId()
    {
        return raiz.Contenidos.Count + 1;
    }

    public Contenido Buscar(int id)
    {
        return BuscarRecursivo(raiz, id);
    }

    public Contenido BuscarRecursivo(NodoFactura nodo, int id)
    {
        int i = 0;
        while(i < nodo.Contenidos.Count && id > nodo.Contenidos[i].Id)
        {
            i++;
        }
        if(i < nodo.Contenidos.Count && nodo.Contenidos[i].Id == id)
        {
            return nodo.Contenidos[i];
        }
        if(nodo.EsHoja)
        {
            return new Contenido(-1, -1, 0.0f); 
        }
        else
        {
            return BuscarRecursivo(nodo.Hijos[i], id);
        }
    }

    public void Eliminar(int id)
    {
        EliminarRecursivo(raiz, id);

        if(raiz.Contenidos.Count == 0)
        {
            raiz = raiz.Hijos[0];
        }
    }

    public void EliminarRecursivo(NodoFactura nodo, int id)
    {
        int i = 0;
        while(i < nodo.Contenidos.Count && id > nodo.Contenidos[i].Id)
        {
            i++;
        }
        if(i < nodo.Contenidos.Count && nodo.Contenidos[i].Id == id)
        {
            if(nodo.EsHoja)
            {
                nodo.Contenidos.RemoveAt(i);
            }
            else
            {
                Contenido predecesor = ObtenerPredecesor(nodo, i);
                nodo.Contenidos[i] = predecesor;
                EliminarRecursivo(nodo.Hijos[i], predecesor.Id);
            }
        }
        else
        {
            if(nodo.EsHoja)
            {
                return;
            }
            bool bandera = (i == nodo.Contenidos.Count);
            if(nodo.Hijos[i].Contenidos.Count < mC)
            {
                Llenar(nodo, i);
            }
            if(bandera && i > nodo.Contenidos.Count)
            {
                EliminarRecursivo(nodo.Hijos[i - 1], id);
            }
            else
            {
                EliminarRecursivo(nodo.Hijos[i], id);
            }
        }
    }

    public Contenido ObtenerPredecesor(NodoFactura nodo, int i)
    {
        NodoFactura actual = nodo.Hijos[i];
        while(!actual.EsHoja)
        {
            actual = actual.Hijos[actual.Contenidos.Count];
        }
        return actual.Contenidos[actual.Contenidos.Count - 1];
    }

    public void Llenar(NodoFactura nodo, int i)
    {
        if(i != 0 && nodo.Hijos[i - 1].Contenidos.Count > mC)
        {
            PrestarDeAnterior(nodo, i);
        }
        else if(i != nodo.Contenidos.Count && nodo.Hijos[i + 1].Contenidos.Count > mC)
        {
            PrestarDeSiguiente(nodo, i);
        }
        else
        {
            if(i != nodo.Contenidos.Count)
            {
                Combinar(nodo, i);
            }
            else
            {
                Combinar(nodo, i - 1);
            }
        }
    }

    public void PrestarDeAnterior(NodoFactura nodo, int i)
    {
        NodoFactura hijo = nodo.Hijos[i];
        NodoFactura hermano = nodo.Hijos[i - 1];

        hijo.Contenidos.Insert(0, nodo.Contenidos[i - 1]);
        if(!hijo.EsHoja)
        {
            hijo.Hijos.Insert(0, hermano.Hijos[hermano.Contenidos.Count]);
            hermano.Hijos.RemoveAt(hermano.Contenidos.Count);
        }
        nodo.Contenidos[i - 1] = hermano.Contenidos[hermano.Contenidos.Count - 1];
        hermano.Contenidos.RemoveAt(hermano.Contenidos.Count - 1);
    }

    public void PrestarDeSiguiente(NodoFactura nodo, int i)
    {
        NodoFactura hijo = nodo.Hijos[i];
        NodoFactura hermano = nodo.Hijos[i + 1];

        hijo.Contenidos.Add(nodo.Contenidos[i]);
        if(!hijo.EsHoja)
        {
            hijo.Hijos.Add(hermano.Hijos[0]);
            hermano.Hijos.RemoveAt(0);
        }
        nodo.Contenidos[i] = hermano.Contenidos[0];
        hermano.Contenidos.RemoveAt(0);
    }

    public void Combinar(NodoFactura nodo, int i)
    {
        NodoFactura hijo = nodo.Hijos[i];
        NodoFactura hermano = nodo.Hijos[i + 1];

        hijo.Contenidos.Add(nodo.Contenidos[i]);
        for(int j = 0; j < hermano.Contenidos.Count; j++)
        {
            hijo.Contenidos.Add(hermano.Contenidos[j]);
        }
        if(!hijo.EsHoja)
        {
            for(int j = 0; j < hermano.Hijos.Count; j++)
            {
                hijo.Hijos.Add(hermano.Hijos[j]);
            }
        }
        nodo.Contenidos.RemoveAt(i);
        nodo.Hijos.RemoveAt(i + 1);
    }

    public ListStore RecorrerInOrden(NodoFactura nodo, ListStore modelo)
    {
        if (nodo != null)
        {
            for (int i = 0; i < nodo.Contenidos.Count; i++)
            {
                if (i < nodo.Hijos.Count) // Verifica si el hijo existe
                {
                    RecorrerInOrden(nodo.Hijos[i], modelo);
                }
                modelo.AppendValues(nodo.Contenidos[i].Id, nodo.Contenidos[i].IdOrden, nodo.Contenidos[i].Total);
            }
            if (nodo.Contenidos.Count < nodo.Hijos.Count) // Verifica si el último hijo existe
            {
                RecorrerInOrden(nodo.Hijos[nodo.Contenidos.Count], modelo);
            }
        }
        return modelo;
    }

    public bool EstaVacio()
    {
        return raiz.Contenidos.Count == 0;
    }

    public void Graficar()
    {
        StringBuilder dot = new StringBuilder();
        dot.Append("digraph G {\n");
        dot.Append("node [shape=record];\n");

        if(!EstaVacio())
        {
            GenerarNodos(Raiz,dot,0);
        }

        dot.Append("}");

        string rutaDot = "reportedot/B5.dot";
        string rutaReporte = "Reportes/B5.png";
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

    private static int GenerarNodos(NodoFactura nodo, StringBuilder dot, int id)
    {
        int nodoId = id;
        dot.Append($"node{nodoId} [label=\"");
        for (int i = 0; i < nodo.Contenidos.Count; i++)
        {
            dot.Append($"<f{i}>Id: {nodo.Contenidos[i].Id}\\n---\\nOrden: {nodo.Contenidos[i].IdOrden}\\n---\\nTotal: {nodo.Contenidos[i].Total}");
            if (i < nodo.Contenidos.Count - 1)
            {
            dot.Append("|");
            }
        }
        dot.Append("\"];\n");

        int hijoId = nodoId + 1;
        for (int i = 0; i < nodo.Hijos.Count; i++)
        {
            dot.Append($"node{nodoId}:f{i} -> node{hijoId};\n");
            hijoId = GenerarNodos(nodo.Hijos[i], dot, hijoId);
        }
        return hijoId;
    }
}
