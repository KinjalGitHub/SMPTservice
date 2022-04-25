using SMPTservice.Models;
using SMPTservice.Controllers;
using SMPTservice.Service;
using System.Diagnostics;
using MimeKit;
using MailKit.Net.Smtp;
namespace SMPTservice.Service
{
    public class SMTPService
    {
        public bool IsMailSent(SendMails mail)
        {
            ConfigureMailSettings settings = new ConfigureMailSettings();
            settings.ConfigureServices();

            //var message = new MimeMessage();
            //message.From.Add(new MailboxAddress("test project jayesh", "8902jerry@gmail.com"));

            return false;
        }
    }
}
