using Microsoft.Extensions.Logging.Abstractions;
using SMTPservice.Models;
using System.Net.Mail;
using SMTPservice.Controllers;
using SMTPservice.Interface;

namespace SMTPservice.Service
{
    public class SMTPService : ISMTPService
    {
        private IConfigureMailSettings _mailSettings;

        private readonly ILogger<SMTPService> _logger;
        public SMTPService(ILogger<SMTPService> logger,IConfigureMailSettings _mailSettings)
        {
            this._logger = logger;
            this._mailSettings = _mailSettings;
            _mailSettings.ConfigureServices();
            _mailSettings.CreateSmtpClient();
        }

        public bool IsMailSent(SendMail mail)
        {
            try
            {
                SmtpClient smtpClient = _mailSettings.getSmtpClient();
                var email = CreateMail(mail);
                smtpClient.Send(email);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
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
    }
}
