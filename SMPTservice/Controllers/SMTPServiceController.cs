using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMPTservice.Models;
namespace SMPTservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMTPServiceController : ControllerBase
    {
        [HttpPost("sendmail")]
        public SendMail sendMail([FromBody] SendMail mail)
        {
            return mail;
        }
    }
}
