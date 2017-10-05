using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.utils
{
    public static partial class SystemValidation
    {

        #region Int

        private static Random rnd = new Random((int)DateTime.Now.Ticks);

        public static int[] DaDivisores(int x)
        {
            int[] array = new int[0];
            if (x < 1)
                throw new Exception(GlobalVariables.GlobalVariables.Resource.GetString("DividersErrorString", GlobalVariables.GlobalVariables.Culture));
            for (int index = x; index >= 1; --index)
            {
                if (x % index == 0)
                {
                    ArrayManagement.AdicionaElemento(ref array);
                    array[array.Length - 1] = index;
                }
            }
            return array;
        }

        public static bool Perfeito(int x)
        {
            int[] numArray = DaDivisores(x);
            int num = 0;
            for (int index = 0; index < numArray.Length; ++index)
            {
                if (numArray[index] != x)
                    num += numArray[index];
            }
            return num == x;
        }

        public static int GetRandomNumber(int min, int max)
        {
            return rnd.Next(min, max + 1);
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
        public static bool Int16(string label, Nullable<System.Int16> value, bool mandatory, System.Int16 min, System.Int16 max, ref string info)
        {
            bool valido = true;
            string localInfo = "";

            if (value == null && mandatory)         // se for nulo e obrigatorio
            {
                localInfo += "#Tipo$ preenchimento obrigatório.";//Traduzir
                info += string.Format("[{0}]", label);
                info += localInfo;
                valido = false;
                return valido;                     // tem que abandonar o teste já aqui porque se for null nao consegue fazer os testes seguintes
            }

            if (value == null && !mandatory)        // se for nulo e não obrigatorio
            {
                valido = true;                      // não havia necessidade de colocar porque já é verdadeiro, mas é para reforçar a ideia
                return valido;                      // sair porque não há mais nada a testar e está valido
            }


            if (value < min)
            {
                localInfo += "#Limite$ inteiro inferior ao permitido.";
                valido = false;
            }

            if (value > max)
            {
                localInfo += "#Limite$ inteiro superior ao permitido.";
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
        /// <param name="info">razão pela qual é inválido</param>
        /// <returns>verdadeiro se for um numero válido</returns>
        public static bool Int32(string label, Nullable<System.Int32> value, bool mandatory, System.Int32 min, System.Int32 max, ref string info)
        {
            bool valido = true;
            string localInfo = "";

            if (value == null && mandatory)         // se for nulo e obrigatorio
            {
                localInfo += "#Tipo$ preenchimento obrigatório.";
                info += string.Format("[{0}]", label);
                info += localInfo;
                valido = false;
                return valido;                      // tem que abandonar o teste já aqui porque se for null nao consegue fazer os testes seguintes
            }

            if (value == null && !mandatory)        // se for nulo e não obrigatorio
            {
                valido = true;                      // para reforçar
                return valido;                      // sair porque não há mais nada a testar
            }


            if (value < min)
            {
                localInfo += "#Limite$ inteiro inferior ao permitido.";
                valido = false;
            }

            if (value > max)
            {
                localInfo += "#Limite$ inteiro superior ao permitido.";
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
        /// <param name="info">razão pela qual é inválido</param>
        /// <returns>verdadeiro se for um numero válido</returns>
        public static bool Int64(string label, Nullable<System.Int64> value, bool mandatory, System.Int64 min, System.Int64 max, ref string info)
        {
            bool valido = true;
            string localInfo = "";

            if (value == null && mandatory)         // se for nulo e obrigatorio
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
                localInfo += "#Limite$ inteiro inferior ao permitido.";
                valido = false;
            }

            if (value > max)
            {
                localInfo += "#Limite$ inteiro superior ao permitido.";
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
