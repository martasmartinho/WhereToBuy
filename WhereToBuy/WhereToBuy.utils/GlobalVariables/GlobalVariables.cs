using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using WhereToBuy.entities;


namespace WhereToBuy.utils.GlobalVariables
{
    public static class GlobalVariables
    {

        

        private static string projectName = "WhereToBuy";

        public static string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        private static string assemblyName = "WhereToBuy.web";

        public static string AssemblyName
        {
            get { return assemblyName; }
            set { assemblyName = value; }
        }

        private static string languagePath = "WhereToBuy.web.App_GlobalResources.lang";

        public static string LanguagePath
        {
            get { return languagePath; }
            set { languagePath = value; }
        }

        static ResourceManager resource;    // declare Resource manager to access to specific cultureinfo


        public static ResourceManager Resource
        {
            get
            {
                if (resource == null)
                {
                    resource = new ResourceManager(languagePath, SystemAssembly);
                }
                return resource;
            }
            set { resource = value; }
        }


        static CultureInfo culture;            // declare culture info

        public static CultureInfo Culture
        {
            get
            {
                if (culture == null)
                {
                    culture = new CultureInfo("en-US");
                }
                return culture;
            }
            set { culture = value; }
        }



        static Language language;            // declare culture info

        public static Language Language
        {
            get
            {
                if (language == null)
                {
                    language = new Language();
                }
                return language;
            }
            set { language = value; }
        }


        static Assembly systemAssembly;

        public static Assembly SystemAssembly
        {
            get
            {
                if (systemAssembly == null)
                {
                    systemAssembly = Assembly.Load(assemblyName);
                }
                return systemAssembly;
            }
            set { systemAssembly = value; }
        }


    }
}
