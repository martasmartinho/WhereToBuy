using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class ClasseSpecs
    {
        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "ClassSpecs";




        public const bool Code_Necesssary = true;
        public const int Code_MinSize = 1;
        public const int Code_MaxSize = 5;


        public const bool Description_Necesssary = true;
        public const int Description_MinSize = 4;
        public const int Description_MaxSize = 50;


        public const bool RangeMinValue_Necesssary = true;
        public const decimal RangeMinValue_MinSize = 0;
        public const decimal RangeMinValue_MaxSize = decimal.MaxValue;
        public const int RangeMinValue_MaxNumberDigits = 4;

        public const bool Range_Necesssary = false;
        public const double Range_MinSize = 0;
        public const double Range_MaxSize = double.MaxValue;
        public const int Range_MaxNumberDigits = 4;

        public const bool Notes_Necesssary = false;
        public const int Notes_MinSize = 4;
        public const int Notes_MaxSize = 50;



        public static Classe New()
        {
            Classe classe = new Classe();
            classe.Inactive = false;
            classe.EditionMode = false;
            return classe;
        }


        public static void Copia(Classe original, out Classe copy)
        {

            Classe classe = New();
            classe.Code = null;
            classe.Description = original.Description;
            classe.Inactive = original.Inactive;
            classe.EditionMode = false;  //redundante, porque Novo() já trazia esta configuração
            copy = classe;
        }


        public static string Describe(Classe classe)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'; [{11}]='{12}'; [{13}]='{14}'; [{15}]='{16}'; [{17}]='{18}'";
            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("ClassString", GlobalVariables.Culture), 
                                                                     GlobalVariables.Resource.GetString("CodeString", GlobalVariables.Culture), classe.Code,
                                                                     GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), classe.Description,
                                                                     GlobalVariables.Resource.GetString("CatalogCodeString", GlobalVariables.Culture), classe.Catalog.Code ,
                                                                     GlobalVariables.Resource.GetString("RangeString", GlobalVariables.Culture), classe.Range,
                                                                     GlobalVariables.Resource.GetString("RangeMinValueString", GlobalVariables.Culture), classe.RangeMinValue,
                                                                     GlobalVariables.Resource.GetString("NotesString", GlobalVariables.Culture), classe.RangeMinValue, 
                                                                     GlobalVariables.Resource.GetString("InactiveString", GlobalVariables.Culture), classe.Inactive.ToString(),
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), classe.Version.ToString(), 
                                                                     GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), classe.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(Classe classe, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            CodeValidation(classe.Code, ref msg);
            DescriptionValidation(classe.Description, ref msg);
            RangeMinValueValidation(classe.RangeMinValue, ref msg);
            RangeValidation(classe.Range, ref msg);
            NotesValidation(classe.Notes, ref msg);
            EditionModeEdition(classe.EditionMode, validationPurpose, ref msg);

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
                info += "[Class.Code]" + msg;
            }

        }


        public static void DescriptionValidation(string description, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), description, Description_Necesssary, true, Description_MinSize, Description_MaxSize, ref info);
        }


        public static void RangeMinValueValidation(decimal rangeMinValue, ref string info)
        {

            SystemValidation.Decimal(GlobalVariables.Resource.GetString("RangeMinValueString", GlobalVariables.Culture), rangeMinValue, RangeMinValue_Necesssary, RangeMinValue_MinSize, RangeMinValue_MaxSize, RangeMinValue_MaxNumberDigits, ref info);
        }

        public static void RangeValidation(double range, ref string info)
        {

            SystemValidation.Double(GlobalVariables.Resource.GetString("MinPriceAllowedString", GlobalVariables.Culture), range, Range_Necesssary, Range_MinSize, Range_MaxSize, Range_MaxNumberDigits, ref info);
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
                info += "[Class.EditionMode]" + msg;
            }
        }
    }
}
