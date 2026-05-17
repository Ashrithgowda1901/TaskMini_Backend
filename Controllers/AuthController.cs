using Microsoft.AspNetCore.Mvc;
using TaskMini.Data;

namespace TaskMini.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        


        [HttpPost]
        public IActionResult Register()
        {
            return Ok();
        }
    }
}
