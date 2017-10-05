using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;

namespace WhereToBuy.utils
{
    public class SQLServerManagement
    {
         string connectionString;
        SqlConnection sqlConnection;
        SqlTransaction sqlTransaction;


        /// <summary>
        /// Este construtor recebe um parametro como connection string, mas não abre a connection a base de dados
        /// </summary>
        /// <param name="cnString", GlobalVariables.Culture>connection string [ex: ConfigurationManager.ConnectionStrings("cnTeste").ConnectionString]</param>
        public SQLServerManagement(string connectionString)
        {
            this.connectionString = connectionString;
        }


        #region Código relacionado com a connection


        /// <summary>
        /// Este método abre a conection à base de dados 
        /// </summary>
        public void OpenConnection()
        {
            if (sqlConnection == null)
            {
                sqlConnection = new SqlConnection(connectionString);
            }
            sqlConnection.Open();

        }


        /// <summary>
        /// Este método fecha a connection à base de dados
        /// </summary>
        public void CloseConnection()
        {
            sqlConnection.Close();
        }


        /// <summary>
        /// Este metodo serve para verificar se a connection está aberta e pronta a ser usada.
        /// </summary>
        /// <returns>true se estiver aberta e preparada ou false senão</returns>
        public bool IsConnectionOpen()
        {

            if (sqlConnection == null)
            {
                return false;
            }

            if (sqlConnection.State == ConnectionState.Broken || sqlConnection.State == ConnectionState.Closed)
            {
                return false;
            }

            return true;
        }



        #endregion


        #region Código relacionado com a transaction


        /// <summary>
        /// Este método inicia uma nova transaction na connection actual
        /// </summary>
        public void BeginTransaction()
        {
            if (!this.IsConnectionOpen())
            {
                throw new Exception("DBBuilder - A connection não está preparada para iniciar uma transacção.");//Traduzir
            }
            sqlTransaction = sqlConnection.BeginTransaction();
        }



        /// <summary>
        /// Este método faz commit à transaction actual
        /// </summary>
        public void CommitTransaction()
        {
            if (sqlTransaction == null)
            {
                throw new Exception("DBBuilder - Não existe nenhuma transaction para fazer Commit.");//Traduzir
            }
            sqlTransaction.Commit();
            sqlTransaction.Dispose();
            sqlTransaction = null;
        }



        /// <summary>
        /// Este método faz Rollback à transaction actual
        /// </summary>
        public void RollbackTransaction()
        {
            if (sqlTransaction == null)
            {
                throw new Exception("DBBuilder - Não existe nenhuma transaction para fazer Rollback.");//Traduzir
            }
            sqlTransaction.Rollback();
            sqlTransaction.Dispose();
            sqlTransaction = null;
        }


        /// <summary>
        /// Este método indica se existe alguma transacção activa neste momento
        /// </summary>
        public bool IsTransactionAlive
        {
            get { return (sqlTransaction != null); }
        }



        #endregion


        #region Execução contra base de dados



        /// <summary>
        /// Executa uma string ou uma stored procedure contra uma base de dados.
        /// </summary>
        /// <param name="commandType">tipo de command SQL</param>
        /// <param name="commandText">string contendo o texto a ser executado</param>
        /// <param name="runUnderTransaction">obriga a que esta instrução corra debaixo de uma transacção</param>
        /// <returns>retorna o numero de linhas afectadas com a instrução</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText, bool runUnderTransaction)
        {
            return ExecuteNonQuery(commandType, commandText, runUnderTransaction, null);
        }



        /// <summary>
        /// Executa uma string ou uma stored procedure contra uma base de dados.
        /// </summary>
        /// <param name="commandType">tipo de command SQL</param>
        /// <param name="commandText">string contendo o texto a ser executado</param>
        /// <param name="runUnderTransaction">obriga a que esta instrução corra debaixo de uma transacção</param>
        /// <param name="sqlParameters">um array de parametros sql</param>
        /// <returns>retorna o numero de linhas afectadas com a instrução</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText, bool runUnderTransaction, List<SqlParameter> sqlparameters)
        {
            int affectedLines;
            SqlCommand sqlCommand;

            if (!IsConnectionOpen())
            {
                throw new Exception("DBBuilder - Não pode usar o método ExecuteNonQuery() sem a connection aberta.");//Traduzir
            }


            if (runUnderTransaction && sqlTransaction == null)
            {
                throw new Exception("DBBuilder - Antes de executar ExecuteNonQuery(), deve ser gerada uma transaction.");//Traduzir
            }

            sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            if (runUnderTransaction)
            {
                sqlCommand.Transaction = sqlTransaction;
            }
            sqlCommand.CommandType = commandType;
            sqlCommand.CommandText = commandText;

            if (sqlparameters != null)
            {
                foreach (SqlParameter p in sqlparameters)
                {
                    sqlCommand.Parameters.Add(p);
                }
            }

            try
            {
                affectedLines = sqlCommand.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                sqlCommand.Dispose();
                throw ex;
            }
            catch (Exception ex)
            {
                sqlCommand.Dispose();
                throw ex;
            }
            finally
            {
                sqlCommand.Dispose();
            }
            return affectedLines;
        }



