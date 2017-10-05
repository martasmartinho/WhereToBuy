using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class UserSpecs
    {
        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "UserSpecs";


        public const bool Username_Necesssary = true;
        public const int Username_MinSize = 1;
        public const int Username_MaxSize = 20;


        public const bool Password_Necesssary = true;
        public const int Password_MinSize = 6;
        public const int Password_MaxSize = 12;

        public const bool Name_Necesssary = true;
        public const int Name_MinSize = 1;
        public const int Name_MaxSize = 50;

        public const bool Email_Necesssary = true;
        public const int Email_MinSize = 1;
        public const int Email_MaxSize = 100;

        public const bool Mobile_Necesssary = true;
        public const int Mobile_MinSize = 1;
        public const int Mobile_MaxSize = 15;

        public const bool Sms_Necesssary = true;
        public const int Sms_MinSize = 1;
        public const int Sms_MaxSize = 15;

        public static User New()
        {
            User user = new User();
            user.Inactive = false;
            user.EditionMode = false;
            return user;
        }


        public static void Copia(User original, out User copy)
        {

            User user = New();
            user.Username = null;
            user.Password = original.Password;
            user.Name = original.Name;
            user.Email = original.Email;
            user.Mobile = original.Mobile;
            user.Sms = original.Sms;
            user.Administrator = original.Administrator;
            user.Language = original.Language;
            user.Inactive = original.Inactive;
            user.EditionMode = false;  //redundante, porque Novo() já trazia esta configuração
            copy = user;
        }


        public static string Describe(User user)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}', [{11}]='{12}'; [{13}]='{14}'";
            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("UserString", GlobalVariables.Culture), 
                                                                     GlobalVariables.Resource.GetString("UsernameString", GlobalVariables.Culture), user.Username,
                                                                     GlobalVariables.Resource.GetString("PasswordString", GlobalVariables.Culture), user.Password,
                                                                     GlobalVariables.Resource.GetString("NameString", GlobalVariables.Culture), user.Name,
                                                                     GlobalVariables.Resource.GetString("AdministratorString", GlobalVariables.Culture), user.Administrator.ToString(),
                                                                     GlobalVariables.Resource.GetString("LanguageString", GlobalVariables.Culture), user.Language.Code.ToString(),
                                                                     GlobalVariables.Resource.GetString("InactiveString", GlobalVariables.Culture), user.Inactive.ToString(),
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), user.Version.ToString(), 
                                                                     GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), user.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(User user, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            UsernameValidation(user.Username, ref msg);
            PasswordValidation(user.Password, ref msg);
            NameValidation(user.Name, ref msg);
            EmailValidation(user.Email, ref msg);
            MobileValidation(user.Mobile, ref msg);
            SmsValidation(user.Sms, ref msg);
            EditionModeEdition(user.EditionMode, validationPurpose, ref msg);

            if (msg.Trim().Length > 0)
            {
                info += msg;
                return false;
            }

            return true;
        }


        public static void UsernameValidation(string username, ref string info)
        {
            SystemValidation.Text(GlobalVariables.Resource.GetString("UsernameString", GlobalVariables.Culture), username, Username_Necesssary, true, Username_MinSize, Username_MaxSize, ref info);

            string msg = "";

            if (username.Contains(" "))
            {
                msg += string.Format("#{0}$ {1}!", GlobalVariables.Resource.GetString("SpaceExistence", GlobalVariables.Culture), GlobalVariables.Resource.GetString("CannotHaveSpaces", GlobalVariables.Culture).ToLower());
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

            if (password.Contains(" "))
            {
                msg += string.Format("#{0}$ {1}!", GlobalVariables.Resource.GetString("SpaceExistence", GlobalVariables.Culture), GlobalVariables.Resource.GetString("CannotHaveSpaces", GlobalVariables.Culture).ToLower());
            }

            if (password.IndexOfAny(new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')' }) < 0)
            {
                msg +=string.Format("#{0}$ {1}!", GlobalVariables.Resource.GetString("SpecialChar", GlobalVariables.Culture), GlobalVariables.Resource.GetString("MustHaveSpecialChar", GlobalVariables.Culture).ToLower());
            }

            if (password.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }) < 0)
            {
                msg += string.Format("#{0}$ {1}!", GlobalVariables.Resource.GetString("NumericChar", GlobalVariables.Culture), GlobalVariables.Resource.GetString("MustHaveNumericChar", GlobalVariables.Culture).ToLower());
            }

            if (msg.Length > 0)
            {
                info += "[User.Password]" + msg;
            }

        }
  

        public static void NameValidation(string name, ref string info)
        {
            SystemValidation.Text(GlobalVariables.Resource.GetString("NameString", GlobalVariables.Culture), name, Name_Necesssary, true, Name_MinSize, Name_MaxSize, ref info);

        }


        public static void EmailValidation(string email, ref string info)
        {
            SystemValidation.Email(GlobalVariables.Resource.GetString("EmailString", GlobalVariables.Culture), email, Email_MaxSize, Email_Necesssary, ref info);
        }


        public static void MobileValidation(string mobile, ref string info)
        {
            SystemValidation.Text(GlobalVariables.Resource.GetString("CellPhoneString", GlobalVariables.Culture), mobile, Mobile_Necesssary, new Char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }, Mobile_MinSize, Mobile_MaxSize, ref info);
        }


        public static void SmsValidation(string sms, ref string info)
        {
            SystemValidation.Text(GlobalVariables.Resource.GetString("SmsString", GlobalVariables.Culture), sms, Sms_Necesssary, new Char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }, Sms_MinSize, Sms_MaxSize, ref info);
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
                info += "[User.EditionMode]" + msg;
            }
        }
    }
}
