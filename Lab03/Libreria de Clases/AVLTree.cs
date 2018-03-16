using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria_de_Clases
{
    class AVLTree<T> : IEnumerable<T> where T : IComparable
    {

        public NodoArbol<T> nRaiz;
        int iElementos;

        public AVLTree()
        {
            this.nRaiz = null;
            this.iElementos = 0;
        }


        public void Insertar(NodoArbol<T> nNuevo)
        {
            if (nRaiz == null)
            {
                nRaiz = nNuevo;
            }
            else
            {
                NodoArbol<T> nodoAuxiliar = nRaiz;
                NodoArbol<T> nodoPadre = nRaiz;
                bool bIzquierda = false;

                while (nodoAuxiliar != null)
                {
                    nodoPadre = nodoAuxiliar;

                    if (nNuevo.valor.CompareTo(nodoAuxiliar.valor) < 0)
                    {
                        nodoAuxiliar = nodoAuxiliar.izquierdo;
                        bIzquierda = true;
                    }
                    else if (nNuevo.valor.CompareTo(nodoAuxiliar.valor) > 0)
                    {
                        nodoAuxiliar = nodoAuxiliar.derecho;
                        bIzquierda = false;
                    }
                    else
                        return;
                }

                if (bIzquierda)
                    nodoPadre.izquierdo = nNuevo;
                else
                    nodoPadre.derecho = nNuevo;

            }
        }

        public NodoArbol<T> Eliminar(T valor)
        {
            NodoArbol<T> auxiliar = nRaiz;
            NodoArbol<T> padre = nRaiz;
            bool esHijoIzquierdo = true;

            while (valor.CompareTo(auxiliar.valor) != 0)
            {
                padre = auxiliar;

                if (valor.CompareTo(auxiliar.valor) < 0)
                {
                    esHijoIzquierdo = true;
                    auxiliar = auxiliar.izquierdo;
                }
                else
                {
                    esHijoIzquierdo = false;
                    auxiliar = auxiliar.derecho;
                }
                if (auxiliar == null)
                {
                    return null;
                }
            }// Fin ciclo inicial

            if (auxiliar.EsHoja)
            {
                if (auxiliar == nRaiz)
                {
                    nRaiz = null;
                }
                else if (esHijoIzquierdo)
                {
                    padre.izquierdo = null;
                }
                else
                {
                    padre.derecho = null;
                }
            }
            else if (auxiliar.derecho == null)
            {
                NodoArbol<T> temp = auxiliar.izquierdo;
                if (auxiliar == nRaiz)
                {
                    nRaiz = temp;
                }
                else if (esHijoIzquierdo)
                {
                    padre.izquierdo = temp;
                }
                else
                {
                    padre.derecho = temp;
                }
            }
            else if (auxiliar.izquierdo == null)
            {
                NodoArbol<T> temp = auxiliar.derecho;
                if (auxiliar == nRaiz)
                {
                    nRaiz = temp;
                }
                else if (esHijoIzquierdo)
                {
                    padre.izquierdo = temp;
                }
                else
                {
                    padre.derecho = temp;
                }
            }
            else
            {
                NodoArbol<T> reemplazo = Reemplazar(auxiliar);
                if (auxiliar == nRaiz)
                {
                    nRaiz = reemplazo;
                }
                else if (esHijoIzquierdo)
                {
                    padre.izquierdo = reemplazo;
                }
                else
                {
                    padre.derecho = reemplazo;
                }
                reemplazo.izquierdo = auxiliar.izquierdo;

            }
            return auxiliar;
        }

        /// <summary>
        /// Elimina un Nodo mediante sustitucion
        /// </summary>
        /// <param name="NodoAEliminar">Nodo a Eliminar </param>
        /// <returns>Nodo de Reemplazo</returns>
        private NodoArbol<T> Reemplazar(NodoArbol<T> NodoAEliminar)
        {
            NodoArbol<T> remplazoPadre = NodoAEliminar;
            NodoArbol<T> reemplazo = NodoAEliminar;
            NodoArbol<T> auxiliar = NodoAEliminar.derecho;
            while (auxiliar != null)
            {
                remplazoPadre = reemplazo;
                reemplazo = auxiliar;
                auxiliar = auxiliar.izquierdo;
            }
            if (reemplazo != NodoAEliminar.derecho)
            {
                remplazoPadre.izquierdo = reemplazo.derecho;
                reemplazo.derecho = NodoAEliminar.derecho;
            }
            return reemplazo;
        }


        private void PreOrden(NodoArbol<T> nodoAuxiliar, ref List<T> resultado)
        {
            if (nodoAuxiliar != null)
            {
                resultado.Add(nodoAuxiliar.valor);
                PreOrden(nodoAuxiliar.izquierdo, ref resultado);
                PreOrden(nodoAuxiliar.derecho, ref resultado);
            }
        }

        private void InOrden(NodoArbol<T> nodoAuxiliar, ref List<T> resultado)
        {
            if (nodoAuxiliar != null)
            {
                InOrden(nodoAuxiliar.izquierdo, ref resultado);
                resultado.Add(nodoAuxiliar.valor);
                InOrden(nodoAuxiliar.derecho, ref resultado);
            }
        }

        private void PostOrden(NodoArbol<T> nodoAuxiliar, ref List<T> resultado)
        {
            if (nodoAuxiliar != null)
            {
                PostOrden(nodoAuxiliar.izquierdo, ref resultado);
                PostOrden(nodoAuxiliar.derecho, ref resultado);
                resultado.Add(nodoAuxiliar.valor);
            }
        }

        /// <summary>
        /// Muestra el arbol en los distintos ordenes
        /// </summary>
        /// <param name="code">1. Pre-Orden  2. In-Orden  3. Post-Orden</param>
        /// <param name="dt">DataTable donde se almacena la informacion de los nodos</param>
        public List<T> LeerArbol(int code)
        {
            List<T> resultado = new List<T>();
            switch (code)
            {
                case 1: PreOrden(nRaiz, ref resultado); break;
                case 2: InOrden(nRaiz, ref resultado); break;
                case 3: PostOrden(nRaiz, ref resultado); break;
            }

            return resultado;
        }

        public string Degenerado()
        {
            NodoArbol<T> auxiliar = nRaiz;
            bool Degenerado = false;

            while (auxiliar != null)
            {
                if (auxiliar.derecho == null)
                {
                    auxiliar = auxiliar.izquierdo;
                    Degenerado = true;
                }
                else if (auxiliar.izquierdo == null)
                {
                    auxiliar = auxiliar.derecho;
                    Degenerado = true;
                }
                else
                {
                    Degenerado = false;
                    break;
                }

            }

            if (nRaiz.izquierdo == null || nRaiz.derecho == null)
            {
                Degenerado = true;
            }

            if (Degenerado == false)
            {
                return "Generado";
            }
            else
            {
                return "Degenerado";
            }

        }

        public string Balanceado()
        {
            NodoArbol<T> auxiliar = nRaiz;
            NodoArbol<T> NodoNoBalanceadoI = nRaiz;
            NodoArbol<T> NodoNoBalanceadoD = nRaiz;
            int ContadorIzquierda = 0;
            int ContadorDerecha = 0;

            while (auxiliar != null)
            {
                if (nRaiz.izquierdo == null || nRaiz.derecho == null)
                {
                    ContadorIzquierda = -1;
                    break;
                }
                if (auxiliar.izquierdo == null)
                {
                    if (auxiliar.derecho != null)
                    {
                        auxiliar = auxiliar.derecho;
                        NodoNoBalanceadoI = auxiliar;
                        ContadorIzquierda++;
                    }
                    else
                    {
                        auxiliar = auxiliar.derecho;
                    }
                }
                else
                {
                    auxiliar = auxiliar.izquierdo;
                    if (auxiliar != null)
                    {
                        NodoNoBalanceadoI = auxiliar;
                    }
                    ContadorIzquierda++;
                }
            }

            auxiliar = nRaiz;
            while (auxiliar != null)
            {
                if (nRaiz.izquierdo == null || nRaiz.derecho == null)
                {
                    ContadorDerecha = -1;
                    break;
                }
                if (auxiliar.derecho == null)
                {
                    if (auxiliar.izquierdo != null)
                    {
                        auxiliar = auxiliar.izquierdo;
                        NodoNoBalanceadoD = auxiliar;
                        ContadorDerecha++;
                    }
                    else
                    {
                        auxiliar = auxiliar.izquierdo;
                    }
                }
                else
                {
                    auxiliar = auxiliar.derecho;
                    if (auxiliar != null)
                    {
                        NodoNoBalanceadoD = auxiliar;
                    }
                    ContadorDerecha++;
                }
            }

            if ((ContadorDerecha == ContadorIzquierda && (ContadorDerecha != -1 || ContadorIzquierda != -1)))
            {
                return "Balanceado";
            }
            else if (ContadorDerecha == -1 || ContadorIzquierda == -1)
            {
                return "No Balanceado, el Árbol no se encuentra balanceado porque el nodo raiz, solo posee un nodo, ya sea izquierdo o Derecho";
            }
            else if (ContadorDerecha > ContadorIzquierda)
            {
                if ((ContadorDerecha - ContadorIzquierda) <= 1)
                {
                    return "Balanceado";
                }
                else
                {
                    return "No Balanceado, El nodo que no se encuentra balanceado es: " + NodoNoBalanceadoI.valor;
                }

            }
            else if (ContadorIzquierda > ContadorDerecha)
            {
                if ((ContadorIzquierda - ContadorDerecha) <= 1)
                {
                    return "Balanceado";
                }
                else
                {
                    return "No Balanceado, El nodo que no se encuentra balanceado es: " + NodoNoBalanceadoD.valor;
                }

            }

            return "Error";

        }



        public IEnumerator<T> GetEnumerator()
        {
            NodoArbol<T> current = nRaiz;
            int i = 0;
            while (current != null && i < iElementos)
            {
                yield return current.valor;
                current = current.izquierdo;
                current = current.derecho;
                i++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

    }

