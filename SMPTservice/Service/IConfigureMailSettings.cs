using System.Net.Mail;

namespace SMPTservice.Service
{
    public interface IConfigureMailSettings
    {
        public void ConfigureServices();
        public void CreateSmtpClient();
        
        public static SmtpClient getSmtpClient()
        {
            SmtpClient smtpClient = ConfigureMailSettings.getSmtpClient();
            return smtpClient;
        }
    }
}
