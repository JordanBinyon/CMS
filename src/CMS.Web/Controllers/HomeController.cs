using System.Diagnostics;
using CMS.Interfaces.User;
using CMS.Models.Services;
using CMS.Models.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CMS.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Web.Controllers;

public class HomeController(
    IAuthenticationService authenticationService,
    IUserService userService) : BaseController(authenticationService)
{
    public IActionResult Index()
    {
        return View();
    }

    [Route("Login")]
    public IActionResult Login()
    {
        if (authenticationService.IsAuthenticated())
        {
            return RedirectToAction("Dashboard", "Home");
        }

        return View();
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        AuthenticatedUser? result = await userService.AuthenticateUser(model.Email, model.Password);

        if (result == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid email or password");
            
            return View(model);
        }
        
        await authenticationService.SignIn(result, true);

        return RedirectToAction("Dashboard", "Home");
    }

    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        await authenticationService.SignOut();
        
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    [Route("Dashboard")]
    public IActionResult Dashboard()
    {
        return View(PopulateBaseViewModel(new BaseViewModel()));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Route("403")]
    public IActionResult PermissionDenied()
    {
        return View();
    }
}