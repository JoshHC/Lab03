using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria_de_Clases
{
    public class NodoAVL<T>
    {
        public T Valor;
        public int FactorBalanceo;
        public NodoAVL<T> HijoIzquierdo, HijoDerecho;

        public bool EsHoja
        {
            get
            {
                return HijoDerecho == null && HijoIzquierdo == null;
            }
        }


        public NodoAVL(T valor)
        {
            this.Valor = valor;
            this.FactorBalanceo = 0;
            this.HijoIzquierdo = null;
            this.HijoDerecho = null;
        }
    }
}
