using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria_de_Clases
{
    public class NodoAVL<T>
    {
        public T Value { get; set; }
        public NodoAVL<T> Izquierdo { get; set; }
        public NodoAVL<T> Derecho { get; set; }
        public int LongitudIzquierda { get; set; }
        public int LongitudDerecha { get; set; }

        public NodoAVL(T value, NodoAVL<T> left, NodoAVL<T> right, int leftSize, int rightSize)
        {
            Value = value;
            Izquierdo = left;
            Derecho = right;
            LongitudIzquierda = leftSize;
            LongitudDerecha = rightSize;
        }

        public NodoAVL(T value) : this(value, null, null, 0, 0) { }

        public NodoAVL() { }

        public bool EsHoja() { return Izquierdo == null && Derecho == null; }

        public bool Lleno() { return Izquierdo != null && Derecho != null; }

        public bool Degenerado()
        {
            if (this.Izquierdo != null)
            {
                if (this.Derecho != null)
                {
                    return false; // Un nodo fue encontrado con dos hijos
                }
                else
                {
                    return this.Izquierdo.Degenerado();
                }
            }
            else
            {
                if (this.Derecho != null)
                {
                    return this.Derecho.Degenerado();
                }
                else
                {
                    return true; // Ningún nodo tiene dos hijos
                }
            }
        }

    }

}
