using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SMPTservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMTPServiceController : ControllerBase
    {
        [Route("SendMail")]
        [HttpGet]
        public bool sendMail()
        { 
            return false;
        }

    }
}
