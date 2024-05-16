using Identity.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Identity.Controllers;

public class AuthController(
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    IIdentityServerInteractionService interactionService)
    : Controller
{
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IIdentityServerInteractionService _interactionService = interactionService;

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        var viewModel = new LoginViewModel
        {
            ReturnUrl = returnUrl,
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        
        var user = await _userManager.FindByNameAsync(viewModel.Username);
        
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View(viewModel);
        }
        
        var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);
        
        if (result.Succeeded)
        {
            return Redirect(viewModel.ReturnUrl);
        }
        
        ModelState.AddModelError(string.Empty, "Invalid username or password");
        return View(viewModel);
    }
}