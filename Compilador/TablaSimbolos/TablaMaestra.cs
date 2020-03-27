using Compilador.Transversal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compilador.TablaSimbolos
{
    public class TablaMaestra
    {
        public static void SincronizarSimbolo (ComponenteLexico componente)
        {
            if(componente != null)
            {
                switch(componente.Tipo)
                {
                    case TipoComponente.DUMMY:
                        TablaSimbolos.Agregar(componente);
                        break;
                    case TipoComponente.SIMBOLO:
                        TablaDummys.Agregar(componente);
                        break;
                }
            }
        }
    }
}
