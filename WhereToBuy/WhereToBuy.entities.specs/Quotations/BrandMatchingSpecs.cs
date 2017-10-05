using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class BrandMatchingSpecs
    {

        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "BrandMatchingSpecs";


        public const bool Code_Necesssary = true;
        public const int Code_MinSize = 1;
        public const int Code_MaxSize = 128;


        public const bool Description_Necesssary = true;
        public const int Description_MinSize = 3;
        public const int Description_MaxSize = 128;



        public static BrandMatching New()
        {
            BrandMatching brandMatching = new BrandMatching();
            brandMatching.Inactive = false;
            brandMatching.EditionMode = false;
            return brandMatching;
        }


        public static void Copia(BrandMatching original, out BrandMatching copy)
        {

            BrandMatching brandMatching = New();
            brandMatching.Supplier = null;
            brandMatching.Code = null;
            brandMatching.Description = original.Description;
            brandMatching.MapTo = original.MapTo;
            brandMatching.Inactive = original.Inactive;
            brandMatching.EditionMode = false;  
            copy = brandMatching;
        }


        public static string Describe(BrandMatching brandMatching)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'; [{11}]='{12}'; [{13}]='{14}'";
            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("BrandMatchingString", GlobalVariables.Culture),
                                                                     GlobalVariables.Resource.GetString("SupplierCodeString", GlobalVariables.Culture), brandMatching.Supplier.Code,
                                                                     GlobalVariables.Resource.GetString("CodeString", GlobalVariables.Culture), brandMatching.Code,
                                                                     GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), brandMatching.Description,
                                                                     GlobalVariables.Resource.GetString("MatchingString", GlobalVariables.Culture),  brandMatching.MapTo != null ? brandMatching.MapTo.Code : string.Empty,
                                                                     GlobalVariables.Resource.GetString("InactiveString", GlobalVariables.Culture), brandMatching.Inactive.ToString(),
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), brandMatching.Version.ToString(), 
                                                                     GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), brandMatching.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(BrandMatching brandMatching, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            CodeValidation(brandMatching.Code, ref msg);
            DescriptionValidation(brandMatching.Description, ref msg);
            EditionModeEdition(brandMatching.EditionMode, validationPurpose, ref msg);

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
                info += "[BrandMatching.Code]" + msg;
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
                info += "[BrandMatching.EditionMode]" + msg;
            }
        }
    }
}
