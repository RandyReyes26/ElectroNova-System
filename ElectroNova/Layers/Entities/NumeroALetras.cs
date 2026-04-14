using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.Entities
{
    public static class NumeroALetras
    {
        public static string ConvertirNumeroALetras(decimal numero)
        {
            long enteros = (long)Math.Floor(numero);
            int decimales = (int)Math.Round((numero - enteros) * 100, 0);

            string letras = ConvertirEntero(enteros);

            if (decimales > 0)
                return $"{letras} con {decimales:00}/100";
            else
                return letras;
        }

        private static string ConvertirEntero(long numero)
        {
            if (numero == 0)
                return "cero";

            if (numero < 0)
                return "menos " + ConvertirEntero(Math.Abs(numero));

            if (numero <= 15)
            {
                string[] unidades =
                {
                "", "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete",
                "ocho", "nueve", "diez", "once", "doce", "trece", "catorce", "quince"
            };
                return unidades[numero];
            }

            if (numero < 20)
                return "dieci" + ConvertirEntero(numero - 10);

            if (numero == 20)
                return "veinte";

            if (numero < 30)
                return "veinti" + ConvertirEntero(numero - 20);

            if (numero < 100)
            {
                string[] decenas =
                {
                "", "", "veinte", "treinta", "cuarenta", "cincuenta",
                "sesenta", "setenta", "ochenta", "noventa"
            };

                long d = numero / 10;
                long r = numero % 10;

                if (r == 0)
                    return decenas[d];

                return decenas[d] + " y " + ConvertirEntero(r);
            }

            if (numero == 100)
                return "cien";

            if (numero < 200)
                return "ciento " + ConvertirEntero(numero - 100);

            if (numero < 1000)
            {
                string[] centenas =
                {
                "", "ciento", "doscientos", "trescientos", "cuatrocientos",
                "quinientos", "seiscientos", "setecientos", "ochocientos", "novecientos"
            };

                long c = numero / 100;
                long r = numero % 100;

                if (r == 0)
                    return centenas[c];

                return centenas[c] + " " + ConvertirEntero(r);
            }

            if (numero == 1000)
                return "mil";

            if (numero < 2000)
                return "mil " + ConvertirEntero(numero % 1000);

            if (numero < 1000000)
            {
                long miles = numero / 1000;
                long resto = numero % 1000;

                string resultado = ConvertirEntero(miles) + " mil";

                if (resto > 0)
                    resultado += " " + ConvertirEntero(resto);

                return resultado;
            }

            if (numero == 1000000)
                return "un millón";

            if (numero < 2000000)
                return "un millón " + ConvertirEntero(numero % 1000000);

            if (numero < 1000000000000)
            {
                long millones = numero / 1000000;
                long resto = numero % 1000000;

                string resultado = ConvertirEntero(millones) + " millones";

                if (resto > 0)
                    resultado += " " + ConvertirEntero(resto);

                return resultado;
            }

            return numero.ToString();
        }
    }
}
