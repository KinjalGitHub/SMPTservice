using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMPTservice.Models;
using SMPTservice.Service;
namespace SMPTservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMTPServiceController : ControllerBase
    {
        [HttpPost("sendmail")]
        public bool sendMail([FromBody] SendMail mail)
        {
            SMTPService service = new SMTPService();    
            return service.IsMailSent(mail);
        }
    }
}
