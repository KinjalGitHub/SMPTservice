using System.Net.Mail;

namespace SMTPservice.Service
{
    public interface IConfigureMailSettings
    {
        void ConfigureServices();
        void CreateSmtpClient();
        SmtpClient getSmtpClient();
     
    }
}
