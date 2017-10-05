using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class CategorySpecs
    {
        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "CategorySpecs";

       
        public const bool Code_Necesssary = true;
        public const int Code_MinSize = 1;
        public const int Code_MaxSize = 20;


        public const bool Description_Necesssary = true;
        public const int Description_MinSize = 4;
        public const int Description_MaxSize = 128;

        public const bool UnityWeightAverage_Necesssary = true;
        public const double UnityWeightAverage_MinSize = 0;
        public const double UnityWeightAverage_MaxSize = double.MaxValue;
        public const int UnityWeightAverage_MaxNumberDigits = 4;

        public const bool MinPriceAllowed_Necesssary = true;
        public const decimal MinPriceAllowed_MinSize = 0;
        public const decimal MinPriceAllowed_MaxSize = decimal.MaxValue;
        public const int MinPriceAllowed_MaxNumberDigits = 4;

        public const bool MaxPriceAllowed_Necesssary = true;
        public const decimal MaxPriceAllowed_MinSize = 0;
        public const decimal MaxPriceAllowed_MaxSize = decimal.MaxValue;
        public const int MaxPriceAllowed_MaxNumberDigits = 4;

        public const bool MaxPriceAmplitude_Necesssary = false;
        public const double MaxPriceAmplitude_MinSize = 0;
        public const double MaxPriceAmplitude_MaxSize = double.MaxValue;
        public const int MaxPriceAmplitude_MaxNumberDigits = 4;

        public const bool Trust_Necesssary = false;
        public const double Trust_MinSize = 0;
        public const double Trust_MaxSize = 100;
        public const int Trust_MaxNumberDigits = 4;



        public static Category New()
        {
            Category category = new Category();
            category.Inactive = false;
            category.EditionMode = false;
            return category;
        }


        public static void Copia(Category original, out Category copy)
        {

            Category category = New();
            category.Code = null;
            category.Description = original.Description;
            
            category.Inactive = original.Inactive;
            category.EditionMode = false;  //redundante, porque Novo() já trazia esta configuração
            copy = category;
        }


        public static string Describe(Category category)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'; [{11}]='{12}'; [{13}]='{14}'; [{15}]='{16}'";
            completeDescription = string.Format(completeDescription, "Categoria", 
                                                                     GlobalVariables.Resource.GetString("CodeString", GlobalVariables.Culture), category.Code,
                                                                     GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), category.Description,
                                                                     GlobalVariables.Resource.GetString("UnityWeightAverageString", GlobalVariables.Culture), category.UnityWeightAverage.ToString(),
                                                                     GlobalVariables.Resource.GetString("MinPriceAllowedString", GlobalVariables.Culture), category.MinPriceAllowed.ToString(),
                                                                     GlobalVariables.Resource.GetString("MaxPriceAllowedString", GlobalVariables.Culture), category.MaxPriceAllowed.ToString(),
                                                                     GlobalVariables.Resource.GetString("MaxPriceAmplitudeString", GlobalVariables.Culture), category.MaxPriceAmplitude.ToString(),
                                                                     GlobalVariables.Resource.GetString("TrustString", GlobalVariables.Culture), category.Trust.ToString(),
                                                                     GlobalVariables.Resource.GetString("InactiveString", GlobalVariables.Culture), category.Inactive.ToString(),
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), category.Version.ToString(), 
                                                                     GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), category.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(Category category, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            CodeValidation(category.Code, ref msg);
            DescriptionValidation(category.Description, ref msg);
            UnityWeightAverageValidation(category.UnityWeightAverage, ref msg);
            MinPriceAllowedValidation(category.MinPriceAllowed, ref msg);
            MaxPriceAllowedValidation(category.MaxPriceAllowed, ref msg);
            MaxPriceAmplitudeValidation(category.MaxPriceAmplitude, ref msg);
            TrustValidation(category.Trust, ref msg);
            
            EditionModeEdition(category.EditionMode, validationPurpose, ref msg);

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

        }


        public static void DescriptionValidation(string description, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), description, Description_Necesssary, true, Description_MinSize, Description_MaxSize, ref info);
        }

        public static void UnityWeightAverageValidation(double unityWeightAverage, ref string info)
        {

            SystemValidation.Double(GlobalVariables.Resource.GetString("UnityWeightAverageString", GlobalVariables.Culture), unityWeightAverage, UnityWeightAverage_Necesssary, UnityWeightAverage_MinSize, UnityWeightAverage_MaxSize, UnityWeightAverage_MaxNumberDigits, ref info);
        }

        public static void MinPriceAllowedValidation(decimal minPriceAllowed, ref string info)
        {

            SystemValidation.Decimal(GlobalVariables.Resource.GetString("MinPriceAllowedString", GlobalVariables.Culture), minPriceAllowed, MinPriceAllowed_Necesssary, MinPriceAllowed_MinSize, MinPriceAllowed_MaxSize, MinPriceAllowed_MaxNumberDigits, ref info);
        }

        public static void MaxPriceAllowedValidation(decimal maxPriceAllowed, ref string info)
        {

            SystemValidation.Decimal(GlobalVariables.Resource.GetString("MaxPriceAllowedString", GlobalVariables.Culture), maxPriceAllowed, MaxPriceAllowed_Necesssary, MaxPriceAllowed_MinSize, MaxPriceAllowed_MaxSize, MaxPriceAllowed_MaxNumberDigits, ref info);
        }

        public static void MaxPriceAmplitudeValidation(double maxPriceAmplitude, ref string info)
        {

            SystemValidation.Double(GlobalVariables.Resource.GetString("MaxPriceAmplitudeString", GlobalVariables.Culture), maxPriceAmplitude, MaxPriceAmplitude_Necesssary, MaxPriceAmplitude_MinSize, MaxPriceAmplitude_MaxSize, MaxPriceAmplitude_MaxNumberDigits, ref info);
        }

        public static void TrustValidation(double trust, ref string info)
        {

            SystemValidation.Double(GlobalVariables.Resource.GetString("TrustString", GlobalVariables.Culture), trust, Trust_Necesssary, Trust_MinSize, Trust_MaxSize, Trust_MaxNumberDigits, ref info);
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
                info += "[Category.EditionMode]" + msg;
            }
        }
    }
    
}
