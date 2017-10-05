using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;
using WhereToBuy.utils;

namespace WhereToBuy.data
{
    public partial class QuotationWarnings
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        QuotationWarning Deserialize(ref SqlDataReader sqlDataReader)
        {
            QuotationWarning warning = new QuotationWarning();

            warning.Id = ((Guid)sqlDataReader["Id"]);
            warning.Date = ((DateTime)sqlDataReader["Data"]);
            warning.ProductCode = ((string)sqlDataReader["_ProdutoCodigo"]).TrimEnd();
            warning.SupplementCode = ((string)sqlDataReader["_ComplementoCodigo"]).TrimEnd();
            warning.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();


            warning.MetaInfo = new Dictionary<string, object>();
            warning.MetaInfo.Add("Supplier.Code", (object)sqlDataReader["FornecedorCodigo"]);
            warning.MetaInfo.Add("Supplier.Name", (object)sqlDataReader["Nome"]);
            warning.MetaInfo.Add("WarningType.Code", (object)sqlDataReader["AvisoTipoCodigo"].ToString());
            warning.MetaInfo.Add("WarningType.Description", (object)sqlDataReader["AvisoTipoDescricao"].ToString());
            warning.MetaInfo.Add("WarningType.Severity", ((short)sqlDataReader["Gravidade"]));
            
            warning.Creation = (DateTime)sqlDataReader["Criacao"];
            return warning;
        }

       
    }
}
