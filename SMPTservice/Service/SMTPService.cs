using HtmlAgilityPack;
using SMPTservice.Models;
using System.Net;

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

                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                smtpClient.Credentials = new NetworkCredential(MailSettings.DomainUserName, MailSettings.DomainPassword);
                smtpClient.Host = MailSettings.SMTPServerName;
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;


                var email = new System.Net.Mail.MailMessage();
                if (mail.fromEmailAddress.Trim().Length == 0)
                {
                    mail.fromEmailAddress = "DoNotReply@bbd.co.za";
                }
                if (mail.fromEmailName.Trim().Length == 0)
                {
                    mail.fromEmailName = "Automated Email";
                }
                email.From = new System.Net.Mail.MailAddress(MailSettings.DomainUserName, mail.fromEmailName);
                email.Subject = mail.mailSubject;

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(mail.mailBody);

                HtmlNode docNode = doc.DocumentNode;
                var trNodes = docNode.SelectNodes("//tr");
                foreach (HtmlNode trNode in trNodes)
                {
                    var firstnode = trNode.FirstChild;
                
                        if (firstnode.InnerHtml.Equals("Deleted"))
                            trNode.Attributes.Add("style", "color: red;");
                        else
                            trNode.Attributes.Add("style", "color: green;");
                        continue;
                }

                email.Body =doc.DocumentNode.OuterHtml;
                email.IsBodyHtml = true;
                foreach (string emailAdress in mail.lstToEmailAddress.Where(s => !string.IsNullOrEmpty(s)))
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
                smtpClient.Dispose();
                    return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                //File.WriteAllText(ConfigurationManager.AppSettings["Log_Path"], ex.ToString());
                //Removing this since it was causing a loop when the emails couldn't send
                //this.SendMail("", "Error Occured Mail WebService", "johan@bbd.co.za;".Split(new char[] { ';' }).ToList<string>(), "Mail Webservice Failure", string.Concat("The following error occured at the mail service ", exception.ToString()), "".Split(new char[0]).ToList<string>());
                return false;
            }



        }
    }
}
