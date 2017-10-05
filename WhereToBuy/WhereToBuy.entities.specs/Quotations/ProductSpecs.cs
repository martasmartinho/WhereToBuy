using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class ProductSpecs
    {

        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "ProductSpecs";

        //string description;
        //string partnumber;
        //Category category;
        //Brand brand;
        //Tax tax;
        //Supplier supplier;
        //decimal costPrice;
        //DateTime costPrice_Date;
        //decimal costPrice_U1;
        //DateTime costPrice_U1Date;
        //decimal costPrice_U2;
        //DateTime costPrice_U2Date;
        //decimal costPrice_U3;
        //DateTime costPrice_U3Date;
        //Stock stock;
        //DateTime stock_Date;
        //Stock stock_U1;
        //DateTime stock_U1Date;
        //Stock stock_U2;
        //DateTime stock_U2Date;
        //Stock stock_U3;
        //DateTime stock_U3Date;
        //bool discontinued;

        public const bool Code_Necesssary = true;
        public const int Code_MinSize = 1;
        public const int Code_MaxSize = 40;

        public const bool Description_Necesssary = false;
        public const int Description_MinSize = 0;
        public const int Description_MaxSize = 256;

        public const bool Partnumber_Necesssary = true;
        public const int Partnumber_MinSize = 4;
        public const int Partnumber_MaxSize = 25;

        public static Product New()
        {
            Product product = new Product();
            product.Details = new List<ProductDetail>();
            product.EditionMode = false;
            return product;
        }


        public static void Copy(Product original, out Product copy)
        {

            Product product = New();
            product.Category = null;
            product.Brand = null;
            product.Tax = null;
            product.Supplier = null;
            product.Description = original.Description;
            product.Partnumber = original.Partnumber;
            product.Discontinued = original.Discontinued;
            product.EditionMode = false;
            product.Details = original.Details.ToList();
            copy = product;
        }


        public static string Describe(Product product)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'; [{11}]='{12}'; [{13}]='{14}'";
            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("ProductString", GlobalVariables.Culture),
                                                                     GlobalVariables.Resource.GetString("CodeString", GlobalVariables.Culture), product.Code,
                                                                     GlobalVariables.Resource.GetString("SupplierCodeString", GlobalVariables.Culture), product.Supplier.Code,
                                                                     GlobalVariables.Resource.GetString("BrandCodeString", GlobalVariables.Culture), product.Brand.Code,
                                                                     GlobalVariables.Resource.GetString("CategoryCodeString", GlobalVariables.Culture), product.Category.Code,
                                                                     GlobalVariables.Resource.GetString("CategoryCodeString", GlobalVariables.Culture), product.Tax.Code,
                                                                     GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), product.Description,
                                                                     GlobalVariables.Resource.GetString("PartnumberString", GlobalVariables.Culture), product.Partnumber);

            return completeDescription;
        }


        public static bool Validation(Product product, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            CodeValidation(product.Code, ref msg);
            DescriptionValidation(product.Description, ref msg);
            PartnumberValidation(product.Partnumber, ref msg);
            EditionModeEdition(product.EditionMode, validationPurpose, ref msg);

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


        public static void PartnumberValidation(string partnumber, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("PartnumberString", GlobalVariables.Culture), partnumber, Partnumber_Necesssary, true, Partnumber_MinSize, Partnumber_MaxSize, ref info);
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
                info += "Product.EditionMode]" + msg;
            }
        }
    }
}
