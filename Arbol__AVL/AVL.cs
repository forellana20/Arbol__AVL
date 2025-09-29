using System;
using System.Drawing;

namespace SimuladorAVL
{
    public class AVL
    {
        public int valor;
        public AVL NodoIzquierdo;
        public AVL NodoDerecho;
        public int altura;

        private const int Radio = 30;
        private const int DistanciaH = 40;
        private const int DistanciaV = 10;

        private int CoordenadaX;
        private int CoordenadaY;

        // Constructor
        public AVL(int valor)
        {
            this.valor = valor;
            NodoIzquierdo = null;
            NodoDerecho = null;
            altura = 1;
        }

        // Método para el recorrido Pre-Orden (Raíz → Izquierdo → Derecho)
        public string RecorridoPreOrden(AVL nodo)
        {
            if (nodo == null) return "";
            return nodo.valor + " " + RecorridoPreOrden(nodo.NodoIzquierdo) + RecorridoPreOrden(nodo.NodoDerecho);
        }

        // Método para el recorrido En-Orden (Izquierdo → Raíz → Derecho)
        public string RecorridoEnOrden(AVL nodo)
        {
            if (nodo == null) return "";
            return RecorridoEnOrden(nodo.NodoIzquierdo) + nodo.valor + " " + RecorridoEnOrden(nodo.NodoDerecho);
        }

        // Método para el recorrido Post-Orden (Izquierdo → Derecho → Raíz)
        public string RecorridoPostOrden(AVL nodo)
        {
            if (nodo == null) return "";
            return RecorridoPostOrden(nodo.NodoIzquierdo) + RecorridoPostOrden(nodo.NodoDerecho) + nodo.valor + " ";
        }

        // Método para obtener la altura de un nodo
        public int ObtenerAltura(AVL nodo)
        {
            return nodo == null ? 0 : nodo.altura;
        }

        // Método para obtener el factor de balanceo de un nodo
        public int ObtenerBalance(AVL nodo)
        {
            return nodo == null ? 0 : ObtenerAltura(nodo.NodoIzquierdo) - ObtenerAltura(nodo.NodoDerecho);
        }

        // Método para insertar un valor en el árbol
        public AVL Insertar(AVL nodo, int valor, ref string rotacion)
        {
            if (nodo == null)
                return new AVL(valor);

            if (valor < nodo.valor)
                nodo.NodoIzquierdo = Insertar(nodo.NodoIzquierdo, valor, ref rotacion);
            else if (valor > nodo.valor)
                nodo.NodoDerecho = Insertar(nodo.NodoDerecho, valor, ref rotacion);
            else
                return nodo;

            nodo.altura = 1 + Math.Max(ObtenerAltura(nodo.NodoIzquierdo), ObtenerAltura(nodo.NodoDerecho));

            int balance = ObtenerBalance(nodo);

            // Rotaciones
            if (balance > 1 && valor < nodo.NodoIzquierdo.valor)
            {
                rotacion = "Rotación Simple Derecha (RSD)";
                return RotacionDerecha(nodo);
            }

            if (balance < -1 && valor > nodo.NodoDerecho.valor)
            {
                rotacion = "Rotación Simple Izquierda (RSI)";
                return RotacionIzquierda(nodo);
            }

            if (balance > 1 && valor > nodo.NodoIzquierdo.valor)
            {
                rotacion = "Rotación Doble Derecha (RDD)";
                nodo.NodoIzquierdo = RotacionIzquierda(nodo.NodoIzquierdo);
                return RotacionDerecha(nodo);
            }

            if (balance < -1 && valor < nodo.NodoDerecho.valor)
            {
                rotacion = "Rotación Doble Izquierda (RDI)";
                nodo.NodoDerecho = RotacionDerecha(nodo.NodoDerecho);
                return RotacionIzquierda(nodo);
            }

            return nodo;
        }

        //Metodo Buscar
        public bool buscar(int valorBuscar, AVL nodo)
        {
            if (nodo == null) return false;

            if (valorBuscar == nodo.valor) return true;

            if (valorBuscar < nodo.valor)
                return buscar(valorBuscar, nodo.NodoIzquierdo);

            return buscar(valorBuscar, nodo.NodoDerecho);
        }

