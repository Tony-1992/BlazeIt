using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazeIt.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        [Route("status")]
        [Authorize]
        public IActionResult CheckStatus()
        {
            return Ok(new
            {
                Status = "API is currently running"
            });
        }
    }
}
