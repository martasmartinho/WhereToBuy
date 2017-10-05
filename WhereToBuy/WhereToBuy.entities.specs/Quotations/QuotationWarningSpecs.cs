using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs.Quotations
{
    public static class QuotationWarningSpecs
    {
        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        //static string _className = "QuotationWarningSpecs";


        public const bool Id_Necesssary = true;

        public const bool ProductCode_Necesssary = true;
        public const int ProductCode_MinSize = 1;
        public const int ProductCode_MaxSize = 256;

        public const bool SupplementCode_Necesssary = true;
        public const int SupplementCode_MinSize = 1;
        public const int SupplementCode_MaxSize = 126;

       
        public const bool Description_Necesssary = true;
        public const int Description_MinSize = 1;
        public const int Description_MaxSize = 2048;



        public static QuotationWarning New()
        {
            QuotationWarning warning = new QuotationWarning();
            warning.Id = Guid.NewGuid();
            warning.Date = DateTime.Now;
            return warning;
        }


        public static void Copia(QuotationWarning original, out QuotationWarning copy)
        {

            QuotationWarning warning = New();
            warning.Supplier = original.Supplier;
            warning.ProductCode = original.ProductCode;
            warning.SupplementCode = original.SupplementCode;
            warning.WarningType = original.WarningType;
            warning.Description = original.Description;
            warning.Date = original.Date;
            copy = warning;
        }


        public static string Describe(QuotationWarning warning)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'; [{11}]='{12}'; [{13}]='{14}'; [{15}]='{16}'";
            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("WarningString", GlobalVariables.Culture),
                                                                     "Id", warning.Id,
                                                                     GlobalVariables.Resource.GetString("SupplierCodeString", GlobalVariables.Culture), warning.Supplier.Code,
                                                                     GlobalVariables.Resource.GetString("ProductString", GlobalVariables.Culture), warning.ProductCode,
                                                                     GlobalVariables.Resource.GetString("SupplementString", GlobalVariables.Culture), warning.SupplementCode,
                                                                     GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), warning.Description,
                                                                     GlobalVariables.Resource.GetString("WarningTypeString", GlobalVariables.Culture), warning.WarningType,
                                                                     GlobalVariables.Resource.GetString("DateString", GlobalVariables.Culture), warning.Date.ToString(),
                                                                     GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), warning.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(QuotationWarning warning, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            IdValidation(warning.Id.ToString(), ref msg);
            ProductCodeValidation(warning.ProductCode, ref msg);
            SupplementCodeValidation(warning.SupplementCode, ref msg);
            DescriptionValidation(warning.Description, ref msg);

            if (msg.Trim().Length > 0)
            {
                info += msg;
                return false;
            }

            return true;
        }


        public static void IdValidation(string id, ref string info)
        {
            string msg = "";

            SystemValidation.Guid("Id", id, Id_Necesssary, ref info);

            if (msg.Length > 0)
            {
                info += "[QuotationWarning.Id]" + msg;
            }

        }


        public static void DescriptionValidation(string description, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), description, Description_Necesssary, true, Description_MinSize, Description_MaxSize, ref info);
        }

        public static void ProductCodeValidation(string productCode, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("ProductString", GlobalVariables.Culture), productCode, ProductCode_Necesssary, true, ProductCode_MinSize, ProductCode_MaxSize, ref info);
        }

        public static void SupplementCodeValidation(string supplementCode, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("SupplementString", GlobalVariables.Culture), supplementCode, SupplementCode_Necesssary, true, SupplementCode_MinSize,SupplementCode_MaxSize, ref info);
        }


    }
}
