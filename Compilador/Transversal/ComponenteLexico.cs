using System;
using System.Collections.Generic;
using System.Text;

namespace Compilador.Transversal
{
    public class ComponenteLexico
    {
        public string Lexema { get; set; }
        public Categoria Categoria { get; set; }
        public int NumeroLinea { get; set; }
        public int PosicionInicial { get; set; }
        public int PosicionFinal { get; set; }
        public TipoComponente Tipo { get; set;  }


    }
}
