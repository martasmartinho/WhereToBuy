using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class StockSpecs
    {
        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "StockSpecs";



        public const bool Code_Necesssary = true;
        public const int Code_MinSize = 1;
        public const int Code_MaxSize = 5;


        public const bool Description_Necesssary = true;
        public const int Description_MinSize = 4;
        public const int Description_MaxSize = 50;

        public const bool AvailabilityLevel_Necesssary = true;
        public const short AvailabilityLevel_MinSize = -5;
        public const short AvailabilityLevel_MaxSize = 5;

        public const bool Notes_Necesssary = false;
        public const int Notes_MinSize = 0;
        public const int Notes_MaxSize = 256;



        public static Stock New()
        {
            Stock brand = new Stock();
            brand.Inactive = false;
            brand.EditionMode = false;
            return brand;
        }


        public static void Copia(Stock original, out Stock copy)
        {

            Stock stock = New();
            stock.Code = null;
            stock.Description = original.Description;
            stock.AvailabilityLevel = original.AvailabilityLevel;
            stock.StockCodeExpirationP50 = original.StockCodeExpirationP50;
            stock.StockCodeExpirationP60 = original.StockCodeExpirationP60;
            stock.StockCodeExpirationP70 = original.StockCodeExpirationP70;
            stock.StockCodeExpirationP80 = original.StockCodeExpirationP80;
            stock.StockCodeExpirationP90 = original.StockCodeExpirationP90;
            stock.Notes = original.Notes;

            stock.Inactive = original.Inactive;
            stock.EditionMode = false;  //redundante, porque Novo() já trazia esta configuração
            copy = stock;
        }

        public static string Describe(Stock stock)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}' ; [{9}]='{10}'; [{11}]='{12}'; [{13}]='{14}'; [{15}]='{16}'; [{17}]='{18}'; [{19}]='{20}'; [{21}]='{22}'; [{23}]='{24}'";
            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("StockString", GlobalVariables.Culture), 
                                                                     GlobalVariables.Resource.GetString("CodeString", GlobalVariables.Culture), stock.Code,
                                                                     GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), stock.Description,
                                                                     GlobalVariables.Resource.GetString("AvailabilityLevelString", GlobalVariables.Culture), stock.AvailabilityLevel.ToString(),
                                                                     GlobalVariables.Resource.GetString("StockCodeExpirationP50String", GlobalVariables.Culture), stock.StockCodeExpirationP50 != null ? stock.StockCodeExpirationP50.Code : string.Empty,
                                                                     GlobalVariables.Resource.GetString("StockCodeExpirationP60String", GlobalVariables.Culture), stock.StockCodeExpirationP60 != null ? stock.StockCodeExpirationP60.Code : string.Empty,
                                                                     GlobalVariables.Resource.GetString("StockCodeExpirationP70String", GlobalVariables.Culture), stock.StockCodeExpirationP70 != null ? stock.StockCodeExpirationP70.Code : string.Empty,
                                                                     GlobalVariables.Resource.GetString("StockCodeExpirationP80String", GlobalVariables.Culture), stock.StockCodeExpirationP80 != null ? stock.StockCodeExpirationP80.Code : string.Empty, 
                                                                     GlobalVariables.Resource.GetString("StockCodeExpirationP90String", GlobalVariables.Culture), stock.StockCodeExpirationP90 != null ? stock.StockCodeExpirationP90.Code : string.Empty,
                                                                     GlobalVariables.Resource.GetString("NotesString", GlobalVariables.Culture), stock.Notes != null ? stock.Notes : string.Empty,
                                                                     GlobalVariables.Resource.GetString("InactiveString", GlobalVariables.Culture), stock.Inactive.ToString(),
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), stock.Version.ToString(), 
                                                                     GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), stock.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(Stock stock, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            CodeValidation(stock.Code, ref msg);
            DescriptionValidation(stock.Description, ref msg);
            AvailabilityLevelValidation(stock.AvailabilityLevel, ref msg);
            NotesValidation(stock.Notes, ref msg);
            EditionModeEdition(stock.EditionMode, validationPurpose, ref msg);

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


        public static void AvailabilityLevelValidation(short availabilityLevel, ref string info) 
        {
            SystemValidation.Int16(GlobalVariables.Resource.GetString("AvailabilityLevelString", GlobalVariables.Culture), availabilityLevel, AvailabilityLevel_Necesssary, AvailabilityLevel_MinSize, AvailabilityLevel_MaxSize, ref info);
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
                info += "[Stock.EditionMode]" + msg;
            }
        }
    }
}
