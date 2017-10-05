using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace WhereToBuy.utils
{
    public static partial class SystemValidation
    {

        #region Emails



        /// <summary>
        /// Este metodo valida se um email é válido
        /// </summary>
        /// <param name="label">identificador do campo que está a ser validado</param>
        /// <param name="value">endereço a validar</param>
        /// <param name="info">razão pela qual é inválido</param>
        /// <returns>verdadeiro se for válido</returns>
        public static bool Email(string label, string value, ref string info)
        {
            bool valido;
            string localInfo = "";

            valido = IsValidEmail(value, ref localInfo);

            if (localInfo.Length > 0)
            {
                info += string.Format("[{0}]", label);
                info += localInfo;
            }

            return valido;
        }



        /// <summary>
        /// Este metodo valida se um email é válido
        /// </summary>
        /// <param name="label">identificador do campo que está a ser validado</param>
        /// <param name="value">endereço a validar</param>
        /// <param name="maxLenght">tamanho máximo da string</param>
        /// <param name="info">razão pela qual é inválido</param>
        /// <returns>verdadeiro se for válido</returns>
        public static bool Email(string label, string value, int maxLenght, ref string info)
        {
            bool valido;
            string localInfo = "";

            valido = IsValidEmail(value, ref localInfo);

            if (value.TrimEnd().Length > maxLenght)
            {
                localInfo += "#Comprimento$ email com tamanho superior ao permitido.";
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
        /// Este metodo valida se um email é válido
        /// </summary>
        /// <param name="label">identificador do campo que está a ser validado</param>
        /// <param name="value">endereço a validar</param>
        /// <param name="maxLenght">tamanho máximo da string</param>
        /// <param name="mandatory">preenchimento obrigatório</param>
        /// <param name="info">razão pela qual é inválido</param>
        /// <returns>verdadeiro se for válido</returns>
        public static bool Email(string label, string value, int maxLenght, bool mandatory, ref string info)
        {
            bool valido;
            string localInfo = "";

            if (mandatory && value == null)
            {
                localInfo += "#Tipo$ email de preenchimento obrigatório.";
                valido = false;
            }
            else
            {
                valido = IsValidEmail(value, ref localInfo);

                if (value.TrimEnd().Length > maxLenght)
                {
                    localInfo += "#Comprimento$ email com tamanho superior ao permitido.";
                    valido = false;
                }

                if (value.Trim().Length == 0)
                {
                    localInfo += "#Tipo$ email de preenchimento obrigatório.";
                    valido = false;
                }
            }

            if (localInfo.Length > 0)
            {
                info += string.Format("[{0}]", label);
                info += localInfo;
            }

            return valido;
        }



        /// <summary>
        /// Metodo privado que valida se uma string é um email valido
        /// </summary>
        /// <param name="email">email a validar</param>
        /// <param name="info">razão pela qual é inválido</param>
        /// <returns>verdadeiro se o email é válido</returns>
        private static bool IsValidEmail(string email, ref string info)
        {
            if (email == null)
            {
                throw new Exception("O metodo IsValidEmail() não aceita um email com valor nulo!");
            }

            // string emailPattern = @"^(([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]+)(\;)?(\;\s)?)+$";
            Regex regex = new Regex("^(?<user>[^@]+)@(?<host>.+)$");
            Match match = regex.Match(email);
            if (match.Success == false)
            {
                info += "#Formato$ email inválido.";
            }
            return match.Success;
        }



        #endregion
    }
}
