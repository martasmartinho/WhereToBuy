using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.entities.specs
{
    public class WorryingTermSpecs
    {
        static string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static string _className = "WorryingTermSpecs";



        public const bool Term_Necesssary = true;
        public const int Term_MinSize = 1;
        public const int Term_MaxSize = 50;

        public const bool Index_Necesssary = true;
        public const short Index_MinSize = 0;
        public const short Index_MaxSize = short.MaxValue;

        public const bool Notes_Necesssary = false;
        public const int Notes_MinSize = 0;
        public const int Notes_MaxSize = 256;


        public static WorryingTerm New()
        {
            WorryingTerm worryingTerm = new WorryingTerm();
            worryingTerm.Inactive = false;
            worryingTerm.EditionMode = false;
            return worryingTerm;
        }


        public static void Copia(WorryingTerm original, out WorryingTerm copy)
        {

            WorryingTerm worryingTerm = New();
            worryingTerm.Term = null;
            worryingTerm.Index = original.Index;
            worryingTerm.Notes = original.Notes;
            worryingTerm.Inactive = original.Inactive;
            worryingTerm.EditionMode = false;  //redundante, porque Novo() já trazia esta configuração
            copy = worryingTerm;
        }


        public static string Describe(WorryingTerm worryingTerm)
        {
            string completeDescription;

            completeDescription = "(({0})) [{1}]='{2}'; [{3}]='{4}'; [{5}]='{6}'; [{7}]='{8}'; [{9}]='{10}'; [{11}]='{12}'";
            completeDescription = string.Format(completeDescription, GlobalVariables.Resource.GetString("WorryingTermString", GlobalVariables.Culture),
                                                                     GlobalVariables.Resource.GetString("TermString", GlobalVariables.Culture), worryingTerm.Term,
                                                                     GlobalVariables.Resource.GetString("IndexString", GlobalVariables.Culture), worryingTerm.Index.ToString(),
                                                                     GlobalVariables.Resource.GetString("NotesString", GlobalVariables.Culture), worryingTerm.Notes,
                                                                     GlobalVariables.Resource.GetString("InactiveString", GlobalVariables.Culture), worryingTerm.Inactive.ToString(),
                                                                     GlobalVariables.Resource.GetString("VersionString", GlobalVariables.Culture), worryingTerm.Version.ToString(),
                                                                     GlobalVariables.Resource.GetString("CreationString", GlobalVariables.Culture), worryingTerm.Creation.ToString());

            return completeDescription;
        }


        public static bool Validation(WorryingTerm worryingTerm, ValidationPurpose validationPurpose, ref string info)
        {

            string msg = "";

            TermValidation(worryingTerm.Term, ref msg);
            IndexValidation(worryingTerm.Index, ref msg);
            NotesValidation(worryingTerm.Notes, ref msg);
            EditionModeEdition(worryingTerm.EditionMode, validationPurpose, ref msg);

            if (msg.Trim().Length > 0)
            {
                info += msg;
                return false;
            }

            return true;
        }


        public static void TermValidation(string term, ref string info)
        {
            SystemValidation.Text(GlobalVariables.Resource.GetString("TermString", GlobalVariables.Culture), term, Term_Necesssary, true, Term_MinSize, Term_MaxSize, ref info);

        }


        public static void IndexValidation(short index, ref string info)
        {

            SystemValidation.Int16(GlobalVariables.Resource.GetString("IndexString", GlobalVariables.Culture), index, Index_Necesssary, Index_MinSize, Index_MaxSize, ref info);
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
                info += "[WorryingTerm.EditionMode]" + msg;
            }
        }
    }
}
