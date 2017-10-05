using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    public class MyException:Exception
    {
        public enum OriginClassSqlError
        { SQLStoredProcedure, SQLFunction }


        string module;
        string classe;
        string location;
        string message;



        public MyException(string module, string classe, string location, string nessage)
        {
            this.module = module;
            this.classe = classe;
            this.location = location;
            this.message = string.Format("#[{0}] {1}$ Line: {2}, {3}", module, classe, location, nessage);
        }


        public MyException(string module, OriginClassSqlError originClassSqlError, string originLocation, SqlErrorCollection sqlErrors)
        {

            this.module = module;
            this.classe = originClassSqlError.ToString();
            this.location = originLocation;
            this.message = "";
            foreach (SqlError sqlError in sqlErrors)
            {
                this.message += string.Format("#[{0}] {1}$ Line: {2}, {3}", sqlError.Source, sqlError.Procedure, sqlError.LineNumber, sqlError.Message);
            }
        }


        public string Module
        {
            get { return module; }
        }


        public string Classe
        {
            get { return classe; }
        }


        public string Location
        {
            get { return location; }
        }


        public override string Source
        {
            get
            {
                return string.Format("{0} / {1} / {2}", module, classe, location);
            }
        }


        public override string Message
        {
            get
            {
                return message;
            }
        }


    }
}
