using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public class SupplierBrandSpecs
    {
        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "SupplierBrandSpecs";


        public const bool Trust_Necesssary = false;
        public const double Trust_MinSize = 0;
        public const double Trust_MaxSize = 100;
        public const int Trust_MaxNumberDigits = 4;


        public const bool Notes_Necesssary = false;
        public const int Notes_MinSize = 0;
        public const int Notes_MaxSize = 256;



        public static SupplierBrand New()
        {
            SupplierBrand supplierBrand = new SupplierBrand();
            supplierBrand.EditionMode = false;
            return supplierBrand;
        }


        public static void Copia(SupplierBrand original, out SupplierBrand copy)
        {

            SupplierBrand supplierBrand = New();
            supplierBrand.Supplier = null;
            supplierBrand.Brand = null;
            supplierBrand.Trust = original.Trust;
            supplierBrand.Notes = original.Notes;
            supplierBrand.EditionMode = false;
            copy = supplierBrand;
        }


        public static string Describe(SupplierBrand supplierBrand)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'";
            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("SupplierBrandTrustString", GlobalVariables.Culture),
                                                                     GlobalVariables.Resource.GetString("SupplierCodeString", GlobalVariables.Culture), supplierBrand.Supplier.Code,
                                                                     GlobalVariables.Resource.GetString("BrandCodeString", GlobalVariables.Culture), supplierBrand.Brand.Code,
                                                                     GlobalVariables.Resource.GetString("TrustString", GlobalVariables.Culture), supplierBrand.Trust.ToString(),
                                                                     GlobalVariables.Resource.GetString("NotesString", GlobalVariables.Culture), supplierBrand.Notes,
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), supplierBrand.Version.ToString());
                                                                   

            return completeDescription;
        }


        public static bool Validation(SupplierBrand supplierBrand, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            TrustValidation(supplierBrand.Trust, ref msg);
            NotesValidation(supplierBrand.Notes, ref msg);
            EditionModeEdition(supplierBrand.EditionMode, validationPurpose, ref msg);

            if (msg.Trim().Length > 0)
            {
                info += msg;
                return false;
            }

            return true;
        }



        public static void NotesValidation(string notes, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), notes, Notes_Necesssary, true, Notes_MinSize, Notes_MaxSize, ref info);
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
                info += "[SupplierBrand.EditionMode]" + msg;
            }
        }
    }
}
