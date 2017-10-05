using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class SupplementSpecs
    {
        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "SupplementSpecs";


        


        public const bool Code_Necesssary = true;
        public const int Code_MinSize = 1;
        public const int Code_MaxSize = 5;


        public const bool Description_Necesssary = true;
        public const int Description_MinSize = 4;
        public const int Description_MaxSize = 50;

        public const bool TextToAdd_Necesssary = false;
        public const int TextToAdd_MinSize = 0;
        public const int TextToAdd_MaxSize = 50;

        public const bool TextToRemove_Necesssary = false;
        public const int TextToRemove_MinSize = 0;
        public const int TextToRemove_MaxSize = 100;



        public static Supplement New()
        {
            Supplement supplement = new Supplement();
            supplement.Inactive = false;
            supplement.EditionMode = false;
            return supplement;
        }


        public static void Copia(Supplement original, out Supplement copy)
        {

            Supplement supplement = New();
            supplement.Code = null;
            supplement.Description = original.Description;
            supplement.TextToAdd = original.TextToAdd;
            supplement.TextToRemove = original.TextToRemove;
            supplement.Inactive = original.Inactive;
            supplement.EditionMode = false;  //redundante, porque Novo() já trazia esta configuração
            copy = supplement;
        }


        public static string Describe(Supplement supplement)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'; [{11}]='{12}'; [{13}]='{14}'";
            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("SupplementString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("CodeString", GlobalVariables.Culture), supplement.Code,
                                                                     GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), supplement.Description,
                                                                     GlobalVariables.Resource.GetString("TextToAddString", GlobalVariables.Culture), supplement.TextToAdd,
                                                                     GlobalVariables.Resource.GetString("TextToRemoveString", GlobalVariables.Culture), supplement.TextToRemove,
                                                                     GlobalVariables.Resource.GetString("InactiveString", GlobalVariables.Culture), supplement.Inactive.ToString(),
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), supplement.Version.ToString(), 
                                                                     GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), supplement.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(Supplement supplement, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            CodeValidation(supplement.Code, ref msg);
            DescriptionValidation(supplement.Description, ref msg);
            TextToAddValidation(supplement.TextToAdd, ref msg);
            TextToRemoveValidation(supplement.TextToRemove, ref msg);
            EditionModeEdition(supplement.EditionMode, validationPurpose, ref msg);

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
                info += "[Supplement.Code]" + msg;
            }

        }


        public static void DescriptionValidation(string description, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), description, Description_Necesssary, true, Description_MinSize, Description_MaxSize, ref info);
        }

        public static void TextToAddValidation(string textToAdd, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("TextToAddString", GlobalVariables.Culture), textToAdd, TextToAdd_Necesssary, true, TextToAdd_MinSize, TextToAdd_MaxSize, ref info);
        }

        public static void TextToRemoveValidation(string textToRemove, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("TextToRemoveString", GlobalVariables.Culture), textToRemove, TextToRemove_Necesssary, true, TextToRemove_MinSize, TextToRemove_MaxSize, ref info);
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
                info += "[Supplement.EditionMode]" + msg;
            }
        }
    }
}
