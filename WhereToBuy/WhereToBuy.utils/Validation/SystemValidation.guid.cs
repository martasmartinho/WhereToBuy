using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WhereToBuy.utils
{
    public static partial class SystemValidation
    {

        #region Guid



        /// <summary>
        /// Este metodo valida se um guid é válido
        /// </summary>
        /// <param name="label">identificador do campo que está a ser validado</param>
        /// <param name="value">guid a validar</param>
        /// <param name="info">razão pela qual é inválido</param>
        /// <returns>verdadeiro se for um guid válido</returns>
        public static bool Guid(string label, string value, ref string info)
        {
            string localInfo = "";
            bool valido;

            valido = IsValidGuid(value, ref localInfo);

            if (localInfo.Length > 0)
            {
                info += string.Format("[{0}]", label);
                info += localInfo;
            }

            return valido;
        }



        /// <summary>
        /// Este metodo valida se um guid é válido
        /// </summary>
        /// <param name="label">identificador do campo que está a ser validado</param>
        /// <param name="value">guid a validar</param>
        /// <param name="mandatory">preenchimento obrigatório</param>
        /// <param name="info">razão pela qual é inválido</param>
        /// <returns>verdadeiro se for um guid válido</returns>
        public static bool Guid(string label, string value, bool mandatory, ref string info)
        {
            bool valido;
            string localInfo = "";

            valido = IsValidGuid(value, ref localInfo);

            if ((value.Trim().Length == 0) && mandatory == true)
            {
                localInfo += "#Tipo$ guid com preenchimento obrigatório.";
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
        /// Metodo privado que valida se uma string é um guid valido
        /// </summary>
        /// <param name="guid">guid a validar</param>
        /// <param name="info">razão pela qual é inválido</param>
        /// <returns>verdadeiro se o guid é válido</returns>
        private static bool IsValidGuid(string guid, ref string info)
        {
            System.Guid temp;
            bool valido;

            if (!System.Guid.TryParse(guid, out temp))
            {
                info += "#Formato$ guid inválido.";
                valido = false;
            }
            else
            {
                valido = true;
            }

            return valido;
        }



        #endregion
    }
}
