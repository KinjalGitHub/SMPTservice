using SMPTservice.Models;
using Microsoft.Extensions.Configuration;
namespace SMPTservice.Service
{
    public class ConfigureMailSettings
    {
        private IConfiguration Configuration;

        public ConfigureMailSettings()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        public void ConfigureServices()
        {
            MailSettings.DomainUserName = Configuration.GetSection("MailSettings")["DomainUserName"];
            MailSettings.DomainPassword = Configuration.GetSection("MailSettings")["DomainPassword"];
            MailSettings.SMTPServerName = Configuration.GetSection("MailSettings")["SMTPServerName"];
            MailSettings.Port = Convert.ToInt32(Configuration.GetSection("MailSettings")["Port"]);
        }
    }
}