        /// <summary>
        /// Executa uma string ou uma stored procedure contra uma base de dados.
        /// </summary>
        /// <param name="commandType">tipo de command SQL</param>
        /// <param name="commandText">string contendo o texto a ser executado</param>
        /// <param name="runUnderTransaction">obriga a que esta instrução corra debaixo de uma transacção</param>
        /// <param name="sqlParameters">um array de parametros sql</param>
        /// <returns>um objecto retornado pelo único valor (scalar) retornado pela string sql</returns>
        public object ExecuteScalar(CommandType commandType, string commandText, bool runUnderTransaction, List<SqlParameter> sqlParameters)
        {
            object obj;
            SqlCommand sqlCommand;


            if (!IsConnectionOpen())
            {
                throw new Exception("DBBuilder - Não pode usar o método ExecuteScalar() sem a connection aberta.");//Traduzir
            }


            if (runUnderTransaction && sqlTransaction == null)
            {
                throw new Exception("DBBuilder - Antes de executar ExecuteScalar(), deve ser gerada uma transaction.");//Traduzir
            }

            sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            if (runUnderTransaction)
            {
                sqlCommand.Transaction = sqlTransaction;
            }
            sqlCommand.CommandType = commandType;
            sqlCommand.CommandText = commandText;

            if (sqlParameters != null)
            {
                foreach (SqlParameter p in sqlParameters)
                {
                    sqlCommand.Parameters.Add(p);
                }
            }

            try
            {
                obj = sqlCommand.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                sqlCommand.Dispose();
                throw ex;
            }
            catch (Exception ex)
            {
                sqlCommand.Dispose();
                throw ex;

            }
            finally
            {
                sqlCommand.Dispose();
            }
            return obj;
        }



        /// <summary>
        /// Este metodo devolve uma DataTable usando uma string sql ou storedprocedure sql.
        /// </summary>
        /// <param name="commandType">tipo de comando sql</param>
        /// <param name="commandText">string sql para ser executada</param>
        /// <param name="sqlParameters">>array de parameters sql</param>
        /// <returns>retorna uma datatable com os dados</returns>
        public DataTable GetDataTable(CommandType commandType, string commandText)
        {
            DataTable dataTable = new DataTable();
            try
            {
                this.Fill(commandType, commandText, ref dataTable, null);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dataTable;
        }



        /// <summary>
        /// Este metodo devolve uma DataTable usando uma string sql ou storedprocedure sql.
        /// </summary>
        /// <param name="commandType">tipo de comando sql</param>
        /// <param name="commandText">string sql para ser executada</param>
        /// <param name="sqlParameters">>array de parameters sql</param>
        /// <returns>retorna uma datatable com os dados</returns>
        public DataTable GetDataTable(CommandType commandType, string commandText, List<SqlParameter> sqlParameters)
        {
            DataTable dataTable = new DataTable();
            try
            {
                this.Fill(commandType, commandText, ref dataTable, sqlParameters);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dataTable;
        }



        /// <summary>
        /// Este metodo enche uma DataTable usando uma string sql ou storedprocedure sql.
        /// </summary>
        /// <param name="commandType">tipo de comando sql</param>
        /// <param name="commandText">string sql para ser executada</param>
        /// <param name="dataTable">tabela para encher</param>
        /// <param name="sqlParameters">array de parameters sql</param>
        public void Fill(CommandType commandType, string commandText, ref DataTable dataTable, List<SqlParameter> sqlParameters)
        {
            SqlDataAdapter sqlDataAdapter;
            SqlCommand sqlCommand;

            if (!IsConnectionOpen())
            {
                throw new Exception("DBBuilder - Não pode usar o método Fill() sem a connection aberta.");//Traduzir
            }

            sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = commandType;
            sqlCommand.CommandText = commandText;

            // Se sqlParameters for igual a null, não tem parametros
            if (sqlParameters != null)
            {
                foreach (SqlParameter p in sqlParameters)
                {
                    sqlCommand.Parameters.Add(p);
                }
            }


            try
            {
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                sqlDataAdapter.Dispose();
                sqlDataAdapter = null;
            }
            catch (SqlException ex)
            {
                sqlCommand.Dispose();
                throw ex;

            }
            catch (Exception ex)
            {
                sqlCommand.Dispose();
                throw ex;

            }
            finally
            {
                sqlCommand.Dispose();
            }
        }



        /// <summary>
        /// Este metodo retorna um Reader de SQLServerManagement de leitura rápida (forwardonly)
        /// </summary>
        /// <param name="commandType">tipo de command sql</param>
        /// <param name="commandText">string sql para ser executada</param>
        /// <param name="sqlParameters">Lista de parametros sql</param>
        /// <returns>um apontador para o reader</returns>
        public SqlDataReader ExecuteReader(CommandType commandType, string commandText, List<SqlParameter> sqlParameters)
        {

            SqlCommand sqlCommand;

            if (!IsConnectionOpen())
            {
                throw new Exception("DBBuilder - Não pode usar o método ExecuteReader() sem a connection aberta.");//Traduzir
            }

            sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = commandType;
            sqlCommand.CommandText = commandText;

            // Se sqlParameters for igual a null, não tem parametros
            if (sqlParameters != null)
            {
                foreach (SqlParameter p in sqlParameters)
                {
                    sqlCommand.Parameters.Add(p);
                }
            }


            try
            {
                return sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException ex)
            {
                sqlCommand.Dispose();
                throw ex;

            }
            catch (Exception ex)
            {
                sqlCommand.Dispose();
                throw ex;

            }
            finally
            {
                // a connection é automáticamente fechada quando o reader chega ao fim.
            }
        }



        #endregion
    }
}
