using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.data
{
    public partial class WarningTypes
    {
        #region store and delete

        string _procedureInsertName = "WarningTypeInsert";
        string _procedureUpdateName = "WarningTypeUpdate";
        string _procedureDeleteName = "WarningTypeDelete";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="warningType"></param>
        public void Store(WarningType warningType)
        {
            string info = "";

            switch (warningType.EditionMode)
            {
                case false:
                    {

                        if (!WarningTypeSpecs.Validation(warningType, ValidationPurpose.Insert, ref info))
                        {
                            throw new MyException(_namespace, _className, "Store()", info);
                        }

                        if (this.Exists(warningType.Code, DataState.All))
                        {
                            throw new MyException(_namespace, _className, "Store()", string.Format("{0}!!!", GlobalVariables.Resource.GetString("ExistingInsertCodeString", GlobalVariables.Culture)));
                        }

                        SQLInsert(warningType);
                        break;

                    }
                case true:
                    {

                        if (!WarningTypeSpecs.Validation(warningType, ValidationPurpose.Update, ref info))
                        {
                            throw new MyException(_namespace, _className, "Store()", info);
                        }

                        if (!this.Exists(warningType, DataState.All))
                        {
                            throw new MyException(_namespace, _className, "Store()", string.Format("{0}!!!", GlobalVariables.Resource.GetString("NotExistingUpdateCodeString", GlobalVariables.Culture)));
                        }


                        SQLUpdate(warningType);
                        break;

                    }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="warningType"></param>
        public void Delete(WarningType warningType)
        {
            string info = "";

            if (!WarningTypeSpecs.Validation(warningType, ValidationPurpose.Delete, ref info))
            {
                throw new MyException(_namespace, _className, "Delete()", info);
            }

            if (!this.Exists(warningType, DataState.All))
            {
                throw new MyException(_namespace, _className, "Delete()", string.Format("{0}!!!", GlobalVariables.Resource.GetString("NotExistingDeleteCodeString", GlobalVariables.Culture)));
            }

            this.SQLDelete(warningType);

        }


        #endregion



        #region sql stuff

        /// <summary>
        /// 
        /// </summary>
        /// <param name="warningType"></param>
        private void SQLInsert(WarningType warningType)
        {

            int affectedRecords;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            try
            {
                if (connectionOn)
                {
                    engine.SqlServer.OpenConnection();
                    engine.SqlServer.BeginTransaction();
                }

                affectedRecords = engine.SqlServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, _procedureInsertName, true, this.Serialize(warningType, SqlOperationType.Insert));

                if (affectedRecords != 1)
                {
                    throw new MyException(_namespace, _className, "SQLInsert()", string.Format("{0}!", GlobalVariables.Resource.GetString("InsertSqlErrorsString", GlobalVariables.Culture).ToLower()));
                }

                if (connectionOn)
                {
                    engine.SqlServer.CommitTransaction();
                    engine.SqlServer.CloseConnection();
                }

            }
            catch (SqlException ex)
            {
                if (engine.SqlServer.IsTransactionAlive)
                {
                    engine.SqlServer.RollbackTransaction();
                }

                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(GlobalVariables.ProjectName, MyException.OriginClassSqlError.SQLStoredProcedure, ex.Procedure, ex.Errors);
            }
            catch (MyException)
            {
                if (engine.SqlServer.IsTransactionAlive)
                {
                    engine.SqlServer.RollbackTransaction();
                }

                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw;
            }
            catch (Exception ex)
            {
                if (engine.SqlServer.IsTransactionAlive)
                {
                    engine.SqlServer.RollbackTransaction();
                }

                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(_namespace, _className, "SQLInsert()", ex.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="warningType"></param>
        private void SQLUpdate(WarningType warningType)
        {
            int affectedRecords;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            try
            {
                if (connectionOn)
                {
                    engine.SqlServer.OpenConnection();
                    engine.SqlServer.BeginTransaction();
                }

                affectedRecords = engine.SqlServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, _procedureUpdateName, true, this.Serialize(warningType, SqlOperationType.Update));

                if (affectedRecords != 1)
                {
                    throw new MyException(_namespace, _className, "SQLUpdate()", string.Format("{0}!", GlobalVariables.Resource.GetString("UpdateSqlErrorsString", GlobalVariables.Culture).ToLower()));
                }

                if (connectionOn)
                {
                    engine.SqlServer.CommitTransaction();
                    engine.SqlServer.CloseConnection();
                }

            }
            catch (SqlException ex)
            {
                if (engine.SqlServer.IsTransactionAlive)
                {
                    engine.SqlServer.RollbackTransaction();
                }

                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(GlobalVariables.ProjectName, MyException.OriginClassSqlError.SQLStoredProcedure, ex.Procedure, ex.Errors);
            }
            catch (MyException)
            {
                if (engine.SqlServer.IsTransactionAlive)
                {
                    engine.SqlServer.RollbackTransaction();
                }

                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw;
            }
            catch (Exception ex)
            {
                if (engine.SqlServer.IsTransactionAlive)
                {
                    engine.SqlServer.RollbackTransaction();
                }

                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(_namespace, _className, "SQLUpdate()", ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="warningType"></param>
        private void SQLDelete(WarningType warningType)
        {

            int affectedRecords;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            try
            {
                if (connectionOn)
                {
                    engine.SqlServer.OpenConnection();
                    engine.SqlServer.BeginTransaction();
                }

                affectedRecords = engine.SqlServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, _procedureDeleteName, true, this.Serialize(warningType, SqlOperationType.Delete));

                if (affectedRecords != 1)
                {
                    throw new MyException(_namespace, _className, "SQLDelete()", string.Format("{0}!", GlobalVariables.Resource.GetString("DeleteSqlErrorsString", GlobalVariables.Culture).ToLower()));
                }

                if (connectionOn)
                {
                    engine.SqlServer.CommitTransaction();
                    engine.SqlServer.CloseConnection();
                }

            }
            catch (SqlException ex)
            {
                if (engine.SqlServer.IsTransactionAlive)
                {
                    engine.SqlServer.RollbackTransaction();
                }

                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(GlobalVariables.ProjectName, MyException.OriginClassSqlError.SQLStoredProcedure, ex.Procedure, ex.Errors);
            }
            catch (MyException)
            {
                if (engine.SqlServer.IsTransactionAlive)
                {
                    engine.SqlServer.RollbackTransaction();
                }

                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw;
            }
            catch (Exception ex)
            {
                if (engine.SqlServer.IsTransactionAlive)
                {
                    engine.SqlServer.RollbackTransaction();
                }

                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(_namespace, _className, "SQLDelete()", ex.Message);
            }
        }


        #endregion
    }
}
