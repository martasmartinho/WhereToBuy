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
    public partial class Categories
    {

        #region store and delete

        string _procedureInsertName = "CategoryInsert";
        string _procedureUpdateName = "CategoryUpdate";
        string _procedureDeleteName = "CategoryDelete";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        public void Store(Category category)
        {
            string info = "";

            switch (category.EditionMode)
            {
                case false:
                    {

                        if (!CategorySpecs.Validation(category, ValidationPurpose.Insert, ref info))
                        {
                            throw new MyException(_namespace, _className, "Store()", info);
                        }

                        if (this.Exists(category.Code, DataState.All))
                        {
                            throw new MyException(_namespace, _className, "Store()", string.Format("{0}!!!", GlobalVariables.Resource.GetString("ExistingInsertCodeString", GlobalVariables.Culture)));
                        }

                        SQLInsert(category);
                        break;

                    }
                case true:
                    {

                        if (!CategorySpecs.Validation(category, ValidationPurpose.Update, ref info))
                        {
                            throw new MyException(_namespace, _className, "Store()", info);
                        }

                        if (!this.Exists(category, DataState.All))
                        {
                            throw new MyException(_namespace, _className, "Store()", string.Format("{0}!!!", GlobalVariables.Resource.GetString("NotExistingUpdateCodeString", GlobalVariables.Culture)));
                        }


                        SQLUpdate(category);
                        break;

                    }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        public void Delete(Category category)
        {
            string info = "";

            if (!CategorySpecs.Validation(category, ValidationPurpose.Delete, ref info))
            {
                throw new MyException(_namespace, _className, "Delete()", info);
            }

            if (!this.Exists(category, DataState.All))
            {
                throw new MyException(_namespace, _className, "Delete()", string.Format("{0}!!!", GlobalVariables.Resource.GetString("NotExistingDeleteCodeString", GlobalVariables.Culture)));
            }

            this.SQLDelete(category);

        }


        #endregion



        #region sql stuff

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        private void SQLInsert(Category category)
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

                affectedRecords = engine.SqlServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, _procedureInsertName, true, this.Serialize(category, SqlOperationType.Insert));

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
        /// <param name="category"></param>
        private void SQLUpdate(Category category)
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

                affectedRecords = engine.SqlServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, _procedureUpdateName, true, this.Serialize(category, SqlOperationType.Update));

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
        /// <param name="category"></param>
        private void SQLDelete(Category category)
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

                affectedRecords = engine.SqlServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, _procedureDeleteName, true, this.Serialize(category, SqlOperationType.Delete));

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
