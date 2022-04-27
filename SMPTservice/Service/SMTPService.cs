using SMPTservice.Models;
using SMPTservice.Controllers;
using SMPTservice.Service;
using System.Diagnostics;
using MimeKit;
using MailKit.Net.Smtp;
using HtmlAgilityPack;
namespace SMPTservice.Service
{
    public class SMTPService
    {
        public bool IsMailSent(SendMails mail)
        {
            try
            {
                ConfigureMailSettings settings = new ConfigureMailSettings();
                settings.ConfigureServices();
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential(MailSettings.DomainUserName, MailSettings.DomainPassword);
                smtpClient.Host = MailSettings.SMTPServerName;
                smtpClient.Port = MailSettings.Port;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
                if (mail.fromEmailAddress.Trim().Length == 0)
                {
                    mail.fromEmailAddress = "DoNotReply@bbd.co.za";
                }
                if (mail.fromEmailName.Trim().Length == 0)
                {
                    mail.fromEmailName = "Automated Email";
                }
                var email = new System.Net.Mail.MailMessage();
                email.From = new System.Net.Mail.MailAddress(mail.fromEmailAddress, mail.fromEmailName);
                email.Subject = mail.mailSubject;
                email.Body = FormatMailBody(mail.mailBody);
                email.IsBodyHtml = true;
                foreach (string emailAdress in mail.lstToEmailAddress.Where(s => string.IsNullOrEmpty(s) == false))
                {
                    email.To.Add(emailAdress);
                }
                foreach (string fileurl in mail.lstFilesToAttachPaths)
                {
                    if (System.IO.File.Exists(fileurl))
                    {
                        email.Attachments.Add(new System.Net.Mail.Attachment(fileurl));
                    }
                }
                smtpClient.Send(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string FormatMailBody(string MailBody)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(MailBody);
            HtmlNodeCollection htmlNodes = htmlDoc.DocumentNode.SelectNodes("//tr");
            
            foreach (var trNode in htmlNodes)
            {
                HtmlNode firstChild = trNode.FirstChild;
                if (firstChild.InnerHtml == "Deleted")
                {
                    trNode.Attributes.Add("style", "color:red");
                }
                
                if (firstChild.InnerHtml == "Modified")
                {
                    trNode.Attributes.Add("style", "color:green");
                }
            }
            return htmlDoc.DocumentNode.OuterHtml;
        }
       
    }
}
