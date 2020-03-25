using System;
using System.Collections.Generic;
using System.Text;

namespace Compilador.Cache
{
    public class Cache
    {
        private static List<Linea> lineas = new List<Linea>();
        public static void AgregarLinea(string contenido)
        {
            if (contenido != null)
            {

                Linea linea = new Linea();
                linea.Contenido = contenido;

                if (lineas.Count == 0)
                {

                    linea.Numero = 1;

                }
                else
                {
                    linea.Numero = lineas.Count + 1;
                }

                lineas.Add(linea);

            }
        }

        public static Linea ObtenerLinea(int numero)
        {

            Linea lineaRetorno;

            if (ExisteLinea(numero))
            {
                lineaRetorno = lineas[numero - 1];

            }
            else
            {
                lineaRetorno = new Linea();
                lineaRetorno.Contenido = "@EOF@";
                lineaRetorno.Numero = lineas.Count + 1;
            }

            return lineaRetorno;
        }

        public static void LimpiarLineas()
        {
            lineas.Clear();
        }

        public static bool ExisteLinea(int numero)
        {

            return (lineas.Count <= numero && numero > 0);

        }


    }
}
