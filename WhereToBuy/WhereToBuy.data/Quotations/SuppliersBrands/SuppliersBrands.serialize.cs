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
    public partial class SuppliersBrands
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        SupplierBrand Deserialize(ref SqlDataReader sqlDataReader)
        {
            SupplierBrand supplierBrand = new SupplierBrand();

            supplierBrand.Trust = ((double)sqlDataReader["Confianca"]);
            supplierBrand.Notes = sqlDataReader["Notas"] == DBNull.Value ? string.Empty : ((string)sqlDataReader["Notas"]).TrimEnd();

            supplierBrand.MetaInfo = new Dictionary<string, object>();
            supplierBrand.MetaInfo.Add("Supplier.Code", (object)sqlDataReader["FornecedorCodigo"]);
            supplierBrand.MetaInfo.Add("Supplier.Name", (object)sqlDataReader["FornecedorNome"]);
            supplierBrand.MetaInfo.Add("Brand.Code", (object)sqlDataReader["MarcaCodigo"].ToString());
            supplierBrand.MetaInfo.Add("Brand.Description", (object)sqlDataReader["MarcaDescricao"].ToString());
        
            supplierBrand.Version = (DateTime)sqlDataReader["Versao"];
            supplierBrand.EditionMode = true;

            return supplierBrand;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierBrand"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(SupplierBrand supplierBrand, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(supplierBrand.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@MarcaCodigo", SQLStrings.CleanDangerousText(supplierBrand.Brand.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Confianca", supplierBrand.Trust));
                    if (supplierBrand.Notes != string.Empty && supplierBrand.Notes != null)
                    {
                        sqlParameters.Add(new SqlParameter("@Notas", SQLStrings.CleanDangerousText(supplierBrand.Notes)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@Notas", DBNull.Value));
                    }

                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(supplierBrand.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@MarcaCodigo", SQLStrings.CleanDangerousText(supplierBrand.Brand.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Confianca", supplierBrand.Trust));
                    if (supplierBrand.Notes != string.Empty)
                    {
                        sqlParameters.Add(new SqlParameter("@Notas", SQLStrings.CleanDangerousText(supplierBrand.Notes)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@Notas", DBNull.Value));
                    }
                    sqlParameters.Add(new SqlParameter("@Versao", supplierBrand.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(supplierBrand.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@MarcaCodigo", SQLStrings.CleanDangerousText(supplierBrand.Brand.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Versao", supplierBrand.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
