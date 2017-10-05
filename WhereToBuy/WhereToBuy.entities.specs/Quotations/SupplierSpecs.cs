using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class SupplierSpecs
    {
        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "SupplierSpecs";


        public const bool Code_Necesssary = true;
        public const int Code_MinSize = 5;
        public const int Code_MaxSize = 5;


        public const bool Name_Necesssary = true;
        public const int Name_MinSize = 1;
        public const int Name_MaxSize = 50;

        public const bool Address_Necesssary = true;
        public const int Address_MinSize = 1;
        public const int Address_MaxSize = 200;

        public const bool ZipCode_Necesssary = true;
        public const int ZipCode_MinSize = 1;
        public const int ZipCode_MaxSize = 15;

        public const bool City_Necesssary = true;
        public const int City_MinSize = 1;
        public const int City_MaxSize = 50;

        public const bool IdentificationNumber_Necesssary = true;
        public const int IdentificationNumber_MinSize = 4;
        public const int IdentificationNumber_MaxSize = 50;

        public const bool Salesman_Necesssary = false;
        public const int Salesman_MinSize = 0;
        public const int Salesman_MaxSize = 50;

        public const bool Phone_Necesssary = false;
        public const int Phone_MinSize = 1;
        public const int Phone_MaxSize = 15;

        public const bool CellPhone_Necesssary = true;
        public const int CellPhone_MinSize = 1;
        public const int CellPhone_MaxSize = 15;

        public const bool Sms_Necesssary = true;
        public const int Sms_MinSize = 1;
        public const int Sms_MaxSize = 15;

        public const bool Email_Necesssary = true;
        public const int Email_MinSize = 1;
        public const int Email_MaxSize = 100;

        public const bool Username_Necesssary = true;
        public const int Username_MinSize = 4;
        public const int Username_MaxSize = 20;

        public const bool Password_Necesssary = true;
        public const int Password_MinSize = 6;
        public const int Password_MaxSize = 20;

        public const bool SuggestionExpirationHours_Necesssary = true;
        public const short SuggestionExpirationHours_MinSize = 0;
        public const short SuggestionExpirationHours_MaxSize = short.MaxValue;

        public const bool InitialScoreDescription_Necesssary = true;
        public const short InitialScoreDescription_MinSize = 0;
        public const short InitialScoreDescription_MaxSize = short.MaxValue;

        public const bool InicialScoreFeatures_Necesssary = true;
        public const short InicialScoreFeatures_MinSize = 0;
        public const short InicialScoreFeatures_MaxSize = short.MaxValue;

        public const bool InicialScoreLink_Necesssary = true;
        public const short InicialScoreLink_MinSize = 0;
        public const short InicialScoreLink_MaxSize = short.MaxValue;

        public const bool InicialScoreImage_Necesssary = true;
        public const short InicialScoreImage_MinSize = 0;
        public const short InicialScoreImage_MaxSize = short.MaxValue;

        public const bool ProductPriceTrust_Necesssary = true;
        public const double ProductPriceTrust_MinSize = 0;
        public const double ProductPriceTrust_MaxSize = 100;

        public const bool ProductAvailableTrust_Necesssary = true;
        public const double ProductAvailableTrust_MinSize = 0;
        public const double ProductAvailableTrust_MaxSize = 100;
       

        public static Supplier New()
        {
            Supplier supplier = new Supplier();
            supplier.Inactive = false;
            supplier.EditionMode = false;
            return supplier;
        }




        public static void Copia(Supplier original, out Supplier copy)
        {

            Supplier supplier = New();
            supplier.Code = null;
            supplier.Name = original.Name;
            supplier.Code = original.Code;
            supplier.Name = original.Name;
            supplier.Address = original.Address;
            supplier.ZipCode = original.ZipCode;
            supplier.City = original.City;
            supplier.IdentificationNumber = original.IdentificationNumber;
            supplier.Salesman = original.Salesman;
            supplier.Phone = original.Phone;
            supplier.Cellphone = original.Cellphone;
            supplier.SMS = original.SMS;
            supplier.Email = original.Email;
            supplier.ActiveOnlineAccess = original.ActiveOnlineAccess;
            supplier.Username = original.Username;
            supplier.Password = original.Password;
            supplier.SuggestionExpirationHours = original.SuggestionExpirationHours;
            supplier.AutomaticProductMatching = original.AutomaticProductMatching;
            supplier.ActomaticProductCreation = original.ActomaticProductCreation;
            supplier.InfoProductDetailAvailable = original.InfoProductDetailAvailable;
            supplier.InicialScoreDescription = original.InicialScoreDescription;
            supplier.InicialScoreFeatures = original.InicialScoreFeatures;
            supplier.InicialScoreLink = original.InicialScoreLink;
            supplier.InicialScoreImage = original.InicialScoreImage;
            supplier.InactiveDescriptionSuggestion = original.InactiveDescriptionSuggestion;
            supplier.InactiveFeatureSuggestion = original.InactiveFeatureSuggestion;
            supplier.InactiveLinkSuggestion = original.InactiveLinkSuggestion;
            supplier.InactiveImageSuggestion = original.InactiveImageSuggestion;
            supplier.InactiveAutomaticUpdateSuggestion = original.InactiveAutomaticUpdateSuggestion;
            supplier.ProductPriceTrust = original.ProductPriceTrust;
            supplier.ProductAvailableTrust = original.ProductAvailableTrust;
            supplier.Inactive = original.Inactive;
            supplier.EditionMode = false;  //redundante, porque Novo() já trazia esta configuração
            copy = supplier;
        }


        public static string Describe(Supplier supplier)
        {
            string completeDescription;

            completeDescription = "(({0}))[{1}] = '{2}'; [{3}] = '{4}'; [{5}] = '{6}'; [{7}] = '{8}'; [{9}] = '{10}'; [{11}] = '{12}'; [{13}] = '{14}'; [{15}] = '{16}'; "+
                                                                    "[{17}] = '{18}'; [{19}] = '{20}'; [{21}] = '{22}'; [{23}] = '{24}'; [{25}] = '{26}'; [{27}] = '{28}'; "+
                                                                    "[{29}] = '{30}'; [{31}] = '{32}'";

            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("SupplierString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("CodeString", GlobalVariables.Culture), supplier.Code, 
                                                                     GlobalVariables.Resource.GetString("NameString", GlobalVariables.Culture), supplier.Name,
                                                                     GlobalVariables.Resource.GetString("AddressString", GlobalVariables.Culture), supplier.Address,
                                                                     GlobalVariables.Resource.GetString("ZipCodeString", GlobalVariables.Culture), supplier.ZipCode,
                                                                     GlobalVariables.Resource.GetString("CityString", GlobalVariables.Culture), supplier.City,
                                                                     GlobalVariables.Resource.GetString("IdentificationNumberString", GlobalVariables.Culture), supplier.IdentificationNumber,
                                                                     GlobalVariables.Resource.GetString("SalesmanString", GlobalVariables.Culture), supplier.Salesman,
                                                                     GlobalVariables.Resource.GetString("PhoneString", GlobalVariables.Culture), supplier.Phone,
                                                                     GlobalVariables.Resource.GetString("CellphoneString", GlobalVariables.Culture), supplier.Cellphone,
                                                                     GlobalVariables.Resource.GetString("SMSString", GlobalVariables.Culture), supplier.SMS,
                                                                     GlobalVariables.Resource.GetString("EmailString", GlobalVariables.Culture), supplier.Email,
                                                                     GlobalVariables.Resource.GetString("ActiveOnlineAccessString", GlobalVariables.Culture), supplier.ActiveOnlineAccess,
                                                                     GlobalVariables.Resource.GetString("UserNameString", GlobalVariables.Culture), supplier.Username,
                                                                     GlobalVariables.Resource.GetString("PasswordString", GlobalVariables.Culture), supplier.Password,
                                                                     GlobalVariables.Resource.GetString("SuggestionExpireHoursString", GlobalVariables.Culture), supplier.SuggestionExpirationHours,
                                                                     GlobalVariables.Resource.GetString("AutomaticProductMatchingString", GlobalVariables.Culture), supplier.AutomaticProductMatching,
                                                                     GlobalVariables.Resource.GetString("ActomaticProductCreationString", GlobalVariables.Culture), supplier.ActomaticProductCreation,
                                                                     GlobalVariables.Resource.GetString("InfoProductDetailAvailableString", GlobalVariables.Culture), supplier.InfoProductDetailAvailable,
                                                                     GlobalVariables.Resource.GetString("InitialScoreDescriptionString", GlobalVariables.Culture), supplier.InicialScoreDescription,
                                                                     GlobalVariables.Resource.GetString("InicialScoreFeaturesString", GlobalVariables.Culture), supplier.InicialScoreFeatures,
                                                                     GlobalVariables.Resource.GetString("InicialScoreLinkString", GlobalVariables.Culture), supplier.InicialScoreLink,
                                                                     GlobalVariables.Resource.GetString("InicialScoreImageeString", GlobalVariables.Culture), supplier.InicialScoreImage,
                                                                     GlobalVariables.Resource.GetString("InactiveDescriptionSuggestionString", GlobalVariables.Culture), supplier.InactiveDescriptionSuggestion,
                                                                     GlobalVariables.Resource.GetString("InactiveFeatureSuggestionString", GlobalVariables.Culture), supplier.InactiveFeatureSuggestion,
                                                                     GlobalVariables.Resource.GetString("InactiveLinkSuggestionString", GlobalVariables.Culture), supplier.InactiveLinkSuggestion,
                                                                     GlobalVariables.Resource.GetString("InactiveImageSugestionString", GlobalVariables.Culture), supplier.InactiveImageSuggestion,
                                                                     GlobalVariables.Resource.GetString("InactiveAutomaticUpdateSuggestionString", GlobalVariables.Culture), supplier.InactiveAutomaticUpdateSuggestion,
                                                                     GlobalVariables.Resource.GetString("ProductPriceTrustString", GlobalVariables.Culture), supplier.ProductPriceTrust,
                                                                     GlobalVariables.Resource.GetString("ProductAvailableTrustString", GlobalVariables.Culture), supplier.ProductAvailableTrust,
                                                                     GlobalVariables.Resource.GetString("InactiveString", GlobalVariables.Culture), supplier.Inactive.ToString(),
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), supplier.Version.ToString(), 
                                                                     GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), supplier.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(Supplier supplier, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            CodeValidation(supplier.Code, ref msg);
            NameValidation(supplier.Name, ref msg);
            AddressValidation(supplier.Address, ref msg);
            ZipCodeValidation(supplier.ZipCode, ref msg);
            CityValidation(supplier.City, ref msg);
            IdentificationNumberValidation(supplier.IdentificationNumber, ref msg);
            PhoneValidation(supplier.Phone, ref msg);
            CellPhoneValidation(supplier.Cellphone, ref msg);
            SmsValidation(supplier.SMS, ref msg);
            EmailValidation(supplier.Email, ref msg);
            UsernameValidation(supplier.Username, ref msg);
            PasswordValidation(supplier.Password, ref msg);
            SuggestionExpirationHoursValidation(supplier.SuggestionExpirationHours, ref msg);
            InitialScoreDescriptionValidation(supplier.InicialScoreDescription, ref msg);
            InicialScoreFeaturesValidation(supplier.InicialScoreFeatures, ref msg);
            InicialScoreLinkValidation(supplier.InicialScoreLink, ref msg);
            InicialScoreImageValidation(supplier.InicialScoreImage, ref msg);
            ProductPriceTrustValidation(supplier.ProductPriceTrust, ref msg);
            ProductAvailableTrustValidation(supplier.ProductAvailableTrust, ref msg); 
            EditionModeEdition(supplier.EditionMode, validationPurpose, ref msg);

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


       
        public static void NameValidation(string name, ref string info)
        {
            SystemValidation.Text(GlobalVariables.Resource.GetString("NameString", GlobalVariables.Culture), name, Name_Necesssary, true, Name_MinSize, Name_MaxSize, ref info);
        }

        public static void AddressValidation(string address, ref string info)
        {
            SystemValidation.Text(GlobalVariables.Resource.GetString("AddressString", GlobalVariables.Culture), address, Address_Necesssary, true, Address_MinSize, Address_MaxSize, ref info);
        }

       
        public static void ZipCodeValidation(string zipCode, ref string info)
        {
            SystemValidation.Text(GlobalVariables.Resource.GetString("ZipCodeString", GlobalVariables.Culture), zipCode, ZipCode_Necesssary, true, ZipCode_MinSize, ZipCode_MaxSize, ref info);
        }

        public static void CityValidation(string city, ref string info)
        {
            SystemValidation.Text(GlobalVariables.Resource.GetString("CityString", GlobalVariables.Culture), city, City_Necesssary, true, City_MinSize, City_MaxSize, ref info);
        }

        public static void IdentificationNumberValidation(string identificationNumber, ref string info)
        {
            SystemValidation.Text(GlobalVariables.Resource.GetString("IdentificationNumberString", GlobalVariables.Culture), identificationNumber, IdentificationNumber_Necesssary, true, IdentificationNumber_MinSize, IdentificationNumber_MaxSize, ref info);
        }

        public static void SalesmanValidation(string salesman, ref string info)
        {
            SystemValidation.Text(GlobalVariables.Resource.GetString("SalesmanString", GlobalVariables.Culture), salesman, Salesman_Necesssary, true, Salesman_MinSize, Salesman_MaxSize, ref info);
        }

        public static void UsernameValidation(string username, ref string info)
        {
            SystemValidation.Text(GlobalVariables.Resource.GetString("UsernameString", GlobalVariables.Culture), username, Username_Necesssary, true, Username_MinSize, Username_MaxSize, ref info);

            string msg = "";

            if (username.Contains(" "))
            {
                msg += string.Format("#{0}$ {1}!", GlobalVariables.Resource.GetString("SpaceExistenceString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("CannotHaveSpaces", GlobalVariables.Culture).ToLower()); 
            }

            if (msg.Length > 0)
            {
                info += "[User.Username]" + msg;
            }

        }


        public static void PasswordValidation(string password, ref string info)
        {
            SystemValidation.Text(GlobalVariables.Resource.GetString("PasswordString", GlobalVariables.Culture), password, Password_Necesssary, true, Password_MinSize, Password_MaxSize, ref info);

            string msg = "";

            //if (password.Contains(" "))
            //{
            //    msg += string.Format("#{0}$ {1}!", GlobalVariables.Resource.GetString("SpaceExistenceString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("CannotHaveSpacesString", GlobalVariables.Culture).ToLower());
            //}

            //if (password.IndexOfAny(new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')' }) < 0)
            //{
            //    msg += string.Format("#{0}$ {1}!", GlobalVariables.Resource.GetString("SpecialCharString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("MustHaveSpecialCharString", GlobalVariables.Culture).ToLower());
            //}

            //if (password.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }) < 0)
            //{
            //    msg += string.Format("#{0}$ {1}!", GlobalVariables.Resource.GetString("NumericCharString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("MustHaveNumericCharString", GlobalVariables.Culture).ToLower());
            //}

            if (msg.Length > 0)
            {
                info += "[User.Password]" + msg;
            }

        }

        public static void PhoneValidation(string phone, ref string info)
        {
            SystemValidation.Text(GlobalVariables.Resource.GetString("CellPhoneString", GlobalVariables.Culture), phone, Phone_Necesssary, new Char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }, Phone_MinSize, Phone_MaxSize, ref info);
        }
       
        
        public static void CellPhoneValidation(string cellPhone, ref string info)
        {
            SystemValidation.Text("CellPhone", cellPhone, CellPhone_Necesssary, new Char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }, CellPhone_MinSize, CellPhone_MaxSize, ref info);
        }

        public static void SmsValidation(string sms, ref string info)
        {
            SystemValidation.Text("Sms", sms, Sms_Necesssary, new Char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }, Sms_MinSize, Sms_MaxSize, ref info);
        }

        public static void EmailValidation(string email, ref string info)
        {
            SystemValidation.Email("Email", email, Email_MaxSize, Email_Necesssary, ref info);
        }

        public static void SuggestionExpirationHoursValidation(short suggestionExpirationHours, ref string info)
        {

            SystemValidation.Int16(GlobalVariables.Resource.GetString(GlobalVariables.Resource.GetString("SuggestionExpireHoursString", GlobalVariables.Culture), GlobalVariables.Culture), suggestionExpirationHours, SuggestionExpirationHours_Necesssary, SuggestionExpirationHours_MinSize, SuggestionExpirationHours_MaxSize, ref info);
        }

        public static void InitialScoreDescriptionValidation(short initialScoreDescription, ref string info)
        {

            SystemValidation.Int16(GlobalVariables.Resource.GetString(GlobalVariables.Resource.GetString("InitialScoreDescriptionString", GlobalVariables.Culture), GlobalVariables.Culture), initialScoreDescription, InitialScoreDescription_Necesssary, InitialScoreDescription_MinSize, InitialScoreDescription_MaxSize, ref info);
        }

        public static void InicialScoreFeaturesValidation(short inicialScoreFeatures, ref string info)
        {

            SystemValidation.Int16(GlobalVariables.Resource.GetString(GlobalVariables.Resource.GetString("InicialScoreFeaturesString", GlobalVariables.Culture), GlobalVariables.Culture), inicialScoreFeatures, InicialScoreFeatures_Necesssary, InicialScoreFeatures_MinSize, InicialScoreFeatures_MaxSize, ref info);
        }

        public static void InicialScoreLinkValidation(short inicialScoreLink, ref string info)
        {

            SystemValidation.Int16(GlobalVariables.Resource.GetString(GlobalVariables.Resource.GetString("InicialScoreLinkString", GlobalVariables.Culture), GlobalVariables.Culture), inicialScoreLink, InicialScoreLink_Necesssary, InicialScoreLink_MinSize, InicialScoreLink_MaxSize, ref info);
        }

        public static void InicialScoreImageValidation(short inicialScoreImage, ref string info)
        {

            SystemValidation.Int16(GlobalVariables.Resource.GetString(GlobalVariables.Resource.GetString("InicialScoreImageString", GlobalVariables.Culture), GlobalVariables.Culture), inicialScoreImage, InicialScoreImage_Necesssary, InicialScoreImage_MinSize, InicialScoreImage_MaxSize, ref info);
        }

        public static void ProductPriceTrustValidation(double productPriceTrust, ref string info)
        {

            SystemValidation.Double(GlobalVariables.Resource.GetString(GlobalVariables.Resource.GetString("ProductPriceTrustString", GlobalVariables.Culture), GlobalVariables.Culture), productPriceTrust, ProductPriceTrust_Necesssary, ProductPriceTrust_MinSize, ProductPriceTrust_MaxSize, ref info);
        }

        public static void ProductAvailableTrustValidation(double productAvailableTrust, ref string info)
        {

            SystemValidation.Double(GlobalVariables.Resource.GetString(GlobalVariables.Resource.GetString("ProductAvailableTrustString", GlobalVariables.Culture), GlobalVariables.Culture), productAvailableTrust, ProductAvailableTrust_Necesssary, ProductAvailableTrust_MinSize, ProductAvailableTrust_MaxSize, ref info);
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
