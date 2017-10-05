using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class StockMatchingSpecs
    {
        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "StockMatchingSpecs";


        public const bool Code_Necesssary = true;
        public const int Code_MinSize = 1;
        public const int Code_MaxSize = 128;


        public const bool Description_Necesssary = true;
        public const int Description_MinSize = 3;
        public const int Description_MaxSize = 128;



        public static StockMatching New()
        {
            StockMatching stockMatching = new StockMatching();
            stockMatching.Inactive = false;
            stockMatching.EditionMode = false;
            return stockMatching;
        }


        public static void Copia(StockMatching original, out StockMatching copy)
        {

            StockMatching stockMatching = New();
            stockMatching.Supplier = null;
            stockMatching.Code = null;
            stockMatching.Description = original.Description;
            stockMatching.MapTo = original.MapTo;
            stockMatching.Inactive = original.Inactive;
            stockMatching.EditionMode = false;
            copy = stockMatching;
        }


        public static string Describe(StockMatching stockMatching)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'; [{11}]='{12}'; [{13}]='{14}'";
            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("StockMatchingString", GlobalVariables.Culture),
                                                                     GlobalVariables.Resource.GetString("SupplierCodeString", GlobalVariables.Culture), stockMatching.Supplier.Code,
                                                                     GlobalVariables.Resource.GetString("CodeString", GlobalVariables.Culture), stockMatching.Code,
                                                                     GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), stockMatching.Description,
                                                                     GlobalVariables.Resource.GetString("MatchingString", GlobalVariables.Culture), stockMatching.MapTo != null ? stockMatching.MapTo.Code : string.Empty,
                                                                     GlobalVariables.Resource.GetString("InactiveString", GlobalVariables.Culture), stockMatching.Inactive.ToString(),
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), stockMatching.Version.ToString(),
                                                                     GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), stockMatching.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(StockMatching stockMatching, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            CodeValidation(stockMatching.Code, ref msg);
            DescriptionValidation(stockMatching.Description, ref msg);
            EditionModeEdition(stockMatching.EditionMode, validationPurpose, ref msg);

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
                info += "[StockMatching.Code]" + msg;
            }

        }


        public static void DescriptionValidation(string description, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), description, Description_Necesssary, true, Description_MinSize, Description_MaxSize, ref info);
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
                info += "[StockMatching.EditionMode]" + msg;
            }
        }
    }
}
