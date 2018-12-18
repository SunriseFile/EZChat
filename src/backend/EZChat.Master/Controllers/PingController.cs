using Microsoft.AspNetCore.Mvc;

namespace EZChat.Master.Controllers
{
    [Route("ping")]
    public class PingController : Controller
    {
        [HttpGet]
        public string Ping()
        {
            return $"pong for {Request.HttpContext.User.Identity.Name}";
        }
    }
}
