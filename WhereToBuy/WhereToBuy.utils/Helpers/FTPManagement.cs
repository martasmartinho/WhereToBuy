using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace WhereToBuy.utils
{
    public class FTPManagement
    {
        private string server;
        private string username;
        private string password;

        public FTPManagement(string server, string username, string password)
        {
            if (!server.ToLower().StartsWith("ftp://"))
                this.server = "ftp://" + server;
            this.username = username;
            this.password = password;
        }

        public void UploadFile(string localFileToUpload, string serverFileToSave)
        {
            string address = serverFileToSave.ToLower().StartsWith("/") ? this.server + serverFileToSave : this.server + "/" + serverFileToSave;
            try
            {
                new WebClient()
                {
                    Credentials = ((ICredentials)new NetworkCredential(this.username, this.password))
                }.UploadFile(address, localFileToUpload);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DownloadFile(string serverFileToDownload, string localFileToSave)
        {
            string address = serverFileToDownload.ToLower().StartsWith("/") ? this.server + serverFileToDownload : this.server + "/" + serverFileToDownload;
            try
            {
                new WebClient()
                {
                    Credentials = ((ICredentials)new NetworkCredential(this.username, this.password))
                }.DownloadFile(address, localFileToSave);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Stream DownloadData(string serverFileToDownload)
        {
            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(serverFileToDownload.ToLower().StartsWith("/") ? this.server + serverFileToDownload : this.server + "/" + serverFileToDownload);
            ftpWebRequest.Method = "RETR";
            ftpWebRequest.Credentials = (ICredentials)new NetworkCredential(this.username, this.password);
            return ftpWebRequest.GetResponse().GetResponseStream();
        }

    }
}
