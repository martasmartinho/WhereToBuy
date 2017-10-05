using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WhereToBuy.utils
{
    public static class SQLStrings
    {
        static string[] dangerousWords = { "drop", "table", "database", "'", "<", ">", "truncate", @"\", "~/", "shutdown", "shell", "script" };


        #region metodos publicos DangerousWords




        /// <summary>
        /// Esta propriedade devolve o array das strings que são consideradas perigosas
        /// </summary>
        public static string[] DangerousWords
        {
            get
            {
                return dangerousWords;
            }
        }




        /// <summary>
        /// Este metodo valida a existência de texto perigoso numa string
        /// </summary>
        /// <param name="str">string a ser analisada</param>
        /// <returns>verdadeiro se encontrar algo perigoso</returns>
        public static bool IsDangerousText(string str)
        {
            string dummyText = "";
            return IsDangerousText(str, ref dummyText);
        }




        /// <summary>
        /// Valida a existencia de texto perigoso numa dada string
        /// </summary>
        /// <param name="str">string a ser analisada</param>
        /// <param name="info">informações acerca do perigo detectado</param>
        /// <returns>retorna verdadeiro se encontrar algo perigoso</returns>
        public static bool IsDangerousText(string str, ref string info)
        {
            bool dangerous = false;
            string s = str.ToLower().TrimEnd();


            if (s.Trim().Length == 0)
            {
                info += "sem texto para verificar;";//Traduzir
                dangerous = false;
            }

            if (s.Contains("drop"))
            {
                info += "texto contem DROP;";//Traduzir
                dangerous = true;
            }

            if (s.Contains("table"))
            {
                info += "texto contem TABLE;";//Traduzir
                dangerous = true;
            }

            if (s.Contains("database"))
            {
                info += "texto contem DATABASE;";//Traduzir
                dangerous = true;
            }

            if (s.Contains("'"))
            {
                info += "texto contem ' (pelica);";//Traduzir
                dangerous = true;
            }

            if (s.Contains("<"))
            {
                info += "texto contem <;";//Traduzir
                dangerous = true;
            }

            if (s.Contains(">"))
            {
                info += "texto contem >;";//Traduzir
                dangerous = true;
            }

            if (s.Contains("truncate"))
            {
                info += "texto contem TRUNCATE;";//Traduzir
                dangerous = true;
            }

            if (s.Contains(@"\"))
            {
                info += @"texto contem .\;";//Traduzir
                dangerous = true;
            }

            if (s.Contains("~/"))
            {
                info += "texto contem ~/;";//Traduzir
                dangerous = true;
            }

            if (s.Contains("shutdown"))
            {
                info += "texto contem SHUTDOWN;";//Traduzir
                dangerous = true;
            }

            if (s.Contains("shell"))
            {
                info += "texto contem SHELL;";//Traduzir
                dangerous = true;
            }

            if (s.Contains("script"))
            {
                info += "texto contem SCRIPT;";//Traduzir
                dangerous = true;
            }

            return dangerous;
        }




        /// <summary>
        /// Este metodo limpa uma string de código considerado perigoso
        /// </summary>
        /// <param name="str">string a ser limpa</param>
        /// <returns>string limpa</returns>
        public static string CleanDangerousText(string str)
        {
            string dummy = "";
            string s = str;
            StringVerification(ref str, ref dummy);
            return str;
        }




        /// <summary>
        /// Este metodo limpa uma string de código considerado perigoso
        /// </summary>
        /// <param name="str">string a ser limpa</param>
        /// <param name="info">informação do que foi feito na string</param>
        /// <returns>devolve a string limpa</returns>
        public static string CleanDangerousText(string str, ref string info)
        {
            string s = str;
            StringVerification(ref s, ref info);
            return s;
        }



        #endregion


        #region metodos privados DangerousWords


        private static void StringVerification(ref string str, ref string info)
        {
            int position;

            for (int i = 0; i < dangerousWords.Length; i++)
            {
                while (str.ToLower().Contains(dangerousWords[i]))
                {
                    position = str.ToLower().IndexOf(dangerousWords[i]);
                    if (dangerousWords[i].Length > 1)
                    {
                        info += string.Format("a palavra {0} foi alterada para {1};", dangerousWords[i], dangerousWords[i].Insert(0 + 1, "!"));//Traduzir
                        str = str.Insert(position + 1, "!");
                    }
                    else
                    {
                        info += string.Format("o caracter {0} foi alterado para !;", dangerousWords[i]);//Traduzir
                        str = str.Replace(dangerousWords[i], "!");
                    }
                }
            }
        }


        #endregion


        #region outros



        /// <summary>
        /// Este metodo conta as palavras existentes numa dada string. Parte a string em palavras pelos caracteres passados em array de char[]
        /// </summary>
        /// <param name="s">string a ser processada</param>
        /// <param name="splitingChars">array de char[] com os caracteres que devem partir a string</param>
        /// <returns>quantidade de palavras existentes</returns>
        public static int HowManyWords(string s, char[] splitingChars)
        {
            string[] words = s.Split(splitingChars);
            return words.Length;
        }

        #endregion
    }
}
