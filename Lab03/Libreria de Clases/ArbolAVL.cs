using System;
using System.Collections.Generic;

namespace Libreria_de_Clases
{
    public class ArbolAVL<T> where T : IComparable
        {
            public bool FechaoNumero;

            public NodoAVL<T> Raiz { get; set; }

            public ArbolAVL() { Raiz = null; }

            public void Insertar(T value)
            {
                NodoAVL<T> NewNode = new NodoAVL<T>(value);
                if (Raiz == null)
                {
                    Raiz = NewNode;
                }
                else
                {
                    InsertarHijo(NewNode, Raiz);
                }
            }

            private NodoAVL<T> InsertarHijo(NodoAVL<T> nNuevo, NodoAVL<T> nPadre)
            {
                if (nPadre != null)
                {
                    if (nNuevo.Value.CompareTo(nPadre.Value) <= 0)
                    {
                        if (nPadre.Izquierdo == null)
                        {
                            nPadre.Izquierdo = nNuevo;
                            return InsertarBalanceo(nPadre);
                        }
                        else
                        {
                            return InsertarHijo(nNuevo, nPadre.Izquierdo);
                        }
                    }
                    else
                    {
                        if (nNuevo.Value.CompareTo(nPadre.Value) > 0)
                        {
                            if (nPadre.Derecho == null)
                            {
                                nPadre.Derecho = nNuevo;
                                return InsertarBalanceo(nPadre);
                            }
                            else
                            {
                                return InsertarHijo(nNuevo, nPadre.Derecho);
                            }
                        }
                    }
                }
                return nPadre;
            }

            private NodoAVL<T> NodoMin(NodoAVL<T> Node)
            {
                NodoAVL<T> Aux = Node;

                while (Aux.Izquierdo != null)
                {
                    Aux = Aux.Izquierdo;
                }

                return Aux;
            }

            public NodoAVL<T> Eliminar(T valor)
            {
                NodoAVL<T> root = Raiz;
                NodoAVL<T> nPadre = Raiz;

                while (root.Value.CompareTo(valor) != 0)
                {
                    nPadre = root;
                    if (valor.CompareTo(root.Value) <= 0)
                    {
                        root = root.Izquierdo;
                    }
                    else
                    {
                        root = root.Izquierdo;
                    }

                    if (root == null)
                        return null;
                }

                if (root.Izquierdo == null || root.Derecho == null)
                {
                    NodoAVL<T> Aux;
                    if (root.Izquierdo != null)
                    {
                        Aux = root.Izquierdo;
                    }
                    else
                    {
                        Aux = root.Derecho;
                    }

                    if (Aux == null) //Sin hijos
                    {
                        Aux = root;
                        root = null;
                    }
                    else            //Un solo hijo
                    {
                        root = Aux;
                    }
                }
                else
                {
                    // El más a la izquierda del subarbol derecho
                    NodoAVL<T> Aux = NodoMin(root.Derecho);
                    root = Aux;
                    root.Derecho = Eliminar(root.Derecho.Value);
                }

                if (root == null)
                {
                    return root;
                }

                EliminarBalanceo(root);

                return root;
            }

            public NodoAVL<T> Editar(T valor)
            {
                NodoAVL<T> nAux = Raiz;
                NodoAVL<T> nPadre = Raiz;
                bool isLeftLeaf = true;

                while (nAux.Value.CompareTo(valor) != 0)
                {
                    nPadre = nAux;
                    if (valor.CompareTo(nAux.Value) <= 0)
                    {
                        isLeftLeaf = true;
                        nAux = nAux.Izquierdo;
                    }
                    else
                    {
                        isLeftLeaf = false;
                        nAux = nAux.Izquierdo;
                    }

                    if (nAux == null)
                        return null;
                }

                NodoAVL<T> nReplace = Reemplazar(nAux);
                if (nAux == Raiz)
                {
                    Raiz = nReplace;
                }
                else if (isLeftLeaf)
                {
                    nPadre.Izquierdo = nReplace;
                }
                else
                {
                    nPadre.Derecho = nReplace;
                }
                nReplace.Izquierdo = nAux.Izquierdo;

                return nReplace;

            }

            private NodoAVL<T> Reemplazar(NodoAVL<T> nElimina)
            {
                NodoAVL<T> rPadre = nElimina;
                NodoAVL<T> rReplace = nElimina;
                NodoAVL<T> Aux = nElimina.Derecho;
                while (Aux != null)
                {
                    rPadre = rReplace;
                    rReplace = Aux;
                    Aux = Aux.Izquierdo;
                }
                if (rReplace != nElimina.Derecho)
                {
                    rPadre.Izquierdo = rReplace.Derecho;
                    rReplace.Derecho = nElimina.Derecho;
                }
                return rReplace;
            }

            public NodoAVL<T> Buscar(T value)
            {
                NodoAVL<T> Aux = Raiz;
                while (Aux.Value.CompareTo(value) != 0)
                {
                    if (value.CompareTo(Aux.Value) < 0)
                    {
                        Aux = Aux.Izquierdo;
                    }
                    else
                    {
                        Aux = Aux.Derecho;
                    }

                    if (Aux == null)
                        return null;
                }

                return Aux;
            }

            bool Vacio()
            {
                return Raiz == null;
            }

            public bool Balanceado()
            {
                if (Raiz == null)
                    return true;

                return Balanceado(Raiz);
            }

