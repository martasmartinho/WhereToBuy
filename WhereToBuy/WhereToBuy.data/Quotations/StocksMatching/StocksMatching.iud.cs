﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;
using WhereToBuy.utils.GlobalVariables;


namespace WhereToBuy.data
{
    public partial class StocksMatching
    {
        #region store and delete

        string _procedureInsertName = "StockMatchingInsert";
        string _procedureUpdateName = "StockMatchingUpdate";
        string _procedureDeleteName = "StockMatchingDelete";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StockMatching"></param>
        public void Store(StockMatching stockMatching)
        {
            string info = "";

            switch (stockMatching.EditionMode)
            {
                case false:
                    {

                        if (!StockMatchingSpecs.Validation(stockMatching, ValidationPurpose.Insert, ref info))
                        {
                            throw new MyException(_namespace, _className, "Store()", info);
                        }

                        if (this.Exists(stockMatching.Supplier, stockMatching.Code, DataState.All))
                        {
                            throw new MyException(_namespace, _className, "Store()", string.Format("{0}!!!", GlobalVariables.Resource.GetString("ExistingInsertCodeString", GlobalVariables.Culture)));
                        }

                        SQLInsert(stockMatching);
                        break;

                    }
                case true:
                    {

                        if (!StockMatchingSpecs.Validation(stockMatching, ValidationPurpose.Update, ref info))
                        {
                            throw new MyException(_namespace, _className, "Store()", info);
                        }

                        if (!this.Exists(stockMatching, DataState.All))
                        {
                            throw new MyException(_namespace, _className, "Store()", string.Format("{0}!!!", GlobalVariables.Resource.GetString("NotExistingUpdateCodeString", GlobalVariables.Culture)));
                        }


                        SQLUpdate(stockMatching);
                        break;

                    }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StockMatching"></param>
        public void Delete(StockMatching stockMatching)
        {
            string info = "";

            if (!StockMatchingSpecs.Validation(stockMatching, ValidationPurpose.Delete, ref info))
            {
                throw new MyException(_namespace, _className, "Delete()", info);
            }

            if (!this.Exists(stockMatching, DataState.All))
            {
                throw new MyException(_namespace, _className, "Delete()", string.Format("{0}!!!", GlobalVariables.Resource.GetString("NotExistingDeleteCodeString", GlobalVariables.Culture)));
            }

            this.SQLDelete(stockMatching);

        }


        #endregion



        #region sql stuff

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockMatching"></param>
        private void SQLInsert(StockMatching stockMatching)
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

                affectedRecords = engine.SqlServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, _procedureInsertName, true, this.Serialize(stockMatching, SqlOperationType.Insert));

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
        /// <param name="StockMatching"></param>
        private void SQLUpdate(StockMatching stockMatching)
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

                affectedRecords = engine.SqlServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, _procedureUpdateName, true, this.Serialize(stockMatching, SqlOperationType.Update));

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
        /// <param name="StockMatching"></param>
        private void SQLDelete(StockMatching stockMatching)
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

                affectedRecords = engine.SqlServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, _procedureDeleteName, true, this.Serialize(stockMatching, SqlOperationType.Delete));

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
