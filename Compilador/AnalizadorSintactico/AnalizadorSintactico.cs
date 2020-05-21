using Compilador.ManejadorErrores;
using Compilador.Transversal;
using System;
using System.Windows.Forms;

namespace Compilador
{
    public class AnalizadorSintactico
    {
        private AnalizadorLexico anaLex = new AnalizadorLexico();
        private ComponenteLexico componente = null;
        private string traza = "";
        private bool mostrarTraza = true;


        public void analizar()
        {
            traza = "";
            LeerSiguienteComponente();
            SumaResta("--");


            if (GestorErrores.HayErrores())
            {
                MessageBox.Show("El programa contiene errores. Por favor verifique la consola respectiva...");
            }
            else if (Categoria.EOF.Equals(componente.Categoria))
            {
                MessageBox.Show("El Programa se encuentra bien escrito...");
            }
            else
            {
                MessageBox.Show("El Programa se encuentra bien escrito, faltaron componentes por evaluar...");
            }

            MessageBox.Show("Ruta de evalucion de la gramatica: \n" + traza);
        }

        //<SumaResta> := <MultDiv><CSumaResta>
        private void SumaResta(string posicion)
        {
            posicion = posicion + "----";
            FormarTrazaEntrada(posicion, "SumaResta");
            MultDiv(posicion);
            CSumaResta(posicion);
            FormarTrazaSalida(posicion, "SumaResta");
        }

       

        // <MultDiv> := <Resto><CMultDiv>
        private void MultDiv(string posicion)
        {
            posicion = posicion + "----";
            FormarTrazaEntrada(posicion, "MultDiv");
            Resto(posicion);
            CMultDiv(posicion);
            FormarTrazaSalida(posicion, "MultDiv");
        }

        // <CSumaResta> := +<SumaResta>|-<SumaResta>|Epsilon
        private void CSumaResta(string posicion)
        {
            posicion = posicion + "----";
            FormarTrazaEntrada(posicion, "CSumaResta");
            if (Categoria.SUMA.Equals(componente.Categoria))
            {
                LeerSiguienteComponente();
                SumaResta(posicion);
                //Sumar los numeros
            }
            else if (Categoria.RESTA.Equals(componente.Categoria))
            {
                LeerSiguienteComponente();
                SumaResta(posicion);
                //Restar los numeros
            }
            else
            {
                //Epsilon
            }
            FormarTrazaSalida(posicion, "CSumaResta");
        }

       
        //<CMultDiv> := *<MultDiv>|/<MultDiv>|Epsilon
        private void CMultDiv(string posicion)
        {
            posicion = posicion + "----";
            FormarTrazaEntrada(posicion, "CMultDiv");
            if (Categoria.MULTIPLICACION.Equals(componente.Categoria))
            {
                LeerSiguienteComponente();
                MultDiv(posicion);
                //Multiplicar los numeros
            }
            else if (Categoria.DIVISION.Equals(componente.Categoria))
            {
                LeerSiguienteComponente();
                MultDiv(posicion);
                //Dividir los numeros
            }
            else
            {
                //Epsilon
            }

            FormarTrazaSalida(posicion, "CMultDiv");
        }
        //<Resto> := ENTERO|DECIMAL|(<SumaResta>)
        private void Resto(string posicion)
        {
            posicion = posicion + "----";
            FormarTrazaEntrada(posicion, "Resto");
            if (Categoria.ENTERO.Equals(componente.Categoria))
            {
                LeerSiguienteComponente();
                //convertir el dato a entenro
            }
            else if (Categoria.NUMERO_DECIMAL.Equals(componente.Categoria))
            {
                LeerSiguienteComponente();
                //convertir el dato a deciaml
            }
            else if (Categoria.PARENTESIS_ABRE.Equals(componente.Categoria))
            {
                LeerSiguienteComponente();
                SumaResta(posicion);
                if (Categoria.PARENTESIS_CIERRA.Equals(componente.Categoria))
                {
                    LeerSiguienteComponente();     
                }
                else
                {
                    //Reportar un error sintáctico
                    Error error = Error.CrearErrorSintatico(
                        componente.Lexema,
                        componente.Categoria,
                        componente.NumeroLinea,
                        componente.PosicionInicial,
                        componente.PosicionFinal,
                        "Componente no válido en la ubicación actual", "Leí" + componente.Categoria + ":" + componente.Lexema + "y esperaba un paréntesis que cierra \")\" ",
                        "asegurese que el caracter que se encuentra en la posicion actual sea parentesis que cierra \")\" ");
                    GestorErrores.Reportar(error);
                }
            }
            else
            {
                //Reportar un error sintáctico 
                Error error = Error.CrearErrorSintatico(
                    componente.Lexema,
                    componente.Categoria,
                    componente.NumeroLinea,
                    componente.PosicionInicial,
                    componente.PosicionFinal,
                    "Componente no válido en la ubicación actual", "Leí" + componente.Categoria + ":" + componente.Lexema + "y esperaba un ENTERO, DECIMAL o PARENTESIS QUE ABRE \")\" ",
                    "asegurese que el caracter que se encuentra en la posicion actual sea ENTERO, DECIMAL o PARENTESIS QUE ABRE\")\" ");
                GestorErrores.Reportar(error);

                throw new Exception("Se ha presentado un error de tipo STOPPER durante el analisis Sintáctico por favor verifique la consola de errores");

            }
            FormarTrazaSalida(posicion, "MultDiv");

        }

        private void FormarTrazaEntrada(string posicion, string nombreRegla)
        {
            traza = traza + posicion + "Entrada Regla:" + nombreRegla + "Categoria:" + componente.Categoria + ", lexema: " + componente.Lexema + "\n";
            ImprimirTraza();
        }

        private void FormarTrazaSalida(string posicion, string nombreRegla)
        {
            traza = traza + posicion + "Salida Regla:" + nombreRegla + "\n";
            ImprimirTraza();
        }

        private void ImprimirTraza()
        {
            if (mostrarTraza)
            {
                MessageBox.Show(traza);
            }
        }

        private void LeerSiguienteComponente()
        {
            componente = anaLex.Analizar();
        }
    }
}
