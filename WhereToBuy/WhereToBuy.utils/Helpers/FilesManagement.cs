using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace WhereToBuy.utils
{
    public static class FileManagement
    {

        #region Files

        public static void Save(string fileName, object obj)
        {
            new BinaryFormatter().Serialize((Stream)new FileStream(fileName, FileMode.Create), obj);
        }

        public static object Edit(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            object obj = new BinaryFormatter().Deserialize((Stream)fileStream);
            fileStream.Close();
            return obj;
        }

        #endregion
    }
}
