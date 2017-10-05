using System;
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
    public partial class BrandsMatching
    {

        #region store and delete

        string _procedureInsertName = "BrandMatchingInsert";
        string _procedureUpdateName = "BrandMatchingUpdate";
        string _procedureDeleteName = "BrandMatchingDelete";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandMatching"></param>
        public void Store(BrandMatching brandMatching)
        {
            string info = "";

            switch (brandMatching.EditionMode)
            {
                case false:
                    {

                        if (!BrandMatchingSpecs.Validation(brandMatching, ValidationPurpose.Insert, ref info))
                        {
                            throw new MyException(_namespace, _className, "Store()", info);
                        }

                        if (this.Exists(brandMatching.Supplier, brandMatching.Code, DataState.All))
                        {
                            throw new MyException(_namespace, _className, "Store()", string.Format("{0}!!!", GlobalVariables.Resource.GetString("ExistingInsertCodeString", GlobalVariables.Culture)));
                        }

                        SQLInsert(brandMatching);
                        break;

                    }
                case true:
                    {

                        if (!BrandMatchingSpecs.Validation(brandMatching, ValidationPurpose.Update, ref info))
                        {
                            throw new MyException(_namespace, _className, "Store()", info);
                        }

                        if (!this.Exists(brandMatching, DataState.All))
                        {
                            throw new MyException(_namespace, _className, "Store()", string.Format("{0}!!!", GlobalVariables.Resource.GetString("NotExistingUpdateCodeString", GlobalVariables.Culture)));
                        }


                        SQLUpdate(brandMatching);
                        break;

                    }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandMatching"></param>
        public void Delete(BrandMatching brandMatching)
        {
            string info = "";

            if (!BrandMatchingSpecs.Validation(brandMatching, ValidationPurpose.Delete, ref info))
            {
                throw new MyException(_namespace, _className, "Delete()", info);
            }

            if (!this.Exists(brandMatching, DataState.All))
            {
                throw new MyException(_namespace, _className, "Delete()", string.Format("{0}!!!", GlobalVariables.Resource.GetString("NotExistingDeleteCodeString", GlobalVariables.Culture)));
            }

            this.SQLDelete(brandMatching);

        }


        #endregion



        #region sql stuff

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandMatching"></param>
        private void SQLInsert(BrandMatching brandMatching)
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

                affectedRecords = engine.SqlServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, _procedureInsertName, true, this.Serialize(brandMatching, SqlOperationType.Insert));

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
        /// <param name="brandMatching"></param>
        private void SQLUpdate(BrandMatching brandMatching)
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

                affectedRecords = engine.SqlServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, _procedureUpdateName, true, this.Serialize(brandMatching, SqlOperationType.Update));

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
        /// <param name="brandMatching"></param>
        private void SQLDelete(BrandMatching brandMatching)
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

                affectedRecords = engine.SqlServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, _procedureDeleteName, true, this.Serialize(brandMatching, SqlOperationType.Delete));

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
