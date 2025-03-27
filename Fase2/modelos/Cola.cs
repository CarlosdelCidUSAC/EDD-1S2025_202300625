// filepath: /home/carlos/Escritorio/EDD/Proyecto/EDD-1S2025_202300625/Fase2/modelos/Cola.cs
using System;

namespace EDD.Proyecto.Fase2.Modelos
{
    public class NodoCola
    {
        public NodoFactura Valor { get; set; }
        public NodoCola? Siguiente { get; set; }

        public NodoCola(NodoFactura valor)
        {
            Valor = valor;
            Siguiente = null;
        }
    }

    public class Cola
    {
        private NodoCola? frente;
        private NodoCola? final;

        public Cola()
        {
            frente = null;
            final = null;
        }

        public bool EstaVacia()
        {
            return frente == null;
        }

        public void Encolar(NodoFactura nuevoNodo)
        {
            NodoCola nuevo = new NodoCola(nuevoNodo);
            if (EstaVacia())
            {
                frente = nuevo;
                final = nuevo;
            }
            else
            {
                final!.Siguiente = nuevo;
                final = nuevo;
            }
        }

        public NodoFactura? Desencolar()
        {
            if (EstaVacia())
            {
                return null;
            }

            NodoFactura valor = frente!.Valor;
            frente = frente.Siguiente;

            if (frente == null)
            {
                final = null;
            }

            return valor;
        }

        public NodoFactura? VerFrente()
        {
            return frente?.Valor;
        }

        public int ContarElementos()
        {
            int contador = 0;
            NodoCola? actual = frente;

            while (actual != null)
            {
            contador++;
            actual = actual.Siguiente;
            }

            return contador;
        }
    }
}