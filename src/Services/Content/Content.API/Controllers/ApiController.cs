using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Content.API.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public abstract class ApiController : ControllerBase
{
}