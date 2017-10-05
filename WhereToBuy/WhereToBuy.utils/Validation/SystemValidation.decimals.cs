using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WhereToBuy.utils
{
    public static partial class SystemValidation
    {
        //static Type[] classes;
        //static string p = System.IO.Directory.GetCurrentDirectory();
        //static System.Reflection.Assembly myDllAssembly =
        //       System.Reflection.Assembly.LoadFile(p + @"\DbBuilder.helpers.dll");

        #region Decimal

        public static Decimal DaParteInteira(Decimal x)
        {
            return Math.Truncate(x);
        }

        public static Decimal DaParteDecimal(Decimal x)
        {
            return Math.Round(x - DaParteInteira(x), DaNumeroCasasDecimais(x));
        }

        public static int DaNumeroCasasDecimais(Decimal x)
        {
            string s = Convert.ToString(x).Trim().Replace(".", ",");
            int decimalPos = s.IndexOf(",") + 1;
            return s.Length - decimalPos;
        }

        #endregion


        /// <summary>
        /// Este metodo valida se um determinado valor se encontra dentro do limite min e max.
        /// </summary>
        /// <param name="label">identificador do campo que está a ser validado</param>
        /// <param name="value">valor a ser testado</param>
        /// <param name="mandatory">campo obrigatorio</param>
        /// <param name="min">limite inferior permitido</param>
        /// <param name="max">limite superior permitido</param>
        /// <param name="info">razão pela qual é inválido</param>
        /// <returns>verdadeiro se for um numero válido</returns>
        public static bool Decimal(string label, Nullable<Decimal> value, bool mandatory, Decimal min, Decimal max, ref string info)
        {
            bool valido = true;
            string localInfo = "";

            if (value == null && mandatory)         // se for nulo e obrigatório
            {
                localInfo += "#Tipo$ preenchimento obrigatório.";
                info += string.Format("[{0}]", label);
                info += localInfo;
                valido = false;
                return valido;                      // tem que abandonar o teste já aqui porque se for null nao consegue fazer os testes seguintes
            }

            if (value == null && !mandatory)        // se for nulo e não obrigatorio
            {
                valido = true;                      // para reforçar a ideia
                return valido;                      // sair porque não há mais nada a testar
            }


            if (value < min)
            {
                localInfo += "#Limite$ decimal inferior ao permitido.";
                valido = false;
            }

            if (value > max)
            {
                localInfo += "#Limite$ decimal superior ao permitido.";
                valido = false;
            }

            if (localInfo.Length > 0)
            {
                info += string.Format("[{0}]", label);
                info += localInfo;
            }

            return valido;
        }



        /// <summary>
        /// Este metodo valida se um determinado valor se encontra dentro do limite min e max.
        /// </summary>
        /// <param name="label">identificador do campo que está a ser validado</param>
        /// <param name="value">valor a ser testado</param>
        /// <param name="mandatory">campo obrigatorio</param>
        /// <param name="min">limite inferior permitido</param>
        /// <param name="max">limite superior permitido</param>
        /// <param name="maxNumberOfDecimalsDigits">numero de casas decimais permitido</param>
        /// <param name="info">razão pela qual é inválido</param>
        /// <returns>verdadeiro se for um numero válido</returns>
        public static bool Decimal(string label, Nullable<Decimal> value, bool mandatory, Decimal min, Decimal max, int maxNumberOfDecimalsDigits, ref string info)
        {
            bool valido = true;
            string localInfo = "";

            if (value == null && mandatory)         // se for nulo e obrigatório
            {
                localInfo += "#Tipo$ preenchimento obrigatório.";
                info += string.Format("[{0}]", label);
                info += localInfo;
                valido = false;
                return valido;                      // tem que abandonar o teste já aqui porque se for null nao consegue fazer os testes seguintes
            }

            if (value == null && !mandatory)        // se for nulo e não obrigatorio
            {
                valido = true;                      // para reforçar a ideia
                return valido;                      // sair porque não há mais nada a testar
            }


            if (value < min)
            {
                localInfo += "#Limite$ decimal inferior ao permitido.";
                valido = false;
            }

            if (value > max)
            {
                localInfo += "#Limite$ decimal superior ao permitido.";
                valido = false;
            }

            if (DaNumeroCasasDecimais((decimal)value) > maxNumberOfDecimalsDigits)
            {
                localInfo += "#Parte decimal$ decimal com mais casas decimais do que o permitido.";
                valido = false;
            }


            if (localInfo.Length > 0)
            {
                info += string.Format("[{0}]", label);
                info += localInfo;
            }

            return valido;
        }

    }
}
