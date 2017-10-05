using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class TaxSpecs
    {
        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "TaxSpecs";


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

        public const bool TaxDesignation_Necesssary = true;
        public const int TaxDesignation_MinSize = 4;
        public const int TaxDesignation_MaxSize = 50;


        public const bool TaxValue_Necesssary = true;
        public const double TaxValue_MinSize = 0;
        public const double TaxValue_MaxSize = double.MaxValue;
        public const int TaxValue_MaxNumberDigits = 2;


        public static Tax New()
        {
            Tax tax = new Tax();
            tax.Inactive = false;
            tax.EditionMode = false;
            return tax;
        }


        public static void Copia(Tax original, out Tax copy)
        {

            Tax tax = New();
            tax.Code = null;
            tax.Description = original.Description;
            tax.TaxDesignation = original.TaxDesignation;
            tax.TaxValue = original.TaxValue;
            tax.Inactive = original.Inactive;
            tax.EditionMode = false;  //redundante, porque Novo() já trazia esta configuração
            copy = tax;
        }


        public static string Describe(Tax tax)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'; [{11}]='{12}'; [{13}]='{14}'";
            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("TaxString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("CodeString", GlobalVariables.Culture), tax.Code,
                                                                     GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), tax.Description,
                                                                     GlobalVariables.Resource.GetString("TaxDesignationString", GlobalVariables.Culture), tax.TaxDesignation,
                                                                     GlobalVariables.Resource.GetString("TaxValueString", GlobalVariables.Culture), tax.TaxValue.ToString(), 
                                                                     GlobalVariables.Resource.GetString("InactiveString", GlobalVariables.Culture), tax.Inactive.ToString(),
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), tax.Version.ToString(), GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), tax.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(Tax tax, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            CodeValidation(tax.Code, ref msg);
            DescriptionValidation(tax.Description, ref msg);
            TaxDesignationValidation(tax.TaxDesignation, ref msg);
            TaxValueValidation(tax.TaxValue, ref msg);
            EditionModeEdition(tax.EditionMode, validationPurpose, ref msg);

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
                info += "[Tax.Code]" + msg;
            }

        }


        public static void DescriptionValidation(string description, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), description, Description_Necesssary, true, Description_MinSize, Description_MaxSize, ref info);
        }

        public static void TaxDesignationValidation(string taxDesignation, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("TaxDesignationString", GlobalVariables.Culture), taxDesignation, TaxDesignation_Necesssary, true, TaxDesignation_MinSize, TaxDesignation_MaxSize, ref info);
        }

        public static void TaxValueValidation(double taxValue, ref string info)
        {

            SystemValidation.Double(GlobalVariables.Resource.GetString("TaxValueString", GlobalVariables.Culture), taxValue, TaxValue_Necesssary, TaxValue_MinSize, TaxValue_MaxSize, TaxValue_MaxNumberDigits, ref info);
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
                info += "[Tax.EditionMode]" + msg;
            }
        }
    }
}
