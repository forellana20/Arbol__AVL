using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimuladorAVL
{
    public partial class Form1 : Form
    {
        private DibujaAVL arbolAVL = new DibujaAVL();
        private Graphics g;

        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g = e.Graphics;

            arbolAVL.DibujarArbol(g, this.Font, Brushes.White, Brushes.Black, Pens.Black, 0, Brushes.Red);
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtValor.Text == "")
            {
                errorProvider1.SetError(txtValor, "Debe ingresar un valor.");
                return;
            }

            try
            {
                int valor = int.Parse(txtValor.Text);
                string rotacion = arbolAVL.Insertar(valor); // Inserta y obtiene el tipo de rotación
                if (!string.IsNullOrEmpty(rotacion))
                {
                    MessageBox.Show(rotacion, "Rotación Realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                txtValor.Clear();
                lblAltura.Text = arbolAVL.Raiz.ObtenerAltura(arbolAVL.Raiz).ToString();
                this.Refresh();
            }
            catch (Exception)
            {
                errorProvider1.SetError(txtValor, "Debe ingresar un número válido.");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtValor.Text == "")
            {
                errorProvider1.SetError(txtValor, "Debe ingresar un valor.");
                return;
            }

            try
            {
                int valor = int.Parse(txtValor.Text);
                bool encontrado = arbolAVL.Raiz != null && arbolAVL.Raiz.buscar(valor, arbolAVL.Raiz);
                if (encontrado)
                {
                    MessageBox.Show($"El valor {valor} fue encontrado en el árbol.", "Búsqueda Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"El valor {valor} no se encuentra en el árbol.", "Búsqueda Fallida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                txtValor.Clear();
            }
            catch (Exception)
            {
                errorProvider1.SetError(txtValor, "Debe ingresar un número válido.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtValor.Text == "")
            {
                errorProvider1.SetError(txtValor, "Debe ingresar un valor.");
                return;
            }

            try
            {
                int valor = int.Parse(txtValor.Text);
                arbolAVL.Eliminar(valor);
                txtValor.Clear();
                lblAltura.Text = arbolAVL.Raiz == null ? "0" : arbolAVL.Raiz.ObtenerAltura(arbolAVL.Raiz).ToString();
                this.Refresh();
            }
            catch (Exception)
            {
                errorProvider1.SetError(txtValor, "Debe ingresar un número válido.");
            }
        }

        private void rbPreOrden_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPreOrden.Checked)
            {
                MessageBox.Show(arbolAVL.RecorridoPreOrden(), "Recorrido Pre-Orden", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rbEnOrden_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnOrden.Checked)
            {
                MessageBox.Show(arbolAVL.RecorridoEnOrden(), "Recorrido En-Orden", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rbPostOrden_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPostOrden.Checked)
            {
                MessageBox.Show(arbolAVL.RecorridoPostOrden(), "Recorrido Post-Orden", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}