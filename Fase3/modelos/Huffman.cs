using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class CompresionHuffman
{
    // Devuelve los datos comprimidos como byte[] y el árbol para descompresión
    public static (byte[] Comprimido, NodoHuffman Raiz, int Padding) Comprimir(byte[] datos)
    {
        if (datos == null || datos.Length == 0)
            throw new ArgumentException("La entrada no puede estar vacía.");

        // Calcular frecuencias
        var frequencies = new Dictionary<byte, int>();
        foreach (var b in datos)
            frequencies[b] = frequencies.GetValueOrDefault(b) + 1;

        // Crear cola de prioridad
        var pq = new PriorityQueue<NodoHuffman>();
        foreach (var kv in frequencies)
            pq.Encolar(new NodoHuffman { ByteValue = kv.Key, Frequency = kv.Value });

        // Manejar caso de un solo símbolo
        if (pq.Count == 1)
        {
            var single = pq.Desencolar();
            var dummy = new NodoHuffman { Frequency = single.Frequency, Left = single, Right = null };
            pq.Encolar(dummy);
        }

        // Construir árbol
        while (pq.Count > 1)
        {
            var left = pq.Desencolar();
            var right = pq.Desencolar();
            pq.Encolar(new NodoHuffman
            {
                ByteValue = null,
                Frequency = left.Frequency + right.Frequency,
                Left = left,
                Right = right
            });
        }
        var root = pq.Desencolar();

        // Generar códigos
        var codes = new Dictionary<byte, string>();
        BuildCodes(root, "", codes);

        // Codificar a bitstring
        var bitString = new StringBuilder();
        foreach (var b in datos)
            bitString.Append(codes[b]);

        // Convertir bitstring a bytes
        int padding = (8 - (bitString.Length % 8)) % 8;
        bitString.Append('0', padding);
        int byteCount = bitString.Length / 8;
        var output = new byte[byteCount];
        for (int i = 0; i < byteCount; i++)
        {
            string byteStr = bitString.ToString(i * 8, 8);
            output[i] = Convert.ToByte(byteStr, 2);
        }

        return (output, root, padding);
    }

    // Descomprimir usando árbol y padding
    public static byte[] Descomprimir(byte[] comprimido, NodoHuffman raiz, int padding)
    {
        if (comprimido == null || comprimido.Length == 0)
            return Array.Empty<byte>();

        // Reconstruir bitstring
        var sb = new StringBuilder(comprimido.Length * 8);
        foreach (var b in comprimido)
            sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));

        // Quitar padding
        sb.Length -= padding;

        // Recorrer bits
        var result = new List<byte>();
        var current = raiz;
        foreach (char bit in sb.ToString())
        {
            current = (bit == '0') ? current.Left : current.Right;
            if (current.IsLeaf)
            {
                result.Add(current.ByteValue.Value);
                current = raiz;
            }
        }

        return result.ToArray();
    }

    private static void BuildCodes(NodoHuffman nodo, string prefix, Dictionary<byte, string> codes)
    {
        if (nodo == null) return;
        if (nodo.IsLeaf)
        {
            codes[nodo.ByteValue.Value] = prefix.Length > 0 ? prefix : "0";
        }
        BuildCodes(nodo.Left, prefix + "0", codes);
        BuildCodes(nodo.Right, prefix + "1", codes);
    }
}

public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> data = new List<T>();
    public int Count => data.Count;
    public void Encolar(T item)
    {
        data.Add(item);
        int ci = data.Count - 1;
        while (ci > 0)
        {
            int pi = (ci - 1) / 2;
            if (data[ci].CompareTo(data[pi]) >= 0) break;
            (data[ci], data[pi]) = (data[pi], data[ci]);
            ci = pi;
        }
    }
    public T Desencolar()
    {
        var front = data[0];
        var li = data.Count - 1;
        data[0] = data[li]; data.RemoveAt(li);
        int pi = 0;
        while (true)
        {
            int liChild = 2 * pi + 1;
            if (liChild >= data.Count) break;
            int riChild = liChild + 1;
            int minChild = (riChild < data.Count && data[riChild].CompareTo(data[liChild]) < 0) ? riChild : liChild;
            if (data[pi].CompareTo(data[minChild]) <= 0) break;
            (data[pi], data[minChild]) = (data[minChild], data[pi]);
            pi = minChild;
        }
        return front;
    }
}

public class NodoHuffman : IComparable<NodoHuffman>
{
    public byte? ByteValue { get; set; }
    public int Frequency { get; set; }
    public NodoHuffman Left { get; set; }
    public NodoHuffman Right { get; set; }
    public bool IsLeaf => Left == null && Right == null;

    public int CompareTo(NodoHuffman other)
        => Frequency.CompareTo(other.Frequency);
}
