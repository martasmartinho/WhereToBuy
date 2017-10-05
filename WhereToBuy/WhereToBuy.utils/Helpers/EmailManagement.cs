using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace WhereToBuy.utils
{
    public class EmailManagement
    {

        private bool smtpRequiresAuthentication;
        private string smtpServer;
        private string smtpServerUsername;
        private string smtpServerPassword;

        public EmailManagement(string smtpServer)
        {
            this.smtpRequiresAuthentication = false;
            this.smtpServer = smtpServer;
        }

        public EmailManagement(string smtpServer, string smtpServerUsername, string smtpServerPassword)
        {
            this.smtpRequiresAuthentication = true;
            this.smtpServer = smtpServer;
            this.smtpServerUsername = smtpServerUsername;
            this.smtpServerPassword = smtpServerPassword;
        }

        public void Send(MailAddress from, MailAddress to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.SubjectEncoding = Encoding.UTF8;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.From = from;
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            this.Send(mail);
        }

        public void Send(MailMessage mail)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(this.smtpServer);
                if (this.smtpRequiresAuthentication)
                {
                    NetworkCredential networkCredential = new NetworkCredential(this.smtpServerUsername, this.smtpServerPassword);
                    smtpClient.Credentials = (ICredentialsByHost)networkCredential;
                }

                smtpClient.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
