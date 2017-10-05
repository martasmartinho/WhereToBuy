using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public static class WarningTypeSpecs
    {
        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "WarningTypesSpecs";



        public const bool Code_Necesssary = true;
        public const int Code_MinSize = 1;
        public const int Code_MaxSize = 5;


        public const bool Description_necesssary = true;
        public const int Description_MinSize = 4;
        public const int Description_MaxSize = 128;

        public const bool Severity_Necesssary = true;
        public const short Severity_MinSize = 0;
        public const short Severity_MaxSize = 9;

        public const bool Notes_Necesssary = false;
        public const int Notes_MinSize = 0;
        public const int Notes_MaxSize = 256;

        public const bool Icon_Necesssary = false;
        public const int Icon_MinSize = 0;
        public const int Icon_MaxSize = 20;



        public static WarningType New()
        {
            WarningType warningType = new WarningType();
            warningType.Inactive = false;
            warningType.EditionMode = false;
            return warningType;
        }


        public static void Copia(WarningType original, out WarningType copy)
        {

            WarningType warningType = New();
            warningType.Code = null;
            warningType.Description = original.Description;
            warningType.Severity = original.Severity;
            warningType.Notes = original.Notes;
            warningType.Icon = original.Icon;
            warningType.Inactive = original.Inactive;
            warningType.EditionMode = false;  //redundante, porque Novo() já trazia esta configuração
            copy = warningType;
        }


        public static string Describe(WarningType warningType)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'; [{11}]='{12}'; [{13}]='{14}'; [{15}]='{16}'";
            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("WarningTypeString", GlobalVariables.Culture), 
                                                                     GlobalVariables.Resource.GetString("CodeString", GlobalVariables.Culture), warningType.Code,
                                                                     GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), warningType.Description, 
                                                                     GlobalVariables.Resource.GetString("SeverityString", GlobalVariables.Culture), warningType.Severity.ToString(),
                                                                     GlobalVariables.Resource.GetString("NotesString", GlobalVariables.Culture), warningType.Notes,
                                                                     GlobalVariables.Resource.GetString("IconString", GlobalVariables.Culture), warningType.Icon,
                                                                     GlobalVariables.Resource.GetString("InactiveString", GlobalVariables.Culture), warningType.Inactive.ToString(),
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), warningType.Version.ToString(), 
                                                                     GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), warningType.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(WarningType warningType, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            CodeValidation(warningType.Code, ref msg);
            DescriptionValidation(warningType.Description, ref msg);
            SeverityValidation(warningType.Severity, ref msg);
            NotesValidation(warningType.Notes, ref msg);
            IconValidation(warningType.Icon, ref msg);
            EditionModeEdition(warningType.EditionMode, validationPurpose, ref msg);

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

            SystemValidation.Text(GlobalVariables.Resource.GetString("DescriptionString", GlobalVariables.Culture), description, Description_necesssary, true, Description_MinSize, Description_MaxSize, ref info);
        }

        public static void SeverityValidation(short severity, ref string info)
        {

            SystemValidation.Int16(GlobalVariables.Resource.GetString("SeverityString", GlobalVariables.Culture), severity, Severity_Necesssary, Severity_MinSize, Severity_MaxSize, ref info);
        }

        public static void NotesValidation(string notes, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("NotesString", GlobalVariables.Culture), notes, Notes_Necesssary, true, Notes_MinSize, Notes_MaxSize, ref info);
        }

        public static void IconValidation(string icon, ref string info)
        {

            SystemValidation.Text(GlobalVariables.Resource.GetString("IconString", GlobalVariables.Culture), icon, Icon_Necesssary, true, Icon_MinSize, Icon_MaxSize, ref info);
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
                info += "[WarningType.EditionMode]" + msg;
            }
        }
    }
}
