using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Code_CloudSchool.Controllers
{
    [Route("health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("is Healthy");
        }
    }
}
