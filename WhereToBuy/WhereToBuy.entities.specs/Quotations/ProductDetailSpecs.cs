using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class ProductDetailSpecs
    {

        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "ProductDetailSpecs";

        public const bool Description_Necesssary = true;
        public const int Description_MinSize = 1;
        public const int Description_MaxSize = 256;

        public const bool DescriptionScore_Necesssary = true;
        public const short DescriptionScore_MinSize = 0;
        public const short DescriptionScore_MaxSize = 9;

        public const bool Features_Necesssary = true;
        public const int Features_MinSize = 1;
        public const int Features_MaxSize = 2048;

        public const bool FeaturesScore_Necesssary = true;
        public const short FeaturesScore_MinSize = 0;
        public const short FeaturesScore_MaxSize = 9;

        public const bool Link_Necesssary = true;
        public const int Link_MinSize = 1;
        public const int Link_MaxSize = 1024;

        public const bool LinkScore_Necesssary = true;
        public const short LinkScore_MinSize = 0;
        public const short LinkScore_MaxSize = 9;

        public const bool Image_Necesssary = true;
        public const int Image_MinSize = 1;
        public const int Image_MaxSize = 1024;

        public const bool ImageScore_Necesssary = true;
        public const short ImageScore_MinSize = 0;
        public const short ImageScore_MaxSize = 9;


        public static ProductDetail New()
        {
            ProductDetail productDetail = new ProductDetail();
            productDetail.Inactive = false;
            productDetail.EditionMode = false;
            return productDetail;
        }


        public static void Copia(ProductDetail original, out ProductDetail copy)
        {

            ProductDetail productDetail = New();
            productDetail.ProductCode = string.Empty;
            productDetail.Supplier = null;
            productDetail.Description = original.Description;
            productDetail.DescriptionScore = original.DescriptionScore;
            productDetail.IsDescriptionDisable = original.IsDescriptionDisable;
            productDetail.Features = original.Features;
            productDetail.FeaturesScore = original.FeaturesScore;
            productDetail.IsFeaturesDisable = original.IsFeaturesDisable;
            productDetail.Link = original.Link;
            productDetail.LinkScore = original.LinkScore;
            productDetail.IsLinkDisable = original.IsLinkDisable;
            productDetail.Image = original.Image;
            productDetail.ImageScore = original.ImageScore;
            productDetail.IsImageDisable = original.IsImageDisable;
            productDetail.AutomaticUpdate = original.AutomaticUpdate;
            productDetail.ContentConcernIndex = original.ContentConcernIndex;
            productDetail.NeddManualUpdate = original.NeddManualUpdate;
            productDetail.Inactive = original.Inactive;
            productDetail.EditionMode = false;  //redundante, porque Novo() já trazia esta configuração
            copy = productDetail;
        }


        public static string Describe(ProductDetail productDetail)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'";
            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("ProductDetailString", GlobalVariables.Culture), 
                                                                     GlobalVariables.Resource.GetString("ProductString", GlobalVariables.Culture), productDetail.ProductCode,
                                                                     GlobalVariables.Resource.GetString("SupplierString", GlobalVariables.Culture), productDetail.Supplier.ToString(),
                                                                     GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), productDetail.Description,
                                                                     GlobalVariables.Resource.GetString("ScoreString", GlobalVariables.Culture), productDetail.DescriptionScore,
                                                                     GlobalVariables.Resource.GetString("FeaturesString", GlobalVariables.Culture), productDetail.ProductCode,
                                                                     GlobalVariables.Resource.GetString("ScoreString", GlobalVariables.Culture), productDetail.FeaturesScore,
                                                                     GlobalVariables.Resource.GetString("LinkString", GlobalVariables.Culture), productDetail.Link,
                                                                     GlobalVariables.Resource.GetString("ScoreString", GlobalVariables.Culture), productDetail.LinkScore,
                                                                     GlobalVariables.Resource.GetString("ImageString", GlobalVariables.Culture), productDetail.Image,
                                                                     GlobalVariables.Resource.GetString("ScoreString", GlobalVariables.Culture), productDetail.ImageScore,
                                                                     GlobalVariables.Resource.GetString("ScoreString", GlobalVariables.Culture), productDetail.ImageScore,
                                                                     GlobalVariables.Resource.GetString("ScoreString", GlobalVariables.Culture), productDetail.ImageScore,
                                                                     GlobalVariables.Resource.GetString("IPCString", GlobalVariables.Culture), productDetail.ContentConcernIndex,
                                                                     GlobalVariables.Resource.GetString("InactiveString", GlobalVariables.Culture), productDetail.Inactive.ToString(),
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), productDetail.Version.ToString(),
                                                                     GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), productDetail.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(ProductDetail productDetail, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            DescriptionValidation(productDetail.Description, ref msg);
            DescriptionScoreValidation(productDetail.DescriptionScore, ref msg);
            FeaturesValidation(productDetail.Features, ref msg);
            FeaturesScoreValidation(productDetail.FeaturesScore, ref msg);
            LinkValidation(productDetail.Link, ref msg);
            LinkScoreValidation(productDetail.LinkScore, ref msg);
            ImageValidation(productDetail.Image, ref msg);
            ImageScoreValidation(productDetail.ImageScore, ref msg);
            EditionModeEdition(productDetail.EditionMode, validationPurpose, ref msg);

            if (msg.Trim().Length > 0)
            {
                info += msg;
                return false;
            }

            return true;
        }


        public static void DescriptionValidation(string description, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), description, Description_Necesssary, true, Description_MinSize, Description_MaxSize, ref info);
        }

        public static void DescriptionScoreValidation(short score, ref string info)
        {

            SystemValidation.Int16(GlobalVariables.Resource.GetString("DescriptionScoreString", GlobalVariables.Culture), score, DescriptionScore_Necesssary, DescriptionScore_MinSize, DescriptionScore_MaxSize, ref info);
        }

        public static void FeaturesValidation(string features, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("FeaturesString", GlobalVariables.Culture), features, Features_Necesssary, true, Features_MinSize, Features_MaxSize, ref info);
        }

        public static void FeaturesScoreValidation(short score, ref string info)
        {

            SystemValidation.Int16(GlobalVariables.Resource.GetString("FeaturesScoreString", GlobalVariables.Culture), score, FeaturesScore_Necesssary, FeaturesScore_MinSize, FeaturesScore_MaxSize, ref info);
        }

        public static void LinkValidation(string link, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("LinkString", GlobalVariables.Culture), link, Link_Necesssary, true, Link_MinSize, Link_MaxSize, ref info);
        }

        public static void LinkScoreValidation(short score, ref string info)
        {

            SystemValidation.Int16(GlobalVariables.Resource.GetString("LinkScoreString", GlobalVariables.Culture), score, LinkScore_Necesssary, LinkScore_MinSize, LinkScore_MaxSize, ref info);
        }

        public static void ImageValidation(string image, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("ImageString", GlobalVariables.Culture), image, Image_Necesssary, true, Image_MinSize, Image_MaxSize, ref info);
        }

        public static void ImageScoreValidation(short score, ref string info)
        {

            SystemValidation.Int16(GlobalVariables.Resource.GetString("ImageScoreString", GlobalVariables.Culture), score, ImageScore_Necesssary, ImageScore_MinSize, ImageScore_MaxSize, ref info);
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
                info += "[ProductDetail.EditionMode]" + msg;
            }
        }


    }
}
