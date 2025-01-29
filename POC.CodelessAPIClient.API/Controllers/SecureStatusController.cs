using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace POC.CodelessAPIClient.API.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class SecureStatusController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Authhorised");
    }
}