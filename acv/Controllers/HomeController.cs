using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [Authorize(Policy = "Admin")]
        [HttpGet("secretapi")]
        public IActionResult SecretAPI() => Ok("Secret API");

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index() => Ok("Hello from index");

        [Authorize]
        [HttpGet("claims")]
        public IActionResult Claims()
        {
            return Ok(User.Claims.Select(x => new { Type = x.Type, Value = x.Value }));
        }

        [AllowAnonymous]
        [HttpGet("authentication")]
        public async Task<IActionResult> Authentication()
        {
            ClaimsIdentity identity = new ClaimsIdentity("CookieSeriesAuth");

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "Caina"));
            identity.AddClaim(new Claim(ClaimTypes.Email, "caina@caina.com"));
            identity.AddClaim(new Claim(ClaimTypes.Webpage, "https://caina.dev"));

            identity.AddClaim(new Claim(ClaimTypes.Role, "SecretRole"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Student"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Teen"));

            ClaimsPrincipal principal = new ClaimsPrincipal(new[] { identity });

            await HttpContext.SignInAsync(principal);

            return Ok("Teje logado");
        }
    }
}
