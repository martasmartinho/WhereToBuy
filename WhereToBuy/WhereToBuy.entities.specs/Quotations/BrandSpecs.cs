using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class BrandSpecs
    {
        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "BrandSpecs";


        /*
            brand.Codigo;
            brand.Descricao;
            brand.Inativo;
            brand.Criacao;
            brand.Versao;
            bran.ModoEdicao;
        */


        public const bool Code_Necesssary = true;
        public const int Code_MinSize = 1;
        public const int Code_MaxSize = 5;


        public const bool Description_Necesssary = true;
        public const int Description_MinSize = 4;
        public const int Description_MaxSize = 50;



        public static Brand New()
        {
            Brand brand = new Brand();
            brand.Inactive = false;
            brand.EditionMode = false;
            return brand;
        }


        public static void Copia(Brand original, out Brand copy)
        {

            Brand brand = New();
            brand.Code = null;
            brand.Description = original.Description;
            brand.Inactive = original.Inactive;
            brand.EditionMode  = false;  //redundante, porque Novo() já trazia esta configuração
            copy = brand;
        }


        public static string Describe(Brand brand)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'";
            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("BrandString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("CodeString", GlobalVariables.Culture), brand.Code, 
                                                                     GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), brand.Description, GlobalVariables.Resource.GetString("InactiveString", GlobalVariables.Culture), brand.Inactive.ToString(),
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), brand.Version.ToString(),GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), brand.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(Brand brand, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            CodeValidation(brand.Code, ref msg);
            DescriptionValidation(brand.Description, ref msg);
            EditionModeEdition(brand.EditionMode, validationPurpose, ref msg);

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
                info += "[Brand.Code]" + msg;
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
                info += "[Brand.EditionMode]" + msg;
            }
        }
    }
}
