using HtmlAgilityPack;
using SMPTservice.Models;
using System.Net.Mail;

namespace SMPTservice.Service
{
    public class SMTPService
    {
        private IConfigureMailSettings _mailSettings;
        //public SMTPService(IConfigureMailSettings _mailSettings)
        //{
        //    this._mailSettings = _mailSettings;
        //}
        public SMTPService()
        {
            _mailSettings = new ConfigureMailSettings();
        }
        public void ConfigureServices()
        {   
            _mailSettings.ConfigureServices();
        }
        public void CreateSmtpClient()
        {
            _mailSettings.CreateSmtpClient();
        }
        public bool IsMailSent(SendMail mail)
        {
            try
            {
                SmtpClient smtpClient = IConfigureMailSettings.getSmtpClient();
                var email = CreateMail(mail);
                smtpClient.Send(email);
                return true;
            }
            catch (Exception ex)
            {
                File.WriteAllText(MailSettings.LogPath, ex.ToString());
                return false;
            }
        }

        public MailMessage CreateMail(SendMail mail)
        {
            var email = new MailMessage();
            if (mail.FromEmailAddress.Trim().Length == 0)
            {
                mail.FromEmailAddress = "DoNotReply@bbd.co.za";
            }
            if (mail.FromEmailName.Trim().Length == 0)
            {
                mail.FromEmailName = "Automated Email";
            }
            email.From = new MailAddress(mail.FromEmailAddress, mail.FromEmailName);
            email.Subject = mail.MailSubject;
            email.Body = mail.MailBody;
            email.IsBodyHtml = true;

            foreach (string emailAdress in mail.ToEmailAddresses.Where(s => !string.IsNullOrEmpty(s)))
                email.To.Add(emailAdress);
            foreach (string fileurl in mail.FilesToAttachPaths)
            {
                if (File.Exists(fileurl))
                    email.Attachments.Add(new Attachment(fileurl));
            }
            return email;
        }

        //public string FormatMailBody(string MailBody)
        //{
        //    HtmlDocument htmlDoc = new HtmlDocument();
        //    htmlDoc.LoadHtml(MailBody);
        //    HtmlNodeCollection htmlNodes = htmlDoc.DocumentNode.SelectNodes("//tr");
            
        //    foreach (var trNode in htmlNodes)
        //    {
        //        HtmlNode firstChild = trNode.FirstChild;
        //        if (firstChild.InnerHtml.Equals(Enum.GetName(typeof(ActionPerformed), ActionPerformed.Deleted)))
        //        {
        //            trNode.Attributes.Add("style", "color:red");
        //        }
        //        else if (firstChild.InnerHtml.Equals(Enum.GetName(typeof(ActionPerformed), ActionPerformed.Modified)))
        //        {
        //            trNode.Attributes.Add("style", "color:green");
        //        }
        //    }
        //    return htmlDoc.DocumentNode.OuterHtml;
        //}
       
    }
}
