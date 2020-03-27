using Compilador.AnalisisLexico;
using Compilador.Transversal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //boton compilar
        public void  CompilarButton_Click()
        {
            AnalizadorLexico anaLex = new AnalizadorLexico();
            ComponenteLexico componente = anaLex.Analizar();

            while (componente.Lexema.Equals("@EOF@"))
            {
                MessageBox.Show(componente.ToString());
            }

            //pintar tabla de simbolos, dummys y errores

        }
    }
}
