using Compilador.Cache;
using Compilador.Transversal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compilador.AnalisisLexico
{
    public class AnalizadorLexico
    {
        private int NumeroLineaActual;
        private int Puntero;
        private string CaracterActual;
        private Linea lineaActual;
        string lexema;


        public AnalizadorLexico()
        {
            NumeroLineaActual = 0;
        }

        private void CargarNuevaLinea()
        {
            NumeroLineaActual++;
            lineaActual = Cache.Cache.ObtenerLinea(NumeroLineaActual);
            if (lineaActual.Contenido.Equals("@EOF@"))
            {
                NumeroLineaActual = lineaActual.Numero;
            }

            Puntero = 1;

        }

        private void DevolverPuntero()
        {
            Puntero = Puntero - 1;
        }

        public void LeerSiguienteCaracter()
        {
            if (lineaActual.Contenido.Equals("@EOF@"))
            {
                CaracterActual = lineaActual.Contenido;
            }
            else if (Puntero > lineaActual.Contenido.Length)
            {
                CaracterActual = "@FL@";
            }
            else
            {
                CaracterActual = lineaActual.Contenido.Substring(Puntero, 1);
                Puntero++;
            }
        }

        private void concatenarLexema()
        {
            lexema = lexema + CaracterActual;
        }

        private void limpiarLexema()
        {
            lexema = "";
        }

        private void DevorarEspaciosBlanco()
        {
            while (CaracterActual.Equals(" "))
            {
                LeerSiguienteCaracter();
            }
        }

        public bool EsLetra(string simbolo)
        {

            return Char.IsLetter(simbolo, 0);
        }

        public bool EsDigito(string simbolo)
        {

            return Char.IsDigit(simbolo, 0);
        }

        public bool EsLetraODigito(string simbolo)
        {

            return EsLetra(simbolo) || EsDigito(simbolo);
        }

        public ComponenteLexico Analizar()
        {

            ComponenteLexico componenteLexico = new ComponenteLexico();
            limpiarLexema();
            int estadoActual = 0;
            bool continuarAnalisis = true;
            while (continuarAnalisis)
            {

                if (estadoActual == 0)
                {
                    LeerSiguienteCaracter();
                    DevorarEspaciosBlanco();

                    if (EsLetra(CaracterActual) || CaracterActual.Equals("$") || CaracterActual.Equals("_"))
                    {
                        estadoActual = 4;
                        concatenarLexema();
                    }
                    else if (EsDigito(CaracterActual))
                    {
                        estadoActual = 1;
                        concatenarLexema();
                    }
                    else if (CaracterActual.Equals("+"))
                    {
                        estadoActual = 5;
                        concatenarLexema();
                    }
                    else if (CaracterActual.Equals("-"))
                    {
                        estadoActual = 6;
                        concatenarLexema();
                    }
                    else if (CaracterActual.Equals("*"))
                    {
                        estadoActual = 7;
                        concatenarLexema();
                    }
                    else if (CaracterActual.Equals("/"))
                    {
                        estadoActual = 8;
                        concatenarLexema();
                    }
                    else if (CaracterActual.Equals("("))
                    {
                        estadoActual = 10;
                        concatenarLexema();
                    }
                    else if (CaracterActual.Equals(")"))
                    {
                        estadoActual = 11;
                        concatenarLexema();
                    }
                    else if (CaracterActual.Equals("@EOF"))
                    {
                        estadoActual = 12;
                        concatenarLexema();
                    }
                    else if (CaracterActual.Equals("="))
                    {
                        estadoActual = 19;
                        concatenarLexema();
                    }
                    else if (CaracterActual.Equals("<"))
                    {
                        estadoActual = 20;
                        concatenarLexema();
                    }
                    else if (CaracterActual.Equals(">"))
                    {
                        estadoActual = 21;
                        concatenarLexema();
                    }
                    else if (CaracterActual.Equals(":"))
                    {
                        estadoActual = 22;
                        concatenarLexema();
                    }
                    else if (CaracterActual.Equals("!"))
                    {
                        estadoActual = 30;
                        concatenarLexema();
                    }
                    else if (CaracterActual.Equals("@FL@"))
                    {
                        estadoActual = 13;
                    }
                    else
                    {
                        estadoActual = 18;
                    }
                }
                else if (estadoActual == 1)
                {

                    LeerSiguienteCaracter();

                    if (EsDigito(CaracterActual))
                    {
                        estadoActual = 1;
                        concatenarLexema();
                    }
                    else if (CaracterActual.Equals(","))
                    {
                        estadoActual = 2;
                        concatenarLexema();
                    }
                    else
                    {
                        estadoActual = 14;
                    }

                }
                else if (estadoActual == 2)
                {
                    LeerSiguienteCaracter();
                    if (EsDigito(CaracterActual))
                    {
                        estadoActual = 3;
                        concatenarLexema();
                    }
                    else
                    {
                        estadoActual = 17;
                    }
                }
                else if (estadoActual == 3)
                {

                }
                else if (estadoActual == 17)
                {
                    //pendiente controlar estados de error
                }

                else if (estadoActual == 14)
                {
                    continuarAnalisis = false;
                    DevolverPuntero();
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.ENTERO;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }


            }

            return componenteLexico;
        }
    }
}
