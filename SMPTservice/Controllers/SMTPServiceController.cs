using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMTPservice.Interface;
using SMTPservice.Models;
using SMTPservice.Service;
namespace SMTPservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMTPServiceController : ControllerBase
    {
        private readonly ISMTPService _smtpService;

        public SMTPServiceController(ISMTPService _smtpService)
        {
            this._smtpService = _smtpService;
        }

        [HttpPost("sendmail")]
        public bool sendMail([FromBody] SendMail mail)
        {   
            return _smtpService.IsMailSent(mail);
        }
    }
}