            public bool Balanceado(NodoAVL<T> Node)
            {
                bool Valor;
                if (Node.Izquierdo == null && Node.Derecho != null)
                {
                    Valor = this.Balanceado(Node.Derecho) && (Math.Abs(0 - Altura(Node.Derecho)) <= 1);
                    return Valor;
                }
                else if (Node.Derecho == null && Node.Izquierdo != null)
                {
                    Valor = this.Balanceado(Node.Izquierdo) && (Math.Abs(Altura(Node.Izquierdo) - 0) <= 1);
                    return Valor;
                }
                else if (Node.Izquierdo == null && Node.Derecho == null)
                {
                    return true;
                }
                else
                {
                    Valor = this.Balanceado(Node.Izquierdo) && this.Balanceado(Node.Derecho) && (Math.Abs(Altura(Node.Izquierdo) - Altura(Node.Derecho)) <= 1);
                    return Valor;
                }
            }

            public NodoAVL<T> InsertarBalanceo(NodoAVL<T> Node)
            {
                int Balance = Balancear(Node);

                if (Balance > 1 && Balancear(Node.Izquierdo) == 1)
                {
                    Node.Izquierdo = RotacionIzquierda(Node.Izquierdo);
                    return RotacionDerecha(Node);
                }
                if (Balance < -1 && Balancear(Node.Derecho) == -1)
                {
                    Node.Derecho = RotacionDerecha(Node.Derecho);
                    return RotacionIzquierda(Node);
                }
                if (Balance > 1)
                {
                    return RotacionIzquierda(Node);
                }
                if (Balance < -1)
                {
                    return RotacionDerecha(Node);
                }
  
                return Node;
            }

            public NodoAVL<T> EliminarBalanceo(NodoAVL<T> Node)
            {
                int Balance = Balancear(Node);

                if (Balance > 1 && Balancear(Node.Izquierdo) >= 0)
                {
                    return RotacionDerecha(Node);
                }
                if (Balance > 1 && Balancear(Node.Izquierdo) < 0)
                {
                    Node.Izquierdo = RotacionIzquierda(Node.Izquierdo);
                    return RotacionDerecha(Node);
                }
                if (Balance < -1 && Balancear(Node.Derecho) <= 0)
                {
                    return RotacionIzquierda(Node);
                }
                if (Balance < -1 && Balancear(Node.Derecho) <= 0)
                {
                    return RotacionIzquierda(Node);
                }
                if (Balance < -1 && Balancear(Node.Derecho) > 0)
                {
                    Node.Derecho = RotacionDerecha(Node.Derecho);
                    return RotacionIzquierda(Node);
                }

                return Node;
            }

            private NodoAVL<T> RotacionDerecha(NodoAVL<T> Node)
            {

                NodoAVL<T> NewRoot = Node.Izquierdo;
                NodoAVL<T> Tree2 = NewRoot.Derecho;

                NewRoot.Derecho = Node;
                Node.Izquierdo = Tree2;

                return NewRoot;
            }

            private NodoAVL<T> RotacionIzquierda(NodoAVL<T> Node)
            {
                NodoAVL<T> NewRoot = Node.Derecho;
                NodoAVL<T> Tree2 = NewRoot.Izquierdo;

                NewRoot.Izquierdo = Node;
                Node.Derecho = Tree2;

                return NewRoot;
            }

            public int Balancear(NodoAVL<T> Node)
            {
                if (Node == null)
                {
                    return 0;
                }
                return Altura(Node.Izquierdo) - Altura(Node.Derecho);
            }

            private NodoAVL<T> HallarNodoDesbalanceado()
            {
                return HallarNodoDesbalanceado(Raiz);
            }

            private NodoAVL<T> HallarNodoDesbalanceado(NodoAVL<T> Node)
            {
                if (Node != null)
                {
                    int Balance = Math.Abs(Altura(Node.Izquierdo) - Altura(Node.Derecho));
                    if (Balance <= 1)
                    {
                        if (Node.Izquierdo != null)
                        {
                            return HallarNodoDesbalanceado(Node.Izquierdo);
                        }
                        else
                        {
                            return HallarNodoDesbalanceado(Node.Derecho);
                        }
                    }
                    else
                    {
                        return Node;
                    }
                }
                else
                {
                    return null;
                }

            }

            public int Altura(NodoAVL<T> Node)
            {
                if (Node == null)
                {
                    return -1;
                }
                else
                {
                    int LeftHeight = Altura(Node.Izquierdo);
                    int RightHeight = Altura(Node.Derecho);

                    if (LeftHeight > RightHeight)
                    {
                        return LeftHeight + 1;
                    }
                    else
                    {
                        return RightHeight + 1;
                    }
                }
            }

            private void InOrder(NodoAVL<T> Root, ref List<T> Elements)
            {
                if (Root != null)
                {
                    InOrder(Root.Izquierdo, ref Elements);
                    Elements.Add(Root.Value);
                    InOrder(Root.Derecho, ref Elements);
                }
            }

            private void PostOrder(NodoAVL<T> Root, ref List<T> Elements)
            {
                if (Root != null)
                {
                    PostOrder(Root.Izquierdo, ref Elements);
                    PostOrder(Root.Derecho, ref Elements);
                    Elements.Add(Root.Value);
                }
            }

            private void PreOrder(NodoAVL<T> Root, ref List<T> Elements)
            {
                if (Root != null)
                {
                    Elements.Add(Root.Value);
                    PreOrder(Root.Izquierdo, ref Elements);
                    PreOrder(Root.Derecho, ref Elements);
                }
            }

            public List<T> Orders(string Order)
            {
                List<T> Elements = new List<T>();
                switch (Order)
                {
                    case "PreOrder":
                        PreOrder(Raiz, ref Elements);
                        break;
                    case "InOrder":
                        InOrder(Raiz, ref Elements);
                        break;
                    case "PostOrder":
                        PostOrder(Raiz, ref Elements);
                        break;
                }
                return Elements;
            }


        }
    }
