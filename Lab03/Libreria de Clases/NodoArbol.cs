using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria_de_Clases
{
    public class NodoArbol<T>
    {
        public T valor;
        public NodoArbol<T> izquierdo;
        public NodoArbol<T> derecho;

        public NodoArbol(T Valor, NodoArbol<T> izquierda, NodoArbol<T> derecha)
        {
            this.valor = Valor;
            izquierdo = izquierda;
            derecho = derecha;
        }

        public bool EsHoja
        {
            get
            {
                return derecho == null && izquierdo == null;
            }
        }
    }
}
