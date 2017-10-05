using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WhereToBuy.utils
{
    public static class NumberInWords
    {
        #region NumberString


        public static string GetExtenso(double valor, string sufixoUnidades, string sufixoDecimal, Generos genero)
        {
            string extenso = GetExtenso((int)valor, sufixoUnidades, genero);
            string str = valor.ToString().TrimEnd();
            if (!str.Contains(","))
                return extenso;
            int num = str.IndexOf(",");
            int valor1 = int.Parse(str.Substring(num + 1, str.Length - (num + 1)));
            if (valor1 < 10)
                valor1 *= 10;
            return string.Format("{0} e {1}", (object)extenso, (object)GetExtenso(valor1, sufixoDecimal, genero));
        }

        public static string GetExtenso(int valor, string sufixo, Generos genero)
        {
            if (valor > 999999999)
                throw new Exception("CC - Valor superior ao suportado.");
            if (valor == 0)
                return "zero";
            string str = "";
            int valor1 = valor / 1000000;
            int valor2 = valor / 1000 - valor1 * 1000;
            int valor3 = valor - (valor1 * 1000000 + valor2 * 1000);
            if (valor1 > 0)
                str = valor1 != 1 ? str + string.Format("{0} milhões", (object)GetAte999(valor1, genero)) : str + "um milhão";
            if (valor2 > 0)
            {
                if (valor1 > 0 && valor3 == 0)
                    str += " e ";
                else if (valor1 > 0 && valor3 > 0)
                    str += ", ";
                str = valor2 != 1 ? str + string.Format("{0} mil", (object)GetAte999(valor2, genero)) : str + "mil";
            }
            if (valor3 > 0)
            {
                if (valor2 > 0 || valor1 > 0)
                    str = valor3 > 99 ? str + (valor3 % 100 == 0 ? " e " : " ") : str + " e ";
                str += string.Format("{0}", (object)GetAte999(valor3, genero));
            }
            return string.Format("{0}{1}{2}", (object)str, sufixo.Trim().Length > 0 ? (object)" " : (object)"", (object)sufixo);
        }

        private static string GetAte999(int valor, Generos genero)
        {
            if (valor < 0 || valor > 999)
                throw new Exception("CC - O argumento passado para este método deve estar entre 1 e 999, correspondente ao algarismo das unidades e dezenas.");
            int algarismoCentenas = valor / 100;
            int valor1 = valor - algarismoCentenas * 100;
            switch (algarismoCentenas)
            {
                case 0:
                    return string.Format("{0}", (object)GetAte99(valor1, genero));
                case 1:
                    if (valor1 == 0)
                        return GetCentenas(1, genero);
                    return string.Format("cento e {0}", (object)GetAte99(valor1, genero));
                default:
                    if (valor1 == 0)
                        return GetCentenas(algarismoCentenas, genero);
                    return string.Format("{0} e {1}", (object)GetCentenas(algarismoCentenas, genero), (object)GetAte99(valor1, genero));
            }
        }

        private static string GetAte99(int valor, Generos genero)
        {
            if (valor < 0 || valor > 99)
                throw new Exception("CC - O argumento passado para este método deve estar entre 1 e 99, correspondente ao algarismo das unidades e dezenas.");
            string[] strArray = new string[10]
      {
        "dez",
        "onze",
        "doze",
        "treze",
        "catorze",
        "quinze",
        "dezasseis",
        "dezassete",
        "dezoito",
        "dezanove"
      };
            if (valor <= 19)
            {
                switch (valor)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        return GetUnidades(valor, genero);
                    default:
                        return strArray[valor - 10];
                }
            }
            else
            {
                int algarismoDezenas = valor / 10;
                int algarismoUnidades = valor - algarismoDezenas * 10;
                return string.Format("{0}{1}{2}", (object)GetDezenas(algarismoDezenas), algarismoUnidades == 0 ? (object)"" : (object)" e ", algarismoUnidades == 0 ? (object)"" : (object)GetUnidades(algarismoUnidades, genero));
            }
        }

        private static string GetCentenas(int algarismoCentenas, Generos genero)
        {
            if (algarismoCentenas < 1 || algarismoCentenas > 9)
                throw new Exception("CC - O argumento passado para este método deve estar entre 1 e 9, correspondente ao algarismo das dezenas");
            string[] strArray;
            if (genero == Generos.Masculino)
                strArray = new string[10]
        {
          "",
          "cem",
          "duzentos",
          "trezentos",
          "quatrocentos",
          "quinhentos",
          "seiscentos",
          "setecentos",
          "oitocentos",
          "novecentos"
        };
            else
                strArray = new string[10]
        {
          "",
          "cem",
          "duzentas",
          "trezentas",
          "quatrocentas",
          "quinhentas",
          "seiscentas",
          "setecentas",
          "oitocentas",
          "novecentas"
        };
            return strArray[algarismoCentenas];
        }

        private static string GetDezenas(int algarismoDezenas)
        {
            if (algarismoDezenas < 1 || algarismoDezenas > 9)
                throw new Exception("CC - O argumento passado para este método deve estar entre 1 e 9, correspondente ao algarismo das dezenas");
            return new string[10]
      {
        "",
        "dez",
        "vinte",
        "trinta",
        "quarenta",
        "cinquenta",
        "sessenta",
        "setenta",
        "oitenta",
        "noventa"
      }[algarismoDezenas];
        }

        private static string GetUnidades(int algarismoUnidades, Generos genero)
        {
            if (algarismoUnidades > 9 || algarismoUnidades < 0)
                throw new Exception("CC - O argumento passado para este método deve estar entre 1 e 9, correspondente ao algarismo das unidades");
            string[] strArray;
            if (genero == Generos.Masculino)
                strArray = new string[10]
        {
          "zero",
          "um",
          "dois",
          "três",
          "quatro",
          "cinco",
          "seis",
          "sete",
          "oito",
          "nove"
        };
            else
                strArray = new string[10]
        {
          "zero",
          "uma",
          "duas",
          "três",
          "quatro",
          "cinco",
          "seis",
          "sete",
          "oito",
          "nove"
        };
            return strArray[algarismoUnidades];
        }

        public enum Generos
        {
            Masculino,
            Feminino,
        }

        #endregion
    }
}