        // Rotación simple a la derecha
        private AVL RotacionDerecha(AVL y)
        {
            AVL x = y.NodoIzquierdo;
            AVL T2 = x.NodoDerecho;

            x.NodoDerecho = y;
            y.NodoIzquierdo = T2;

            y.altura = Math.Max(ObtenerAltura(y.NodoIzquierdo), ObtenerAltura(y.NodoDerecho)) + 1;
            x.altura = Math.Max(ObtenerAltura(x.NodoIzquierdo), ObtenerAltura(x.NodoDerecho)) + 1;

            return x;
        }

        // Rotación simple a la izquierda
        private AVL RotacionIzquierda(AVL x)
        {
            AVL y = x.NodoDerecho;
            AVL T2 = y.NodoIzquierdo;

            y.NodoIzquierdo = x;
            x.NodoDerecho = T2;

            x.altura = Math.Max(ObtenerAltura(x.NodoIzquierdo), ObtenerAltura(x.NodoDerecho)) + 1;
            y.altura = Math.Max(ObtenerAltura(y.NodoIzquierdo), ObtenerAltura(y.NodoDerecho)) + 1;

            return y;
        }

        // Métodos de dibujo
        public void PosicionNodo(ref int xmin, int ymin)
        {
            int aux1, aux2;

            CoordenadaY = ymin + Radio / 2;

            if (NodoIzquierdo != null)
            {
                NodoIzquierdo.PosicionNodo(ref xmin, ymin + Radio + DistanciaV);
            }

            if (NodoIzquierdo != null && NodoDerecho != null)
            {
                xmin += DistanciaH;
            }

            if (NodoDerecho != null)
            {
                NodoDerecho.PosicionNodo(ref xmin, ymin + Radio + DistanciaV);
            }

            if (NodoIzquierdo != null && NodoDerecho != null)
            {
                CoordenadaX = (NodoIzquierdo.CoordenadaX + NodoDerecho.CoordenadaX) / 2;
            }
            else if (NodoIzquierdo != null)
            {
                CoordenadaX = NodoIzquierdo.CoordenadaX + DistanciaH / 2;
            }
            else if (NodoDerecho != null)
            {
                CoordenadaX = NodoDerecho.CoordenadaX - DistanciaH / 2;
            }
            else
            {
                CoordenadaX = xmin + Radio / 2;
                xmin += Radio;
            }
        }

        public void DibujarRamas(Graphics grafo, Pen Lapiz)
        {
            if (NodoIzquierdo != null)
            {
                grafo.DrawLine(Lapiz, CoordenadaX, CoordenadaY, NodoIzquierdo.CoordenadaX, NodoIzquierdo.CoordenadaY);
                NodoIzquierdo.DibujarRamas(grafo, Lapiz);
            }

            if (NodoDerecho != null)
            {
                grafo.DrawLine(Lapiz, CoordenadaX, CoordenadaY, NodoDerecho.CoordenadaX, NodoDerecho.CoordenadaY);
                NodoDerecho.DibujarRamas(grafo, Lapiz);
            }
        }

        public void DibujarNodo(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, int dato, Brush encuentro)
        {
            Rectangle rect = new Rectangle(CoordenadaX - Radio / 2, CoordenadaY - Radio / 2, Radio, Radio);

            if (valor == dato)
            {
                grafo.FillEllipse(encuentro, rect);
            }
            else
            {
                grafo.FillEllipse(Relleno, rect);
            }

            grafo.DrawEllipse(Lapiz, rect);

            StringFormat formato = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            grafo.DrawString(valor.ToString(), fuente, RellenoFuente, CoordenadaX, CoordenadaY, formato);

            // Dibuja el Factor de Balanceo (FB)
            int balance = ObtenerBalance(this);
            grafo.DrawString("FB:" + balance.ToString(), new Font("Arial", 8), Brushes.Red, CoordenadaX - 15, CoordenadaY - 25);
        }
    }
}