using System;
using System.Windows.Forms;

namespace SimuladorAVL
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicaci�n.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // Aseg�rate de que aqu� se use "Form1"
        }
    }
}