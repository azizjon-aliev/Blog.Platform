using Microsoft.AspNetCore.Mvc;

namespace Content.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class ApiController: ControllerBase
{
    
}