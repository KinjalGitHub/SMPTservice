using SMPTservice.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace SMPTservice.Service
{
    public class ConfigureMailSettings : IConfigureMailSettings
    {
        private static IConfiguration Configuration;
        private static SmtpClient smtpClient;
        public ConfigureMailSettings()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }
        public  void ConfigureServices()
        {
            MailSettings.DomainUserName = Configuration.GetSection("MailSettings")["DomainUserName"];
            MailSettings.DomainPassword = Configuration.GetSection("MailSettings")["DomainPassword"];
            MailSettings.SMTPServerName = Configuration.GetSection("MailSettings")["SMTPServerName"];
            MailSettings.LogPath = Configuration.GetSection("MailSettings")["Log_Path"];
        }
        public  void CreateSmtpClient()
        {
            smtpClient = new SmtpClient();
            smtpClient.Credentials = new System.Net.NetworkCredential(MailSettings.DomainUserName, MailSettings.DomainPassword);
            smtpClient.Host = MailSettings.SMTPServerName;
            smtpClient.UseDefaultCredentials = false;
        }
        public static SmtpClient getSmtpClient()
        {
            return smtpClient;
        }
    }
}
