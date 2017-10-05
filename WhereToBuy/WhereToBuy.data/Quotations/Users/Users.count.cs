﻿using System;
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

        string _procedureCountName = "UserCount";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public int Count(DataState dataState)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = "[Inativo]='false'";
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = "[Inativo]='true'";
                    break;
                case DataState.All:
                    sqlParameters[sqlParameters.Count - 1].Value = "";
                    break;
                default:
                    throw new MyException(_namespace, _className, "Count()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return Count(ref sqlParameters);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public int Count(string username, DataState dataState)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[Username]='{0}' AND [Inativo]='false'", SQLStrings.CleanDangerousText(username));
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[Username]='{0}' AND [Inativo]='true'", SQLStrings.CleanDangerousText(username));
                    break;
                case DataState.All:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[Username]='{0}'", SQLStrings.CleanDangerousText(username));
                    break;
                default:
                    throw new MyException(_namespace, _className, "Count()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return Count(ref sqlParameters);
        }

        public int Count(string username, string password, DataState dataState)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[Username]='{0}' AND  [Password]=HASHBYTES('SHA2_512', '{1}') AND [Inativo]='false'", SQLStrings.CleanDangerousText(username), SQLStrings.CleanDangerousText(password));
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[Username]='{0}' AND [Password]=HASHBYTES('SHA2_512', '{1}') AND [Inativo]='true'", SQLStrings.CleanDangerousText(username), SQLStrings.CleanDangerousText(password));
                    break;
                case DataState.All:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[Username]='{0}' AND [Password]=HASHBYTES('SHA2_512', '{1}') ", SQLStrings.CleanDangerousText(username), SQLStrings.CleanDangerousText(password));
                    break;
                default:
                    throw new MyException(_namespace, _className, "Count()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return Count(ref sqlParameters);
        }




        int Count(ref List<SqlParameter> sqlParameters)
        {
            int affectedRecords;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            try
            {
                if (connectionOn)
                {
                    engine.SqlServer.OpenConnection();
                }

                affectedRecords = (int)engine.SqlServer.ExecuteScalar(System.Data.CommandType.StoredProcedure, _procedureCountName, false, sqlParameters);

                if (connectionOn)
                {
                    engine.SqlServer.CloseConnection();
                }

            }
            catch (SqlException ex)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(GlobalVariables.ProjectName, MyException.OriginClassSqlError.SQLStoredProcedure, ex.Procedure, ex.Errors);
            }
            catch (MyException)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw;
            }
            catch (Exception ex)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(_namespace, _className, "Count()", ex.Message);
            }

            return affectedRecords;
        }
    }
}
