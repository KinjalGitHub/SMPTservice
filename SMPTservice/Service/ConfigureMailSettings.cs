using SMTPservice.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;


namespace SMTPservice.Service
{
    public class ConfigureMailSettings : IConfigureMailSettings
    {
        private IConfiguration Configuration;
        private SmtpClient smtpClient;
        private string key = "a18cd5898a4f4133bbct2ea2315a1916";
        public ConfigureMailSettings()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }
        public  void ConfigureServices()
        {
            string encryptedPassword = Configuration.GetSection("MailSettings")["DomainPassword"];
            MailSettings.DomainUserName = Configuration.GetSection("MailSettings")["DomainUserName"];
            MailSettings.DomainPassword = EncryptPassword.DecryptPassword(key,encryptedPassword);
            MailSettings.SMTPServerName = Configuration.GetSection("MailSettings")["SMTPServerName"];
            MailSettings.LogPath = Configuration.GetSection("MailSettings")["Log_Path"];
        }
        public void CreateSmtpClient()
        {
            smtpClient = new SmtpClient();
            smtpClient.Credentials = new System.Net.NetworkCredential(MailSettings.DomainUserName, MailSettings.DomainPassword);
            smtpClient.Host = MailSettings.SMTPServerName;
            smtpClient.UseDefaultCredentials = false;
        }
        public SmtpClient getSmtpClient()
        {
            return smtpClient;
        }
    }
}
