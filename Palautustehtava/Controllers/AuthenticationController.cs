using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Palautustehtava.Models;
using Palautustehtava.Services.Interfaces;

namespace Palautustehtava.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticateService _authenticateService;

        public AuthenticationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Credentials tunnukset)
        {
            var loggerUser = _authenticateService.Authenticate(tunnukset.Username, tunnukset.Password);
            if (loggerUser == null)

                return BadRequest(new { message = "...on virheellinen" });

            return Ok(loggerUser);
        }
    }

    
}
