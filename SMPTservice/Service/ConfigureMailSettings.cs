using SMPTservice.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace SMPTservice.Service
{
    public  class ConfigureMailSettings
    {
        private static IConfiguration Configuration;
        private static SmtpClient smtpClient;
        private ConfigureMailSettings()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }
        public static ConfigureMailSettings configureMailSettings;
        public static ConfigureMailSettings GetInstance() 
        {
            if (configureMailSettings == null)
            {
                configureMailSettings = new ConfigureMailSettings();
                ConfigureServices();
                CreateSmtpClient();
            }
            return configureMailSettings;
        }
        public static void ConfigureServices()
        {
            MailSettings.DomainUserName = Configuration.GetSection("MailSettings")["DomainUserName"];
            MailSettings.DomainPassword = Configuration.GetSection("MailSettings")["DomainPassword"];
            MailSettings.SMTPServerName = Configuration.GetSection("MailSettings")["SMTPServerName"];
            MailSettings.Log_Path = Configuration.GetSection("MailSettings")["Log_Path"];
        }

        public static void CreateSmtpClient()
        {
            smtpClient = new SmtpClient();
            smtpClient.Credentials = new System.Net.NetworkCredential(MailSettings.DomainUserName, MailSettings.DomainPassword);
            smtpClient.Host = MailSettings.SMTPServerName;
            smtpClient.UseDefaultCredentials = false;
        }

        public  SmtpClient getSmtpClient()
        {
            return smtpClient;
        }
    }
}
