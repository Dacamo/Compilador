using Compilador.Cache;
using Compilador.Transversal;
using System;


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
                    LeerSiguienteCaracter();
                    if (EsDigito(CaracterActual))
                    {
                        estadoActual = 3;
                        concatenarLexema();
                    }
                    else
                    {
                        estadoActual = 15;
                    }

                }
                else if (estadoActual == 4) 
                {
                    LeerSiguienteCaracter();
                    if (EsLetraODigito(CaracterActual) || CaracterActual.Equals("$") || CaracterActual.Equals("_"))
                    {
                        estadoActual = 4;
                        concatenarLexema();
                    }
                    else
                    {
                        estadoActual = 16;
                    }
                }
                else if (estadoActual == 5)
                {
                    continuarAnalisis = false;
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.SUMA;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                else if (estadoActual == 6)
                {
                    continuarAnalisis = false;
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.RESTA;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                else if (estadoActual == 7)
                {
                    continuarAnalisis = false;
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.MULTIPLICACION;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                else if (estadoActual == 8)
                {
                    LeerSiguienteCaracter();
                    if (CaracterActual.Equals("*"))
                    {
                        estadoActual = 34;
                        concatenarLexema();
                    }
                    else if (CaracterActual.Equals("/"))
                    {
                        estadoActual = 36;
                        concatenarLexema();
                    }
                    else
                    {
                        estadoActual = 33;
                    }

                }
                else if (estadoActual == 9)
                {
                    continuarAnalisis = false;
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.MODULO;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;

                }
                else if (estadoActual == 10)
                {
                    continuarAnalisis = false;
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.PARENTESIS_ABRE;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                else if (estadoActual == 11)
                {
                    continuarAnalisis = false;
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.PARENTESIS_CIERRA;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                //fin de archivo
                else if (estadoActual == 12)
                {
                    continuarAnalisis = false;
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.EOF;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                //cargar nueva linea
                else if (estadoActual == 13)
                {
                    CargarNuevaLinea();
                    limpiarLexema();
                    estadoActual = 0;
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
                else if (estadoActual == 15)
                {
                    continuarAnalisis = false;
                    DevolverPuntero();
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.NUMERO_DECIMAL;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                else if (estadoActual == 16)
                {
                    continuarAnalisis = false;
                    DevolverPuntero();
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.IDENTIFICADOR;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                //estado de error
                else if (estadoActual == 17)
                {
                    
                }
                //estado de error
                else if (estadoActual == 18)
                {
                    
                }
                else if (estadoActual == 19)
                {
                    continuarAnalisis = false;
                    DevolverPuntero();
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.IGUAL_QUE;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                else if (estadoActual == 20)
                {
                    LeerSiguienteCaracter();
                    if (CaracterActual.Equals(">"))
                    {
                        estadoActual = 23;
                        concatenarLexema();
                    }
                    else if(CaracterActual.Equals("="))
                    {
                        estadoActual = 24;
                        concatenarLexema();
                    }
                    else
                    {
                        estadoActual = 24;
                    }

                }
                else if (estadoActual == 21)
                {
                    LeerSiguienteCaracter();
                    if (CaracterActual.Equals("="))
                    {
                        estadoActual = 26;
                        concatenarLexema();
                    }
                    else
                    {
                        estadoActual = 27;
                    }

                }
                else if (estadoActual == 22)
                {
                    LeerSiguienteCaracter();
                    if(CaracterActual.Equals("="))
                    {
                        estadoActual = 28;
                        concatenarLexema();
                    }
                    else
                    {
                        estadoActual = 29;
                    }
                }
                else if (estadoActual == 23)
                {
                    continuarAnalisis = false;
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.DIFERENTE_QUE;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                else if (estadoActual == 24)
                {
                    continuarAnalisis = false;
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.MENOR_IGUAL_QUE;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                else if (estadoActual == 25)
                {
                    continuarAnalisis = false;
                    DevolverPuntero();
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.MENOR_QUE;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                else if (estadoActual == 26)
                {
                    continuarAnalisis = false;
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.MAYOR_IGUAL_QUE;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                else if (estadoActual == 27)
                {
                    continuarAnalisis = false;
                    DevolverPuntero();
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.MENOR_QUE;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                else if (estadoActual == 28)
                {
                    continuarAnalisis = false;
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.ASIGNACION;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                //estado de error
                else if (estadoActual == 29)
                {

                }
                else if (estadoActual == 30)
                {
                    LeerSiguienteCaracter();
                    if(CaracterActual.Equals("="))
                    {
                        estadoActual = 31;
                        concatenarLexema();
                    }
                    else
                    {
                        estadoActual = 32;
                    }
                }
                else if (estadoActual == 31)
                {
                    continuarAnalisis = false;
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.DIFERENTE_QUE;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                //estado de error
                else if (estadoActual == 32)
                {

                }
                else if (estadoActual == 33)
                {
                    continuarAnalisis = false;
                    DevolverPuntero();
                    componenteLexico = new ComponenteLexico();
                    componenteLexico.Categoria = Categoria.DIVISION;
                    componenteLexico.Lexema = lexema;
                    componenteLexico.NumeroLinea = NumeroLineaActual;
                    componenteLexico.PosicionInicial = Puntero - lexema.Length;
                    componenteLexico.PosicionInicial = Puntero - 1;
                }
                //revisar
                else if (estadoActual == 34)
                {
                    LeerSiguienteCaracter();
                    if(CaracterActual.Equals("@FL@"))
                    {
                        estadoActual = 37;
                        CargarNuevaLinea();
                    }
                    else if(CaracterActual.Equals("*"))
                    {
                        estadoActual = 35;
                        concatenarLexema();
                    }
                    else
                    {
                        estadoActual = 34;
                        concatenarLexema();
                    }
                }
                else if (estadoActual == 35)
                {
                    LeerSiguienteCaracter();
                    if(CaracterActual.Equals("*"))
                    {
                        estadoActual = 35;
                        concatenarLexema();
                    }
                    else if(CaracterActual.Equals("/"))
                    {
                        estadoActual = 0;
                        concatenarLexema();
                    }
                    else
                    {
                        estadoActual = 34;
                        concatenarLexema();
                    }
                }
                else if (estadoActual == 36)
                {
                    LeerSiguienteCaracter();
                    if(CaracterActual.Equals("@FL@"))
                    {
                        estadoActual = 13;
                    }
                    else
                    {
                        estadoActual = 36;
                    }

                }
                //estado para arreglar 
                else if (estadoActual == 37)
                {
                    LeerSiguienteCaracter();
                    if (CaracterActual.Equals("@FL@"))
                    {
                        estadoActual = 34;
                    }
                }
            }

            return componenteLexico;
        }
    }
}
