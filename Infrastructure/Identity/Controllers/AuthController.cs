using Identity.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

public class AuthController(
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    IIdentityServerInteractionService interactionService)
    : Controller
{
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

        var user = await userManager.FindByNameAsync(viewModel.Username);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View(viewModel);
        }

        var result = await signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);

        if (result.Succeeded)
        {
            return Redirect(viewModel.ReturnUrl);
        }

        ModelState.AddModelError(string.Empty, "Invalid username or password");
        return View(viewModel);
    }


    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
        var viewModel = new RegisterViewModel
        {
            ReturnUrl = returnUrl,
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var user = new User
        {
            UserName = viewModel.Username,
        };

        var result = await userManager.CreateAsync(user, viewModel.Password);

        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, false);
            return Redirect(viewModel.ReturnUrl);
        }

        ModelState.AddModelError(string.Empty, "Invalid username or password");
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId)
    {
        await signInManager.SignOutAsync();
        var logoutRequest = await interactionService.GetLogoutContextAsync(logoutId);
        return Redirect(logoutRequest.PostLogoutRedirectUri);
    }
}