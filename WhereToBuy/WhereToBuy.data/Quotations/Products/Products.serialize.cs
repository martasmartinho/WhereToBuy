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
    public partial class Products
    {
         /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        Product Deserialize(ref SqlDataReader sqlDataReader)
        {
            Product product = new Product();

            product.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            product.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();
            product.Partnumber = ((string)sqlDataReader["Partnumber"]).TrimEnd();

            product.CostPrice = sqlDataReader["PrecoCusto"] == DBNull.Value ? 0 : (decimal)sqlDataReader["PrecoCusto"];
            product.CostPrice_Date = sqlDataReader["PrecoCusto_Data"] == DBNull.Value ? default(DateTime) : (DateTime)sqlDataReader["PrecoCusto_Data"];
            product.CostPrice_U1 = sqlDataReader["PrecoCusto_U1"] == DBNull.Value ? 0 : (decimal)sqlDataReader["PrecoCusto_U1"];
            product.CostPrice_U1Date = sqlDataReader["PrecoCusto_U1Data"] == DBNull.Value ? default(DateTime) : (DateTime)sqlDataReader["PrecoCusto_U1Data"];
            product.CostPrice_U2 = sqlDataReader["PrecoCusto_U2"] == DBNull.Value ? 0 : (decimal)sqlDataReader["PrecoCusto_U2"];
            product.CostPrice_U2Date = sqlDataReader["PrecoCusto_U2Data"] == DBNull.Value ? default(DateTime) : (DateTime)sqlDataReader["PrecoCusto_U2Data"];
            product.CostPrice_U3 = sqlDataReader["PrecoCusto_U3"] == DBNull.Value ? 0 : (decimal)sqlDataReader["PrecoCusto_U3"];
            product.CostPrice_U3Date = sqlDataReader["PrecoCusto_U3Data"] == DBNull.Value ? default(DateTime) : (DateTime)sqlDataReader["PrecoCusto_U3Data"];
            product.Stock_Date = sqlDataReader["StockCodigo_Data"] == DBNull.Value ? default(DateTime) : (DateTime)sqlDataReader["StockCodigo_Data"];
            product.Stock_U1Date = sqlDataReader["StockCodigo_U1Data"] == DBNull.Value ? default(DateTime) : (DateTime)sqlDataReader["StockCodigo_U1Data"];
            product.Stock_U2Date = sqlDataReader["StockCodigo_U2Data"] == DBNull.Value ? default(DateTime) : (DateTime)sqlDataReader["StockCodigo_U2Data"];
            product.Stock_U3Date = sqlDataReader["StockCodigo_U3Data"] == DBNull.Value ? default(DateTime) : (DateTime)sqlDataReader["StockCodigo_U3Data"];
            product.ContentConcernIndex = (int)sqlDataReader["IPC"];
            product.EEP = (double)sqlDataReader["EEP"];
            product.EEPFormula = (string)sqlDataReader["EEPFormula"];
            product.ICP = (double)sqlDataReader["ICP"];
            product.ICPFormula = (string)sqlDataReader["ICPFormula"];
            product.ICPCE = (double)sqlDataReader["ICPCE"];
            product.ICPCEFormula = (string)sqlDataReader["ICPCEFormula"];
            product.ICPCT = (double)sqlDataReader["ICPCT"];
            product.ICPCEFormula = (string)sqlDataReader["ICPCTFormula"];
            product.ICPCF = (double)sqlDataReader["ICPCF"];
            product.ICPCFFormula = (string)sqlDataReader["ICPCFFormula"];
            product.EED = (double)sqlDataReader["EED"];
            product.EEDFormula = (string)sqlDataReader["EEDFormula"];
            product.ICD = (double)sqlDataReader["ICD"];
            product.ICDFormula = (string)sqlDataReader["ICDFormula"];
            product.ICDCE = (double)sqlDataReader["ICDCE"];
            product.ICDCEFormula = (string)sqlDataReader["ICDCEFormula"];
            product.ICDCT = (double)sqlDataReader["ICDCT"];
            product.ICDCTFormula = (string)sqlDataReader["ICDCEFormula"];
            product.ICDCF = (double)sqlDataReader["ICDCF"];
            product.ICDCFFormula = (string)sqlDataReader["ICDCFFormula"];

            product.MetaInfo = new Dictionary<string, object>();
            product.MetaInfo.Add("Category.Code", (object)sqlDataReader["CategoriaCodigo"]);
            product.MetaInfo.Add("Brand.Code", (object)sqlDataReader["MarcaCodigo"].ToString());
            product.MetaInfo.Add("Supplier.Code", (object)sqlDataReader["FornecedorCodigo"].ToString());
            //product.MetaInfo.Add("Supplier.InactiveAutomaticUpdateSuggestion", (object)sqlDataReader["AtualizacaoAutomatica"].ToString());
            product.MetaInfo.Add("Supplier.ProductPriceTrust", (object)sqlDataReader["ProdutosConfiancaPreco"].ToString());
            product.MetaInfo.Add("Supplier.ProductAvailableTrust", (object)sqlDataReader["ProdutosConfiancaDisponibilidade"].ToString());
            product.MetaInfo.Add("Tax.Code", (object)sqlDataReader["ImpostoCodigo"].ToString());
            product.MetaInfo.Add("Stock.Code", (object)sqlDataReader["StockCodigo"].ToString());
            product.MetaInfo.Add("Stock.Description", (object)sqlDataReader["StockDescricao"].ToString());
            product.MetaInfo.Add("Stock_U1.Code", (object)sqlDataReader["StockCodigo_U1"].ToString());
            product.MetaInfo.Add("Stock_U1.Description", (object)sqlDataReader["StockDescricao_U1"].ToString());
            product.MetaInfo.Add("Stock_U2.Code", (object)sqlDataReader["StockCodigo_U2"].ToString());
            product.MetaInfo.Add("Stock_U2.Description", (object)sqlDataReader["StockDescricao_U2"].ToString());
            product.MetaInfo.Add("Stock_U3.Code", (object)sqlDataReader["StockCodigo_U3"].ToString());
            product.MetaInfo.Add("Stock_U3.Description", (object)sqlDataReader["StockDescricao_U3"].ToString());

            product.Discontinued = (bool)sqlDataReader["Descontinuado"];
            product.Inactive = (bool)sqlDataReader["Inativo"];
            product.Creation = (DateTime)sqlDataReader["Criacao"];
            product.Version = (DateTime)sqlDataReader["Versao"];
            product.EditionMode = true;
            return product;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(Product product, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(product.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao",""));
                    sqlParameters.Add(new SqlParameter("@Partnumber", SQLStrings.CleanDangerousText(product.Partnumber)));
                    sqlParameters.Add(new SqlParameter("@CategoriaCodigo", SQLStrings.CleanDangerousText(product.Category.Code)));
                    sqlParameters.Add(new SqlParameter("@MarcaCodigo", SQLStrings.CleanDangerousText(product.Brand.Code)));
                    sqlParameters.Add(new SqlParameter("@ImpostoCodigo", SQLStrings.CleanDangerousText(product.Tax.Code)));
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(product.Supplier.Code)));
                    sqlParameters.Add(new SqlParameter("@PrecoCusto", DBNull.Value));
                    sqlParameters.Add(new SqlParameter("@PrecoCusto_Data", DBNull.Value));
                    sqlParameters.Add(new SqlParameter("@PrecoCusto_U1", DBNull.Value));
                    sqlParameters.Add(new SqlParameter("@PrecoCusto_U1Data", DBNull.Value));
                    sqlParameters.Add(new SqlParameter("@PrecoCusto_U2", DBNull.Value));
                    sqlParameters.Add(new SqlParameter("@PrecoCusto_U2Data", DBNull.Value));
                    sqlParameters.Add(new SqlParameter("@PrecoCusto_U3", DBNull.Value));
                    sqlParameters.Add(new SqlParameter("@PrecoCusto_U3Data", DBNull.Value));
                    sqlParameters.Add(new SqlParameter("@StockCodigo", DBNull.Value));
                    sqlParameters.Add(new SqlParameter("@StockCodigo_Data", DBNull.Value));
                    sqlParameters.Add(new SqlParameter("@StockCodigo_U1", DBNull.Value));
                    sqlParameters.Add(new SqlParameter("@StockCodigo_U1Data", DBNull.Value));
                    sqlParameters.Add(new SqlParameter("@StockCodigo_U2", DBNull.Value));
                    sqlParameters.Add(new SqlParameter("@StockCodigo_U2Data", DBNull.Value));
                    sqlParameters.Add(new SqlParameter("@StockCodigo_U3", DBNull.Value));
                    sqlParameters.Add(new SqlParameter("@StockCodigo_U3Data", DBNull.Value));

                    sqlParameters.Add(new SqlParameter("@Descontinuado", product.Discontinued));
                    sqlParameters.Add(new SqlParameter("@Inativo", product.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(product.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(product.Description)));
                    sqlParameters.Add(new SqlParameter("@Partnumber", SQLStrings.CleanDangerousText(product.Partnumber)));
                    sqlParameters.Add(new SqlParameter("@CategoriaCodigo", SQLStrings.CleanDangerousText(product.Category.Code)));
                    sqlParameters.Add(new SqlParameter("@MarcaCodigo", SQLStrings.CleanDangerousText(product.Brand.Code)));
                    sqlParameters.Add(new SqlParameter("@ImpostoCodigo", SQLStrings.CleanDangerousText(product.Tax.Code)));
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(product.Supplier.Code)));
                    //sqlParameters.Add(new SqlParameter("@PrecoCusto", product.CostPrice));
                    //sqlParameters.Add(new SqlParameter("@PrecoCusto_Data", product.CostPrice_Date));
                    //sqlParameters.Add(new SqlParameter("@PrecoCusto_U1", product.CostPrice_U1));
                    //sqlParameters.Add(new SqlParameter("@PrecoCusto_U1Data", product.CostPrice_U1Date));
                    //sqlParameters.Add(new SqlParameter("@PrecoCusto_U2", product.CostPrice_U2));
                    //sqlParameters.Add(new SqlParameter("@PrecoCusto_U2Data", product.CostPrice_U2Date));
                    //sqlParameters.Add(new SqlParameter("@PrecoCusto_U3", product.CostPrice_U3));
                    //sqlParameters.Add(new SqlParameter("@PrecoCusto_U3Data", product.CostPrice_U3Date));
                    //sqlParameters.Add(new SqlParameter("@StockCodigo", product.Stock.Code));
                    //sqlParameters.Add(new SqlParameter("@StockCodigo_Data", product.Stock_Date));
                    //sqlParameters.Add(new SqlParameter("@StockCodigo_U1", product.Stock_U1.Code));
                    //sqlParameters.Add(new SqlParameter("@StockCodigo_Data", product.Stock_U1Date));
                    //sqlParameters.Add(new SqlParameter("@StockCodigo", product.Stock_U2.Code));
                    //sqlParameters.Add(new SqlParameter("@StockCodigo_Data", product.Stock_U2Date));
                    //sqlParameters.Add(new SqlParameter("@StockCodigo", product.Stock_U3.Code));
                    //sqlParameters.Add(new SqlParameter("@StockCodigo_Data", product.Stock_U3Date));

                    sqlParameters.Add(new SqlParameter("@Descontinuado", product.Discontinued));
                    sqlParameters.Add(new SqlParameter("@Inativo", product.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", product.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(product.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", product.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}