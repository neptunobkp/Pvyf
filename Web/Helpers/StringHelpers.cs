using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Helpers
{
    public class StringHelpers
    {
        public static string CadenaADobleBarra(string cadena) {
            if (!string.IsNullOrEmpty(cadena))
                return cadena;
            return "--";
        }

        public static string FormatearMiles(string cadena) {
            string resultado = "";
            int i = 0;
            foreach (var caracter in cadena.Reverse())
            {
                i++;
                resultado =  caracter + resultado;
                if (i == 3)
                {
                    resultado = "." + resultado;
                    i = 0;
                }
            }
            return resultado;
        }
    }
}
