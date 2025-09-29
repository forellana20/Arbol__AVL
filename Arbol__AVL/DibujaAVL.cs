using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimuladorAVL
{
    public class DibujaAVL
    {
        public AVL Raiz;

        // Constructor principal
        public DibujaAVL()
        {
            Raiz = null;
        }

        // Método para insertar un valor en el árbol
        public string Insertar(int valor)
        {
            string rotacion = "";
            Raiz = Raiz == null ? new AVL(valor) : Raiz.Insertar(Raiz, valor, ref rotacion);
            return rotacion; // Devuelve el tipo de rotación realizada
        }

        // Método para eliminar un valor del árbol
        public void Eliminar(int valor)
        {
            if (Raiz == null)
            {
                MessageBox.Show("El árbol está vacío", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Raiz = EliminarNodo(Raiz, valor);
        }

        private AVL EliminarNodo(AVL nodo, int valor)
        {
            if (nodo == null)
                return nodo;

            if (valor < nodo.valor)
                nodo.NodoIzquierdo = EliminarNodo(nodo.NodoIzquierdo, valor);
            else if (valor > nodo.valor)
                nodo.NodoDerecho = EliminarNodo(nodo.NodoDerecho, valor);
            else
            {
                if (nodo.NodoIzquierdo == null || nodo.NodoDerecho == null)
                {
                    AVL temp = nodo.NodoIzquierdo ?? nodo.NodoDerecho;
                    nodo = temp;
                }
                else
                {
                    AVL temp = ObtenerMinimo(nodo.NodoDerecho);
                    nodo.valor = temp.valor;
                    nodo.NodoDerecho = EliminarNodo(nodo.NodoDerecho, temp.valor);
                }
            }

            if (nodo == null)
                return nodo;

            nodo.altura = 1 + Math.Max(Raiz.ObtenerAltura(nodo.NodoIzquierdo), Raiz.ObtenerAltura(nodo.NodoDerecho));

            int balance = Raiz.ObtenerBalance(nodo);

            if (balance > 1 && Raiz.ObtenerBalance(nodo.NodoIzquierdo) >= 0)
                return RotacionDerecha(nodo);

            if (balance > 1 && Raiz.ObtenerBalance(nodo.NodoIzquierdo) < 0)
            {
                nodo.NodoIzquierdo = RotacionIzquierda(nodo.NodoIzquierdo);
                return RotacionDerecha(nodo);
            }

            if (balance < -1 && Raiz.ObtenerBalance(nodo.NodoDerecho) <= 0)
                return RotacionIzquierda(nodo);

            if (balance < -1 && Raiz.ObtenerBalance(nodo.NodoDerecho) > 0)
            {
                nodo.NodoDerecho = RotacionDerecha(nodo.NodoDerecho);
                return RotacionIzquierda(nodo);
            }

            return nodo;
        }

        private AVL ObtenerMinimo(AVL nodo)
        {
            AVL actual = nodo;
            while (actual.NodoIzquierdo != null)
                actual = actual.NodoIzquierdo;
            return actual;
        }

        private AVL RotacionDerecha(AVL y)
        {
            AVL x = y.NodoIzquierdo;
            AVL T2 = x.NodoDerecho;

            x.NodoDerecho = y;
            y.NodoIzquierdo = T2;

            y.altura = Math.Max(Raiz.ObtenerAltura(y.NodoIzquierdo), Raiz.ObtenerAltura(y.NodoDerecho)) + 1;
            x.altura = Math.Max(Raiz.ObtenerAltura(x.NodoIzquierdo), Raiz.ObtenerAltura(x.NodoDerecho)) + 1;

            return x;
        }

        private AVL RotacionIzquierda(AVL x)
        {
            AVL y = x.NodoDerecho;
            AVL T2 = y.NodoIzquierdo;

            y.NodoIzquierdo = x;
            x.NodoDerecho = T2;

            x.altura = Math.Max(Raiz.ObtenerAltura(x.NodoIzquierdo), Raiz.ObtenerAltura(x.NodoDerecho)) + 1;
            y.altura = Math.Max(Raiz.ObtenerAltura(y.NodoIzquierdo), Raiz.ObtenerAltura(y.NodoDerecho)) + 1;

            return y;
        }

        // Método para dibujar el árbol
        public void DibujarArbol(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, int dato, Brush encuentro)
        {
            int x = 100;
            int y = 75;
            if (Raiz == null) return;

            Raiz.PosicionNodo(ref x, y);

            Raiz.DibujarRamas(grafo, Lapiz);

            Raiz.DibujarNodo(grafo, fuente, Relleno, RellenoFuente, Lapiz, dato, encuentro);
        }

        // Métodos para los recorridos
        public string RecorridoPreOrden()
        {
            return Raiz == null ? "Árbol vacío" : Raiz.RecorridoPreOrden(Raiz).Trim();
        }

        public string RecorridoEnOrden()
        {
            return Raiz == null ? "Árbol vacío" : Raiz.RecorridoEnOrden(Raiz).Trim();
        }

        public string RecorridoPostOrden()
        {
            return Raiz == null ? "Árbol vacío" : Raiz.RecorridoPostOrden(Raiz).Trim();
        }
    }
}