using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria_de_Clases
{
    public class AVL<T> : IEnumerable<T> where T : IComparable
    {
        public T valor;
        public AVL<T> NodoIzquierdo;
        public AVL<T> NodoDerecho;
        public AVL<T> NodoPadre;
        public int altura;
        int iElementos;

        public AVL()
        {
            this.NodoPadre = null;
            this.iElementos = 0;

        }

        // Constructor.
        public AVL(T valorNuevo, AVL<T> izquierdo, AVL<T> derecho, AVL<T> padre)
        {
            valor = valorNuevo;
            NodoIzquierdo = izquierdo;
            NodoDerecho = derecho;
            NodoPadre = padre;
            altura = 0;
        }

        //Funcion para insertar un nuevo valor en el arbol AVL
        public AVL<T> Insertar(T valorNuevo, AVL<T> Raiz)
        {
            if (Raiz == null)
                Raiz = new AVL<T>(valorNuevo,null,null,null);
            else
            if
            (valorNuevo.CompareTo(Raiz.valor) < 0)
            {
                Raiz.NodoIzquierdo = Insertar(valorNuevo, Raiz.NodoIzquierdo);
            }
            else
            if
            (valorNuevo.CompareTo(Raiz.valor) > 0)
            {
                Raiz.NodoDerecho = Insertar(valorNuevo, Raiz.NodoDerecho);
            }
            else
            {
                //CREAR FUNCION DE ERROR
               // MessageBox.Show("Valor Existente en el Arbol","Error",MessageBoxButtons.OK);
            }
            //Realiza las rotaciones simples o dobles segun el caso
            if
            (Alturas(Raiz.NodoIzquierdo) - Alturas(Raiz.NodoDerecho) == 2)
            {
                if
                (valorNuevo.CompareTo(Raiz.NodoIzquierdo.valor) < 0)
                    Raiz = RotacionIzquierdaSimple(Raiz);
                else
                    Raiz = RotacionIzquierdaDoble(Raiz);
            }
            if
            (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) == 2)
            {
                if
                (valorNuevo.CompareTo(Raiz.NodoDerecho.valor) > 0)
                    Raiz = RotacionDerechaSimple(Raiz);
                else
                    Raiz = RotacionDerechaDoble(Raiz);
            }
            Raiz.altura = max(Alturas(Raiz.NodoIzquierdo), Alturas(Raiz.NodoDerecho)) + 1;
            return Raiz;
        }

        //FUNCION DE PRUEBA PARA REALIZAR LAS ROTACIONES
        //Función para obtener que rama es mayor

        private static int max(int lhs, int rhs)
        {
            return
            lhs > rhs ? lhs : rhs;
        }
        private static int Alturas(AVL<T> Raiz)
        {
            return
            Raiz ==
            null
            ?
            -
            1 : Raiz.altura;
        }

        AVL<T> nodoE, nodoP;

        public AVL<T> Eliminar(T valorEliminar, ref AVL<T> Raiz)
        {
            if(Raiz != null)
        
        {
                if
                (valorEliminar.CompareTo(Raiz.valor) < 0)
                {
                    nodoE = Raiz;
                    Eliminar(valorEliminar, ref Raiz.NodoIzquierdo);
                }
                else
                {
                    if
                    (valorEliminar.CompareTo(Raiz.valor) > 0)
                    {
                        nodoE = Raiz;
                        Eliminar(valorEliminar, ref Raiz.NodoDerecho);
                    }
                        else
                        {
                        //Posicionado sobre el elemento a eliminar
                        AVL<T> NodoEliminar = Raiz;
                        if
                        (NodoEliminar.NodoDerecho == null)
                        {
                            Raiz = NodoEliminar.NodoIzquierdo;
                            if
                            (Alturas(nodoE.NodoIzquierdo) - Alturas(nodoE.NodoDerecho) == 2)
                            {
                                //MessageBox.Show("nodoE" + nodoE.valor.ToString());
                                if
                                (valorEliminar.CompareTo(nodoE.valor) < 0)
                                nodoP = RotacionIzquierdaSimple(nodoE);
                                else
                                    nodoE = RotacionDerechaSimple(nodoE);
                            }
                            if
                            (Alturas(nodoE.NodoDerecho) - Alturas(nodoE.NodoIzquierdo) == 2)
                            {
                                if
                                (valorEliminar.CompareTo(nodoE.NodoDerecho.valor) > 0)
                                nodoE = RotacionDerechaSimple(nodoE);
                                else
                                nodoE = RotacionDerechaDoble(nodoE);
                                nodoP = RotacionDerechaSimple(nodoE);
                            }
                        }
                        else
                        {
                            if
                            (NodoEliminar.NodoIzquierdo == null)
                            {
                                Raiz = NodoEliminar.NodoDerecho;
                            }
                            else
                            {
                                if
                                (Alturas(Raiz.NodoIzquierdo) - Alturas(Raiz.NodoDerecho) > 0)
                                {
                                    AVL<T> AuxiliarNodo = null;
                                    AVL<T> Auxiliar = Raiz.NodoIzquierdo;
                                    bool Bandera = false;
                                    while
                                    (Auxiliar.NodoDerecho != null)
                                    {
                                        AuxiliarNodo = Auxiliar;
                                        Auxiliar = Auxiliar.NodoDerecho;
                                        Bandera = true;
                                    }
                                    Raiz.valor = Auxiliar.valor;
                                    NodoEliminar = Auxiliar;
                                    if (Bandera == true)
                                    {
                                        AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                    }
                                    else
                                    {
                                        Raiz.NodoIzquierdo = Auxiliar.NodoIzquierdo;
                                    }
                                    //Realiza las rotaciones simples o dobles segun el caso
                                }
                                else
                                {
                                    if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) > 0)
                                    {
                                        AVL<T> AuxiliarNodo = null;
                                        AVL<T> Auxiliar = Raiz.NodoDerecho;
                                        bool Bandera = false;
                                        while(Auxiliar.NodoIzquierdo != null)
                                        {
                                            AuxiliarNodo = Auxiliar;
                                            Auxiliar = Auxiliar.NodoIzquierdo;
                                            Bandera = true;
                                        }
                                        Raiz.valor = Auxiliar.valor;
                                        NodoEliminar = Auxiliar;
                                        if (Bandera == true)
                                        {
                                            AuxiliarNodo.NodoIzquierdo = Auxiliar.NodoDerecho;
                                        }
                                        else
                                        {
                                            Raiz.NodoDerecho = Auxiliar.NodoDerecho;
                                        }
                                    }
                                    else
                                    {
                                        if (Alturas(Raiz.NodoDerecho)-Alturas(Raiz.NodoIzquierdo) == 0)
                                        {
                                            AVL<T> AuxiliarNodo = null;
                                            AVL<T> Auxiliar = Raiz.NodoIzquierdo;
                                            bool Bandera = false;
                                            while (Auxiliar.NodoDerecho != null)
                                            {
                                                AuxiliarNodo = Auxiliar;
                                                Auxiliar = Auxiliar.NodoDerecho;
                                                Bandera = true;
                                            }
                                            Raiz.valor = Auxiliar.valor;
                                            NodoEliminar = Auxiliar;
                                            if
                                            (Bandera == true)
                                            {
                                                AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                            }
                                            else
                                            {
                                                Raiz.NodoIzquierdo = Auxiliar.NodoIzquierdo;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else

            {
               // MessageBox.Show("Nodo inexistente en el arbol","Error", MessageBoxButtons.OK);

            }
            return
            nodoP;
        }
        //Seccion de funciones de rotacion

        //Rotacion Izquierda Simple
        private static AVL<T> RotacionIzquierdaSimple(AVL<T> k2)
        {
            AVL<T> k1 = k2.NodoIzquierdo;
            k2.NodoIzquierdo = k1.NodoDerecho;
            k1.NodoDerecho = k2;
            k2.altura = max(Alturas(k2.NodoIzquierdo), Alturas(k2.NodoDerecho)) + 1;
            k1.altura = max(Alturas(k1.NodoIzquierdo), k2.altura) + 1;
            return k1;
        }
        //Rotacion Derecha Simple
        private static AVL<T> RotacionDerechaSimple(AVL<T> k1)
        {
            AVL<T> k2 = k1.NodoDerecho;
            k1.NodoDerecho = k2.NodoIzquierdo;
            k2.NodoIzquierdo = k1;
            k1.altura = max(Alturas (k1.NodoIzquierdo), Alturas(k1.NodoDerecho)) + 1;
            k2.altura = max(Alturas(k2.NodoDerecho), k1.altura) + 1;
            return k2;
        }
        //Doble Rotacion Izquierda
        private static AVL<T> RotacionIzquierdaDoble(AVL<T> k3)
        {
            k3.NodoIzquierdo = RotacionDerechaSimple(k3.NodoIzquierdo);
            return RotacionIzquierdaSimple(k3);
        }
        //Doble Rotacion Derecha
        private static AVL<T> RotacionDerechaDoble(AVL<T> k1)
        {
            k1.NodoDerecho = RotacionIzquierdaSimple(k1.NodoDerecho);
            return RotacionDerechaSimple(k1);
        }
        //Funcion para obtener la altura del arbol
        public int getAltura(AVL<T> nodoActual)
        {
            if (nodoActual == null)
                return 0;
            else
                return
                1 + Math.Max(getAltura(nodoActual.NodoIzquierdo),getAltura(nodoActual.NodoDerecho));
        }

        //Buscar un valor en el arbol
        public void buscar(T valorBuscar,AVL<T> Raiz)
        {
            if
            (Raiz != null)
            {
                if
                (valorBuscar.CompareTo(Raiz.valor) < 0)
                {
                    buscar(valorBuscar, Raiz.NodoIzquierdo);
                }
                else
                {
                    if
                    (valorBuscar.CompareTo(Raiz.valor) > 0)
                    {
                        buscar(valorBuscar, Raiz.NodoDerecho);
                    }
                }
            }
            else
            {
                // MessageBox.Show("Valor no encontrado","Error",MessageBoxButtons.OK);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            AVL<T> current = NodoPadre;
            int i = 0;
            while (current != null && i < iElementos)
            {
                yield return current.valor;
                current = current.NodoIzquierdo;
                current = current.NodoDerecho;
                i++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
