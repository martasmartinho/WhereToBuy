using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.data
{
    public partial class Users
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        User Deserialize(ref SqlDataReader sqlDataReader)
        {
            User user = new User();

            user.Username = ((string)sqlDataReader["Username"]).TrimEnd();
            user.Password = Convert.ToBase64String(((byte[])sqlDataReader["Password"])).TrimEnd();
            user.Name = ((string)sqlDataReader["Nome"]).TrimEnd();
            user.Email = ((string)sqlDataReader["Email"]).TrimEnd();
            user.Mobile = ((string)sqlDataReader["Mobile"]).TrimEnd();
            user.Sms = ((string)sqlDataReader["Sms"]).TrimEnd();
            user.Administrator = (bool)sqlDataReader["Administrador"];
            user.Language = new Language();
            user.Language.Code = ((string)sqlDataReader["IdiomaCodigo"]).TrimEnd();
            user.Inactive = (bool)sqlDataReader["Inativo"];
            user.Creation = (DateTime)sqlDataReader["Criacao"];
            user.Version = (DateTime)sqlDataReader["Versao"];
            user.EditionMode = true;
            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(User user, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@Username", SQLStrings.CleanDangerousText(user.Username)));
                    sqlParameters.Add(new SqlParameter("@Password", Encoding.UTF8.GetBytes(SQLStrings.CleanDangerousText(user.Password))));
                    sqlParameters.Add(new SqlParameter("@Nome", SQLStrings.CleanDangerousText(user.Name)));
                    sqlParameters.Add(new SqlParameter("@Email", SQLStrings.CleanDangerousText(user.Email)));
                    sqlParameters.Add(new SqlParameter("@Mobile", SQLStrings.CleanDangerousText(user.Mobile)));
                    sqlParameters.Add(new SqlParameter("@Sms", SQLStrings.CleanDangerousText(user.Sms)));
                    sqlParameters.Add(new SqlParameter("@Administrador", user.Administrator));
                    sqlParameters.Add(new SqlParameter("@IdiomaCodigo", user.Language.Code));
                    sqlParameters.Add(new SqlParameter("@Inativo", user.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@Username", SQLStrings.CleanDangerousText(user.Username)));
                    sqlParameters.Add(new SqlParameter("@Password", Encoding.UTF8.GetBytes(SQLStrings.CleanDangerousText(user.Password))));
                    sqlParameters.Add(new SqlParameter("@Nome", SQLStrings.CleanDangerousText(user.Name)));
                    sqlParameters.Add(new SqlParameter("@Email", SQLStrings.CleanDangerousText(user.Email)));
                    sqlParameters.Add(new SqlParameter("@Mobile", SQLStrings.CleanDangerousText(user.Mobile)));
                    sqlParameters.Add(new SqlParameter("@Sms", SQLStrings.CleanDangerousText(user.Sms)));
                    sqlParameters.Add(new SqlParameter("@Administrador", user.Administrator));
                    sqlParameters.Add(new SqlParameter("@IdiomaCodigo", user.Language.Code));
                    sqlParameters.Add(new SqlParameter("@Inativo", user.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", user.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@Username", SQLStrings.CleanDangerousText(user.Username)));
                    sqlParameters.Add(new SqlParameter("@Versao", user.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
