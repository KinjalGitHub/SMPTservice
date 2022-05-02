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

        }

        public static ConfigureMailSettings obj;
        public static ConfigureMailSettings GetInstance() 
        {
            if (obj == null)
            {
                obj = new ConfigureMailSettings();
                ConfigureServices();
                CreateSmtpClient();

            }
            return obj;

        }

        public static void ConfigureServices()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            MailSettings.DomainUserName = Configuration.GetSection("MailSettings")["DomainUserName"];
            MailSettings.DomainPassword = Configuration.GetSection("MailSettings")["DomainPassword"];
            MailSettings.SMTPServerName = Configuration.GetSection("MailSettings")["SMTPServerName"];
            MailSettings.Port = Convert.ToInt32(Configuration.GetSection("MailSettings")["Port"]);
            MailSettings.log_path = Configuration.GetSection("MailSettings")["Log_Path"];
        }

        public static void CreateSmtpClient()
        {
            smtpClient = new SmtpClient();
            smtpClient.Credentials = new System.Net.NetworkCredential(MailSettings.DomainUserName, MailSettings.DomainPassword);
            smtpClient.Host = MailSettings.SMTPServerName;
            smtpClient.Port = MailSettings.Port;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
        }

        public  SmtpClient getSmtpClient()
        {
            return smtpClient;
        }

         public string getLog_path()
        {
            return MailSettings.log_path;
        }

    }
}
