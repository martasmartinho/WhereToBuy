using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities.specs;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class ProductMatchingSpecs
    {

        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "ProductMatchingSpecs";


        public const bool Code_Necesssary = true;
        public const int Code_MinSize = 1;
        public const int Code_MaxSize = 128;

        public const bool Supplement_Necesssary = true;
        public const int Supplement_MinSize = 1;
        public const int Supplement_MaxSize = 128;



        public const bool Description_Necesssary = false;
        public const int Description_MinSize = 3;
        public const int Description_MaxSize = 128;

        public const bool Notes_Necesssary = false;
        public const int Notes_MinSize = 0;
        public const int Notes_MaxSize = 126;



        public static ProductMatching New()
        {
            ProductMatching productMatching = new ProductMatching();
            productMatching.NeedPreventionFakeStock = false;
            productMatching.NeedPreventionPricesOut = false;
            productMatching.Inactive = false;
            productMatching.EditionMode = false;
            return productMatching;
        }


        public static void Copia(ProductMatching original, out ProductMatching copy)
        {

            ProductMatching productMatching = New();
            productMatching.Supplier = null;
            productMatching.Supplement = string.Empty;
            productMatching.Code = null;
            productMatching.Description = original.Description;
            productMatching.MapTo = original.MapTo;
            productMatching.QuotationExpireHours = original.QuotationExpireHours;
            productMatching.ReplacementStock = original.ReplacementStock;
            productMatching.NeedPreventionFakeStock = original.NeedPreventionFakeStock;
            productMatching.NeedPreventionPricesOut = original.NeedPreventionPricesOut;
            productMatching.Inactive = original.Inactive;
            productMatching.EditionMode = false;
            copy = productMatching;
        }


        public static string Describe(ProductMatching productMatching)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'; "
                + "[{11}]='{12}'; [{13}]='{14}'; [{15}]='{16}'; [{17}]='{18}'; [{19}]='{20}'; [{21}]='{22}'; "
                + "[{23}]='{24}'; [{25}]='{26}'; [{27}]='{28}'";
            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("ProductMatchingString", GlobalVariables.Culture),
                                                                     GlobalVariables.Resource.GetString("SupplierCodeString", GlobalVariables.Culture), productMatching.Supplier.Code,
                                                                      GlobalVariables.Resource.GetString("SupplementString", GlobalVariables.Culture), productMatching.Supplement,
                                                                     GlobalVariables.Resource.GetString("CodeString", GlobalVariables.Culture), productMatching.Code,
                                                                     GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), productMatching.Description,
                                                                     GlobalVariables.Resource.GetString("MatchingString", GlobalVariables.Culture), productMatching.MapTo != null ? productMatching.MapTo.Code : string.Empty,
                                                                     GlobalVariables.Resource.GetString("SubstituteStockCodeString", GlobalVariables.Culture), productMatching.ReplacementStock != null ? productMatching.ReplacementStock.ToString() : string.Empty,
                                                                     GlobalVariables.Resource.GetString("ExpireHoursString", GlobalVariables.Culture), productMatching.QuotationExpireHours.ToString(),
                                                                     GlobalVariables.Resource.GetString("PreventionPricesOutString", GlobalVariables.Culture), productMatching.NeedPreventionPricesOut.ToString(),
                                                                     GlobalVariables.Resource.GetString("PreventionFakeStockString", GlobalVariables.Culture), productMatching.NeedPreventionFakeStock.ToString(),
                                                                     GlobalVariables.Resource.GetString("DataResetString", GlobalVariables.Culture), productMatching.DataReset.ToString(),
                                                                     GlobalVariables.Resource.GetString("NotesString", GlobalVariables.Culture), productMatching.Notes,
                                                                     GlobalVariables.Resource.GetString("InactiveString", GlobalVariables.Culture), productMatching.Inactive.ToString(),
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), productMatching.Version.ToString(),
                                                                     GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), productMatching.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(ProductMatching productMatching, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            CodeValidation(productMatching.Code, ref msg);
            SupplementValidation(productMatching.Supplement, ref msg);
            DescriptionValidation(productMatching.Description, ref msg);
            NotesValidation(productMatching.Notes, ref msg);
            EditionModeEdition(productMatching.EditionMode, validationPurpose, ref msg);

            if (msg.Trim().Length > 0)
            {
                info += msg;
                return false;
            }

            return true;
        }


        public static void CodeValidation(string code, ref string info)
        {
            SystemValidation.Text(GlobalVariables.Resource.GetString("CodeString", GlobalVariables.Culture), code, Code_Necesssary, true, Code_MinSize, Code_MaxSize, ref info);

            string msg = "";

            if (code != code.ToUpper())
            {
                code = code.ToUpper();
            }

            if (msg.Length > 0)
            {
                info += "[ProductMatching.Code]" + msg;
            }

        }

        public static void SupplementValidation(string supplement, ref string info)
        {
            SystemValidation.Text(GlobalVariables.Resource.GetString("SupplementString", GlobalVariables.Culture), supplement, Supplement_Necesssary, true, Supplement_MinSize, Supplement_MaxSize, ref info);

        }


        public static void DescriptionValidation(string description, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), description, Description_Necesssary, true, Description_MinSize, Description_MaxSize, ref info);
        }


        public static void NotesValidation(string notes, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("NotesString", GlobalVariables.Culture), notes, Notes_Necesssary, true, Notes_MinSize, Notes_MaxSize, ref info);
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
                info += "[ProductMatching.EditionMode]" + msg;
            }
        }

    }
}
