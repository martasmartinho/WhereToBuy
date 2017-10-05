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
    public partial class Stocks
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        Stock Deserialize(ref SqlDataReader sqlDataReader)
        {
            Stock stock = new Stock();

            stock.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            stock.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();
            stock.AvailabilityLevel = ((short)sqlDataReader["DisponibilidadeNivel"]);
            stock.Notes = sqlDataReader["Notas"] == DBNull.Value ? string.Empty : ((string)sqlDataReader["Notas"]).TrimEnd();


            stock.MetaInfo = new Dictionary<string, object>();
            stock.MetaInfo.Add("StockCodeExpirationP50.Code", (object)sqlDataReader["ValidadeP50Codigo"]);
            //stock.MetaInfo.Add("StockCodeExpirationP50.Description", (object)sqlDataReader["ValidadeP50Descricao"]);
            //stock.MetaInfo.Add("StockCodeExpirationP50.AvailabilityLevel", (object)sqlDataReader["ValidadeP50DisponibilidadeNivel"]);

            stock.MetaInfo.Add("StockCodeExpirationP60.Code", (object)sqlDataReader["ValidadeP60Codigo"]);
            //stock.MetaInfo.Add("StockCodeExpirationP60.Description", (object)sqlDataReader["ValidadeP60Descricao"]);
            //stock.MetaInfo.Add("StockCodeExpirationP60.AvailabilityLevel", (object)sqlDataReader["ValidadeP60DisponibilidadeNivel"]);

            stock.MetaInfo.Add("StockCodeExpirationP70.Code", (object)sqlDataReader["ValidadeP70Codigo"]);
            //stock.MetaInfo.Add("StockCodeExpirationP70.Description", (object)sqlDataReader["ValidadeP70Descricao"]);
            //stock.MetaInfo.Add("StockCodeExpirationP70.AvailabilityLevel", (object)sqlDataReader["ValidadeP70DisponibilidadeNivel"]);

            stock.MetaInfo.Add("StockCodeExpirationP80.Code", (object)sqlDataReader["ValidadeP80Codigo"]);
            ////stock.MetaInfo.Add("StockCodeExpirationP80.Description", (object)sqlDataReader["ValidadeP80Descricao"]);
            ////stock.MetaInfo.Add("StockCodeExpirationP80.AvailabilityLevel", (object)sqlDataReader["ValidadeP80DisponibilidadeNivel"]);

            stock.MetaInfo.Add("StockCodeExpirationP90.Code", (object)sqlDataReader["ValidadeP90Codigo"]);
            //stock.MetaInfo.Add("StockCodeExpirationP90.Description", (object)sqlDataReader["ValidadeP90Descricao"]);
            //stock.MetaInfo.Add("StockCodeExpirationP90.AvailabilityLevel", (object)sqlDataReader["ValidadeP90DisponibilidadeNivel"]);

            stock.Inactive = (bool)sqlDataReader["Inativo"];
            stock.Creation = (DateTime)sqlDataReader["Criacao"];
            stock.Version = (DateTime)sqlDataReader["Versao"];
            stock.EditionMode = true;

            return stock;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(Stock stock, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@Codigo", stock.Code.ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(stock.Description)));
                    sqlParameters.Add(new SqlParameter("@DisponibilidadeNivel", stock.AvailabilityLevel));

                    if (stock.StockCodeExpirationP50 != null)
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP50_StockCodigo", SQLStrings.CleanDangerousText(stock.StockCodeExpirationP50.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP50_StockCodigo", DBNull.Value));
                    }

                    if (stock.StockCodeExpirationP60 != null)
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP60_StockCodigo", SQLStrings.CleanDangerousText(stock.StockCodeExpirationP60.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP60_StockCodigo", DBNull.Value));
                    }

                    if (stock.StockCodeExpirationP70 != null)
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP70_StockCodigo", SQLStrings.CleanDangerousText(stock.StockCodeExpirationP70.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP70_StockCodigo", DBNull.Value));
                    }

                    if (stock.StockCodeExpirationP80 != null)
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP80_StockCodigo", SQLStrings.CleanDangerousText(stock.StockCodeExpirationP80.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP80_StockCodigo", DBNull.Value));
                    }

                    if (stock.StockCodeExpirationP90 != null)
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP90_StockCodigo", SQLStrings.CleanDangerousText(stock.StockCodeExpirationP90.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP90_StockCodigo", DBNull.Value));
                    }

                    if (stock.Notes != string.Empty && stock.Notes != null)
                    {
                        sqlParameters.Add(new SqlParameter("@Notas", SQLStrings.CleanDangerousText(stock.Notes)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@Notas", DBNull.Value));
                    }

                    sqlParameters.Add(new SqlParameter("@Inativo", stock.Inactive));
                    break;

                case SqlOperationType.Update:
                    
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(stock.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(stock.Description)));
                    sqlParameters.Add(new SqlParameter("@DisponibilidadeNivel", stock.AvailabilityLevel));

                    
                    if (stock.StockCodeExpirationP50 != null)
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP50_StockCodigo", SQLStrings.CleanDangerousText(stock.StockCodeExpirationP50.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP50_StockCodigo", DBNull.Value));
                    }

                    if (stock.StockCodeExpirationP60 != null)
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP60_StockCodigo", SQLStrings.CleanDangerousText(stock.StockCodeExpirationP60.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP60_StockCodigo", DBNull.Value));
                    }

                    if (stock.StockCodeExpirationP70 != null)
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP70_StockCodigo", SQLStrings.CleanDangerousText(stock.StockCodeExpirationP70.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP70_StockCodigo", DBNull.Value));
                    }

                    if (stock.StockCodeExpirationP80 != null)
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP80_StockCodigo", SQLStrings.CleanDangerousText(stock.StockCodeExpirationP80.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP80_StockCodigo", DBNull.Value));
                    }

                    if (stock.StockCodeExpirationP90 != null)
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP90_StockCodigo", SQLStrings.CleanDangerousText(stock.StockCodeExpirationP90.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@ValidadeP90_StockCodigo", DBNull.Value));
                    }

                    if (stock.Notes != string.Empty && stock.Notes != null)
                    {
                        sqlParameters.Add(new SqlParameter("@Notas", SQLStrings.CleanDangerousText(stock.Notes)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@Notas", DBNull.Value));
                    }

                    sqlParameters.Add(new SqlParameter("@Inativo", stock.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", stock.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(stock.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", stock.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
