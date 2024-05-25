using Identity.API.DTO;
using Identity.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/v1/[controller]/[action]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (request.Password != request.ConfirmPassword)
            return BadRequest("Passwords do not match.");

        try
        {
            var token = await authService.RegisterAsync(request.Username, request.Email, request.Password);
            return Ok(token);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var token = await authService.LoginAsync(request.Username, request.Password);
            return Ok(token);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        try
        {
            var token = await authService.RefreshTokenAsync(refreshToken);
            return Ok(token);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}