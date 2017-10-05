using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class QuotationRuleSpecs
    {
        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "QuotationRuleSpecs";

        //private Supplier supplier;
        //private Brand brand;
        //private Category category;
        //private Stock stock;
        //private short expirationHours;
        //private Stock substituteStock;
        //private DateTime dataReset;
        //private string notes;

        public const bool ExpirationHours_Necesssary = true;
        public const short ExpirationHours_MinSize = 0;
        public const short ExpirationHours_MaxSize = short.MaxValue;

        public const bool DataReset_Necesssary = false;
        //public const string DataReset_MinSize = DateTime.MinValue.ToString();
        //public const string DataReset_MaxSize = DateTime.MaxValue.ToString();

        public const bool Notes_Necesssary = false;
        public const int Notes_MinSize = 0;
        public const int Notes_MaxSize = 256;

        public static QuotationRule New()
        {
            QuotationRule quotationRule = new QuotationRule();
            quotationRule.EditionMode = false;
            return quotationRule;
        }


        public static void Copia(QuotationRule original, out QuotationRule copy)
        {

            QuotationRule quotationRule = New();
            quotationRule.Supplier = null;
            quotationRule.Brand = null;
            quotationRule.Category = null;
            quotationRule.Stock = null;
            quotationRule.ExpitationHours = original.ExpitationHours;
            quotationRule.SubstituteStock = original.SubstituteStock;
            quotationRule.DataReset = original.DataReset;
            quotationRule.Notes = original.Notes;
            quotationRule.EditionMode = false;
            copy = quotationRule;
        }


        public static string Describe(QuotationRule quotationRule)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'; [{11}]='{12}'; [{13}]='{14}'";
            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("QuotationRuleString", GlobalVariables.Culture),
                                                                     GlobalVariables.Resource.GetString("SupplierCodeString", GlobalVariables.Culture), quotationRule.Supplier.Code,
                                                                     GlobalVariables.Resource.GetString("BrandCodeString", GlobalVariables.Culture), quotationRule.Brand.Code,
                                                                     GlobalVariables.Resource.GetString("CategoryCodeString", GlobalVariables.Culture), quotationRule.Category.Code,
                                                                     GlobalVariables.Resource.GetString("ExpireHoursString", GlobalVariables.Culture), quotationRule.ExpitationHours.ToString(),
                                                                     GlobalVariables.Resource.GetString("SubstituteStockCodeString", GlobalVariables.Culture), quotationRule.SubstituteStock != null ? quotationRule.SubstituteStock.Code : string.Empty,
                                                                     GlobalVariables.Resource.GetString("DataResetString", GlobalVariables.Culture), quotationRule.DataReset != null ? quotationRule.DataReset.ToString(): string.Empty,
                                                                     GlobalVariables.Resource.GetString("NotesString", GlobalVariables.Culture), quotationRule.Notes != null ? quotationRule.Notes : string.Empty,                                                                     
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), quotationRule.Version.ToString());

            return completeDescription;
        }


        public static bool Validation(QuotationRule quotationRule, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            ExpirationHoursValidation(quotationRule.ExpitationHours, ref msg);
            NotesValidation(quotationRule.Notes, ref msg);
            DataResetValidation(quotationRule.DataReset, ref msg);
            EditionModeEdition(quotationRule.EditionMode, validationPurpose, ref msg);

            if (msg.Trim().Length > 0)
            {
                info += msg;
                return false;
            }

            return true;
        }


        public static void NotesValidation(string notes, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("NotesString", GlobalVariables.Culture), notes, Notes_Necesssary, true, Notes_MinSize, Notes_MaxSize, ref info);
        }

        public static void ExpirationHoursValidation(short expirationHours, ref string info)
        {

            SystemValidation.Int16(GlobalVariables.Resource.GetString("ExpireHoursString", GlobalVariables.Culture), expirationHours, ExpirationHours_Necesssary, ExpirationHours_MinSize, ExpirationHours_MaxSize, ref info);
        }

        public static void DataResetValidation(DateTime? dataReset, ref string info)
        {

            SystemValidation.Data(GlobalVariables.Resource.GetString("DataResetString", GlobalVariables.Culture), dataReset, DataReset_Necesssary, DateTime.Parse("01/01/1900 00:00:00"), DateTime.Parse("2079-06-06 00:00:00"), ref info);
        }

        public static void EditionModeEdition(bool modoEdicao, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            switch (validationPurpose)
            {
                case ValidationPurpose.Select:
                    // Sem regras
                    break;
                case ValidationPurpose.Insert:
                    if (modoEdicao != false)
                    {
                        msg += string.Format("#{0}$ {1} ValidationPurpose.Insert!", GlobalVariables.Resource.GetString("InconsistencyString", GlobalVariables.Culture).ToLower(), GlobalVariables.Resource.GetString("BooleanInconsistentString", GlobalVariables.Culture).ToLower());
                    }
                    break;
                case ValidationPurpose.Update:
                    if (modoEdicao != true)
                    {
                        msg += string.Format("#{0}$ {1} ValidationPurpose.Update!", GlobalVariables.Resource.GetString("InconsistencyString", GlobalVariables.Culture).ToLower(), GlobalVariables.Resource.GetString("BooleanInconsistentString", GlobalVariables.Culture).ToLower());
                    }
                    break;
                case ValidationPurpose.Delete:
                    if (modoEdicao != true)
                    {
                        msg += string.Format("#{0}$ {1} ValidationPurpose.Delete!", GlobalVariables.Resource.GetString("InconsistencyString", GlobalVariables.Culture).ToLower(), GlobalVariables.Resource.GetString("BooleanInconsistentString", GlobalVariables.Culture).ToLower());
                    }
                    break;
                default:
                    throw new MyException(_namespace, _className, "EditionModeValidation()", string.Format("{0} ValidationPurpose!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));

            }

            if (msg.Length > 0)
            {
                info += "[QuotationRule.EditionMode]" + msg;
            }
        }
    }
}
