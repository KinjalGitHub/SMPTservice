using SMTPservice.Models;

namespace SMTPservice.Interface
{
    public interface ISMTPService
    {
        bool IsMailSent(SendMail mail);
    }
}
