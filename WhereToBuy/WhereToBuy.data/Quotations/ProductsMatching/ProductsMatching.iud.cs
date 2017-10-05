using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;
using WhereToBuy.utils.GlobalVariables;
using System.Data.SqlClient;

namespace WhereToBuy.data
{
    public partial class ProductsMatching
    {
        #region store and delete

        string _procedureInsertName = "ProductMatchingInsert";
        string _procedureUpdateName = "ProductMatchingUpdate";
        string _procedureDeleteName = "ProductMatchingDelete";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productMatching"></param>
        public void Store(ProductMatching productMatching)
        {
            string info = "";

            switch (productMatching.EditionMode)
            {
                case false:
                    {

                        if (!ProductMatchingSpecs.Validation(productMatching, ValidationPurpose.Insert, ref info))
                        {
                            throw new MyException(_namespace, _className, "Store()", info);
                        }

                        if (this.Exists(productMatching.Supplier, productMatching.Code, productMatching.Supplement, DataState.All))
                        {
                            throw new MyException(_namespace, _className, "Store()", string.Format("{0}!!!", GlobalVariables.Resource.GetString("ExistingInsertCodeString", GlobalVariables.Culture)));
                        }

                        SQLInsert(productMatching);
                        break;

                    }
                case true:
                    {

                        if (!ProductMatchingSpecs.Validation(productMatching, ValidationPurpose.Update, ref info))
                        {
                            throw new MyException(_namespace, _className, "Store()", info);
                        }

                        if (!this.Exists(productMatching, DataState.All))
                        {
                            throw new MyException(_namespace, _className, "Store()", string.Format("{0}!!!", GlobalVariables.Resource.GetString("NotExistingUpdateCodeString", GlobalVariables.Culture)));
                        }


                        SQLUpdate(productMatching);
                        break;

                    }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productMatching"></param>
        public void Delete(ProductMatching productMatching)
        {
            string info = "";

            if (!ProductMatchingSpecs.Validation(productMatching, ValidationPurpose.Delete, ref info))
            {
                throw new MyException(_namespace, _className, "Delete()", info);
            }

            if (!this.Exists(productMatching, DataState.All))
            {
                throw new MyException(_namespace, _className, "Delete()", string.Format("{0}!!!", GlobalVariables.Resource.GetString("NotExistingDeleteCodeString", GlobalVariables.Culture)));
            }

            this.SQLDelete(productMatching);

        }


        #endregion



        #region sql stuff

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productMatching"></param>
        private void SQLInsert(ProductMatching productMatching)
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

                affectedRecords = engine.SqlServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, _procedureInsertName, true, this.Serialize(productMatching, SqlOperationType.Insert));

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
        /// <param name="productMatching"></param>
        private void SQLUpdate(ProductMatching productMatching)
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

                affectedRecords = engine.SqlServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, _procedureUpdateName, true, this.Serialize(productMatching, SqlOperationType.Update));

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
        /// <param name="productMatching"></param>
        private void SQLDelete(ProductMatching productMatching)
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

                affectedRecords = engine.SqlServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, _procedureDeleteName, true, this.Serialize(productMatching, SqlOperationType.Delete));

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
