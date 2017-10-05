using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class GroupSpecs
    {
        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "GroupSpecs";


       

        public const bool Code_Necesssary = true;
        public const int Code_MinSize = 1;
        public const int Code_MaxSize = 5;


        public const bool Description_Necesssary = true;
        public const int Description_MinSize = 4;
        public const int Description_MaxSize = 50;



        public static Group New()
        {
            Group group = new Group();
            group.Inactive = false;
            group.EditionMode = false;
            return group;
        }


        public static void Copia(Group original, out Group copy)
        {

            Group group = New();
            group.Code = null;
            group.Description = original.Description;
            group.Inactive = original.Inactive;
            group.EditionMode = false;  //redundante, porque Novo() já trazia esta configuração
            copy = group;
        }


        public static string Describe(Group group)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'";
            completeDescription = string.Format(completeDescription, "Group", GlobalVariables.Resource.GetString("CodeString", GlobalVariables.Culture), group.Code,
                                                                     GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), group.Description, GlobalVariables.Resource.GetString("InactiveString", GlobalVariables.Culture), group.Inactive.ToString(),
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), group.Version.ToString(), GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), group.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(Group group, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            CodeValidation(group.Code, ref msg);
            DescriptionValidation(group.Description, ref msg);
            EditionModeEdition(group.EditionMode, validationPurpose, ref msg);

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
                info += "[Group.Code]" + msg;
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
                info += "[Group.EditionMode]" + msg;
            }
        } 
    }
}
