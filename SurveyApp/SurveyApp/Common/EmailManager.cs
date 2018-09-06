using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Net;
using System.Net.Mail;

using System.Web;

namespace SurveyApp.Common
{
    internal class EmailManager
    {
        public static void SendEmail(string Subject, string Body, string To)
        {

            string From = ConfigurationManager.AppSettings.Get("UserID");
            string Password = ConfigurationManager.AppSettings.Get("Password");
            string SMTPPort = ConfigurationManager.AppSettings.Get("SMTPPort");
            string Host = ConfigurationManager.AppSettings.Get("Host");
            MailMessage mail = new MailMessage();
            mail.To.Add(To);
            mail.From = new MailAddress(From);
            mail.Subject = Subject;
            mail.Body = Body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = Host;
            smtp.Port = System.Convert.ToInt16(SMTPPort);
            smtp.Credentials = new NetworkCredential(From, Password);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}