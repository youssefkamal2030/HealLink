using Microsoft.AspNetCore.Mvc;


namespace HealLink.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
      public async Task<IActionResult> GetProfile(Guid userId)
        {
           
            return Ok(new { Message = "Profile retrieval not implemented yet." });
        }
    }
}
