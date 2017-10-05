using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class CatalogSpecs
    {

        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "CatalogSpecs";




        public const bool Code_Necesssary = true;
        public const int Code_MinSize = 1;
        public const int Code_MaxSize = 5;


        public const bool Description_Necesssary = true;
        public const int Description_MinSize = 4;
        public const int Description_MaxSize = 50;

        public const bool Notes_Necesssary = false;
        public const int Notes_MinSize = 0;
        public const int Notes_MaxSize = 256;



        public static Catalog New()
        {
            Catalog catalog = new Catalog();
            catalog.Inactive = false;
            catalog.EditionMode = false;
            return catalog;
        }


        public static void Copia(Catalog original, out Catalog copy)
        {

            Catalog catalog = New();
            catalog.Code = null;
            catalog.Description = original.Description;
            catalog.Notes = original.Notes;
            catalog.Inactive = original.Inactive;
            catalog.EditionMode = false;  //redundante, porque Novo() já trazia esta configuração
            copy = catalog;
        }


        public static string Describe(Catalog catalog)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'; [{11}]='{12}'";
            completeDescription = string.Format(completeDescription, "Catalog", GlobalVariables.Resource.GetString("CodeString", GlobalVariables.Culture), catalog.Code,
                                                                     GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), catalog.Description,
                                                                     GlobalVariables.Resource.GetString("NotesString", GlobalVariables.Culture), catalog.Notes,
                                                                     GlobalVariables.Resource.GetString("InactiveString", GlobalVariables.Culture), catalog.Inactive.ToString(),
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), catalog.Version.ToString(), 
                                                                     GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), catalog.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(Catalog catalog, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            CodeValidation(catalog.Code, ref msg);
            DescriptionValidation(catalog.Description, ref msg);
            NotesValidation(catalog.Notes, ref msg);
            EditionModeEdition(catalog.EditionMode, validationPurpose, ref msg);

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
                info += "[Catalog.Code]" + msg;
            }

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
                info += "[Catalog.EditionMode]" + msg;
            }
        } 
    }
}
