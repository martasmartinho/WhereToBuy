using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.utils
{
    public static partial class SystemValidation
    {
        #region String



        public static string GetRandomUpperLetters(int length)
        {
            int lowerBound = 65;
            int upperBound = 90;
            string str = "";
            for (int index = 0; index < length; ++index)
                str += (string)(object)GetRandomChar(lowerBound, upperBound);
            return str;
        }

        public static string GetRandomLowerLetters(int length)
        {
            int lowerBound = 97;
            int upperBound = 122;
            string str = "";
            for (int index = 0; index < length; ++index)
                str += (string)(object)GetRandomChar(lowerBound, upperBound);
            return str;
        }

        public static string GetRandomNumbersWithZero(int length)
        {
            int lowerBound = 48;
            int upperBound = 57;
            string str = "";
            for (int index = 0; index < length; ++index)
                str += (string)(object)GetRandomChar(lowerBound, upperBound);
            return str;
        }

        public static string GetRandomNumbersWithoutZero(int length)
        {
            int lowerBound = 49;
            int upperBound = 57;
            string str = "";
            for (int index = 0; index < length; ++index)
                str += (string)(object)GetRandomChar(lowerBound, upperBound);
            return str;
        }

        public static string GetRandomSpecialChars(int length)
        {
            int lowerBound = 33;
            int upperBound = 47;
            string str = "";
            for (int index = 0; index < length; ++index)
                str += (string)(object)GetRandomChar(lowerBound, upperBound);
            return str;
        }

        public static char GetRandomChar(int lowerBound, int upperBound)
        {
            return (char)GetRandomNumber(lowerBound, upperBound);
        }

        public static int HowManyWords(string s, char[] splitingChars)
        {
            return s.Split(splitingChars).Length;
        }

        #endregion

        /// <summary>
        /// Este metodo valida o conteudo do valor nullableValue se está dentro das regras definidas
        /// </summary>
        /// <param name="label">identificador do campo que está a ser validado</param>
        /// <param name="nullableValue">valor a ser testado</param>
        /// <param name="mandatory">campo é obrigatório</param>
        /// <param name="dangerousTextValidation">valida acerca da existência de código perigoso</param>
        /// <param name="maxLength">comprimento maximo aceite</param>
        /// <param name="info">razão pela qual é inválido</param>
        /// <returns>verdadeiro se for texto válido</returns>
        public static bool Text(string label, string nullableValue, bool mandatory, bool dangerousTextValidation, int maxLength, ref string info)
        {
            bool valid = true;
            string localInfo = "";

            if (nullableValue != null)
            {
                // O consumidor deste metodo pode não enviar um null mas sim uma string vazia com o intuito de querer dizer a mesma coisa.
                if (nullableValue.Trim() == "")
                {
                    nullableValue = null;
                }
            }


            if (nullableValue == null && mandatory)         // se for nulo e de preenchimento obrigatório
            {
                localInfo += string.Format("#{0}$ {1}.", GlobalVariables.GlobalVariables.Resource.GetString("TypeString", GlobalVariables.GlobalVariables.Culture), GlobalVariables.GlobalVariables.Resource.GetString("MustBeFilledString", GlobalVariables.GlobalVariables.Culture).ToLower());
                info += string.Format("[{0}]", label);
                info += localInfo;
                valid = false;
                return valid;                              // tem que abandonar o metodo já aqui porque se for null nao consegue fazer os testes seguintes
            }

            if (nullableValue == null && !mandatory)        // se for nulo e de preenchimento facultativo
            {
                valid = true;                              // para reforçar a ideia
                return valid;                              // sair do metodo porque não pode testar mais nada e como não é obrigatório é válido.
            }


            if (dangerousTextValidation && SQLStrings.IsDangerousText(nullableValue))
            {
                localInfo += string.Format("#{0}$ ", GlobalVariables.GlobalVariables.Resource.GetString("OthersString", GlobalVariables.GlobalVariables.Culture));                   // só quero acrescentar o #..: caso exista texto perigoso
                SQLStrings.IsDangerousText(nullableValue, ref localInfo);
            }

            if (nullableValue.Length > maxLength)
            {
                localInfo += string.Format("#{0}$ {1}.", GlobalVariables.GlobalVariables.Resource.GetString("BoundString", GlobalVariables.GlobalVariables.Culture), GlobalVariables.GlobalVariables.Resource.GetString("LargerTextString", GlobalVariables.GlobalVariables.Culture).ToLower());
                valid = false;
            }

            if (localInfo.Length > 0)
            {
                info += string.Format("[{0}]", label);
                info += localInfo;
            }

            return valid;
        }



        /// <summary>
        /// Este metodo valida o conteudo do valor nullableValue se está dentro das regras definidas
        /// </summary>
        /// <param name="label">identificador do campo que está a ser validado</param>
        /// <param name="nullableValue">valor a ser testado</param>
        /// <param name="mandatory">campo é obrigatório</param>
        /// <param name="dangerousTextValidation">valida acerca da existência de código perigoso</param>
        /// <param name="minLength">comprimento minimo aceite</param>
        /// <param name="maxLength">comprimento maximo aceite</param>
        /// <param name="info">razão pela qual é inválido</param>
        /// <returns>verdadeiro se for texto válido</returns>
        public static bool Text(string label, string nullableValue, bool mandatory, bool dangerousTextValidation, int minLength, int maxLength, ref string info)
        {
            bool valido = true;
            string localInfo = "";


            if (nullableValue != null)
            {
                // O consumidor deste metodo pode não enviar um null mas sim uma string vazia com o intuito de querer dizer a mesma coisa.
                if (nullableValue.Trim() == "")
                {
                    nullableValue = null;
                }
            }

            if (nullableValue == null && mandatory)         // se for nulo e de preenchimento obrigatorio
            {
                localInfo += string.Format("#{0}$ {1}.", GlobalVariables.GlobalVariables.Resource.GetString("TypeString", GlobalVariables.GlobalVariables.Culture), GlobalVariables.GlobalVariables.Resource.GetString("MustBeFilledString", GlobalVariables.GlobalVariables.Culture).ToLower()); 
                info += string.Format("[{0}]", label);
                info += localInfo;
                valido = false;
                return valido;                              // tem que abandonar o metodo já aqui porque se for null nao consegue fazer os testes seguintes
            }

            if (nullableValue == null && !mandatory)        // se for nulo e de preenchimento facultativo
            {
                valido = true;                              // só para reforçar a ideia
                return valido;                              // sair do metodo porque não pode testar mais nada, afinal de contas não é obrigatório
            }


            if (dangerousTextValidation && SQLStrings.IsDangerousText(nullableValue))
            {
                localInfo += string.Format("#{0}$ ", GlobalVariables.GlobalVariables.Resource.GetString("OthersString", GlobalVariables.GlobalVariables.Culture));                   // só quero acrescentar o #..: caso exista texto perigoso
                SQLStrings.IsDangerousText(nullableValue, ref localInfo);
            }

            if (nullableValue.Length < minLength)
            {
                localInfo += string.Format("#{0}$ {1}.", GlobalVariables.GlobalVariables.Resource.GetString("BoundString", GlobalVariables.GlobalVariables.Culture), GlobalVariables.GlobalVariables.Resource.GetString("SmallerTextString", GlobalVariables.GlobalVariables.Culture).ToLower());
                valido = false;
            }

            if (nullableValue.Length > maxLength)
            {
                localInfo += String.Format("#{0}$ {1}.", GlobalVariables.GlobalVariables.Resource.GetString("BoundString", GlobalVariables.GlobalVariables.Culture), GlobalVariables.GlobalVariables.Resource.GetString("LargerTextString", GlobalVariables.GlobalVariables.Culture).ToLower());
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
        /// Este metodo valida o conteudo do valor nullableValue se está dentro das regras definidas
        /// </summary>
        /// <param name="label">identificador do campo que está a ser validado</param>
        /// <param name="nullableValue">valor a ser testado</param>
        /// <param name="mandatory">campo é obrigatório</param>
        /// <param name="validChars">caracteres aceites (possiveis de serem usados)</param>
        /// <param name="minLength">comprimento minimo aceite</param>
        /// <param name="maxLength">comprimento maximo aceite</param>
        /// <param name="info">razão pela qual é inválido</param>
        /// <returns>verdadeiro se for texto válido</returns>
        public static bool Text(string label, string nullableValue, bool mandatory, char[] validChars, int minLength, int maxLength, ref string info)
        {
            bool valido = true;
            string localInfo = "";

            if (nullableValue != null)
            {
                // O consumidor deste metodo pode não enviar um null mas sim uma string vazia com o intuito de querer dizer a mesma coisa.
                if (nullableValue.Trim() == "")
                {
                    nullableValue = null;
                }
            }


            if (nullableValue == null && mandatory)         // se for de preenchimento obrigatorio
            {
                localInfo += "#Tipo$ preenchimento obrigatório.";
                info += string.Format("[{0}]", label);
                info += localInfo;
                valido = false;
                return valido;                              // tem que abandonar o metodo já aqui porque se for null nao consegue fazer os testes seguintes
            }

            if (nullableValue == null && !mandatory)        // se for nulo e de preenchimento facultativo
            {
                valido = true;                              // para reforçar a ideia
                return valido;                              // sair do metodo porque não pode testar mais nada.
            }


            //validação se os caracteres da string text estão contidas em validChars
            char[] valueChars = nullableValue.ToCharArray();

            IEnumerable<char> irregularChars = (from t in valueChars
                                                select t).Except(validChars);       //  ou  // char[] irregularChars = textChars.Except(validChars).ToArray();

            if (irregularChars.ToList().Count > 0)
            {
                localInfo += "#Caracteres inválidos$ (";
                foreach (var c in irregularChars)
                {
                    if (localInfo.Substring(localInfo.Length - 1) == "'") { localInfo += ","; }      // coloca a virgula para separar um novo caracter, caso o ultimo caracter seja uma pelica
                    localInfo += string.Format("'{0}'", c);
                }
                localInfo += ")";
                valido = false;
            }


            if (nullableValue.Length < minLength)
            {
                localInfo += "#Limite$ texto com tamanho inferior ao permitido.";
                valido = false;
            }

            if (nullableValue.Length > maxLength)
            {
                localInfo += "#Limite$ texto com tamanho superior ao permitido.";
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
