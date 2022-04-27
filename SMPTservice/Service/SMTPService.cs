using HtmlAgilityPack;
using SMPTservice.Models;
using System.Net.Mail;

namespace SMPTservice.Service
{
    public class SMTPService
    {
        public bool IsMailSent(SendMail mail)
        {
            try
            {
                ConfigureMailSettings settings = new ConfigureMailSettings();
                settings.ConfigureServices();

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential(MailSettings.DomainUserName, MailSettings.DomainPassword);
                smtpClient.Host = MailSettings.SMTPServerName;
                smtpClient.Port = MailSettings.Port;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
                if (mail.FromEmailAddress.Trim().Length == 0)
                {
                    mail.FromEmailAddress = "DoNotReply@bbd.co.za";
                }
                if (mail.FromEmailName.Trim().Length == 0)
                {
                    mail.FromEmailName = "Automated Email";
                }
                var email = new MailMessage();
                email.From = new MailAddress(mail.FromEmailAddress, mail.FromEmailName);
                email.Subject = mail.MailSubject;
                email.Body = FormatMailBody(mail.MailBody);
                email.IsBodyHtml = true;
                foreach (string emailAdress in mail.ToEmailAddresses.Where(s => !string.IsNullOrEmpty(s)))
                {
                    email.To.Add(emailAdress);
                }
                foreach (string fileurl in mail.FilesToAttachPaths)
                {
                    if (File.Exists(fileurl))
                    {
                        email.Attachments.Add(new Attachment(fileurl));
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
