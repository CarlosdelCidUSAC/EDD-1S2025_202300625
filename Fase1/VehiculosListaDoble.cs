using System;
using System.Runtime.InteropServices;

unsafe struct Nodo
{
    public int Id;
    public int Id_Usuario;
    public char* Marca;
    public char* Modelo;
    public char* Placa;
    public Nodo* Siguiente;
    public Nodo* Anterior;  
}

unsafe class VehiculoListaDoble
{

    private Nodo* cabeza;


    public void agregarPrimero(int id, int Id_Usuario, string Marca, string Modelo, string Placa)
    {
        if (string.IsNullOrEmpty(Marca) || string.IsNullOrEmpty(Modelo) || string.IsNullOrEmpty(Placa))
        {
            throw new ArgumentException("Los campos no pueden ser nulos o vacíos.");
        }

        Nodo* nuevoNodo = (Nodo*)Marshal.AllocHGlobal(sizeof(Nodo));

        nuevoNodo->Id = id;
        nuevoNodo->Id_Usuario = Id_Usuario;
        nuevoNodo->Marca = (char*)Marshal.StringToHGlobalAnsi(Marca);
        nuevoNodo->Modelo = (char*)Marshal.StringToHGlobalAnsi(Modelo);
        nuevoNodo->Placa = (char*)Marshal.StringToHGlobalAnsi(Placa);
        nuevoNodo->Siguiente = cabeza;
        nuevoNodo->Anterior = null;

        if (cabeza != null)
        {
            cabeza->Anterior = nuevoNodo;
        }

        cabeza = nuevoNodo;


    }

}