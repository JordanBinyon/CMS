using CMS.Interfaces.User;
using CMS.Models.Services.Pagination;
using CMS.Models.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers;

public class UsersController(IAuthenticationService authenticationService, IUserService userService)
    : BaseController(authenticationService)
{
    [Authorize]
    public IActionResult Index()
    {
        return View(PopulateBaseViewModel(new BaseViewModel()));
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<PaginatedResponse<PaginatedUser>>> GetUsers(int page = 1, string search = "",
        string sortBy = "name", string sortDirection = "asc", int pageSize = 10)
    {
        return await userService.GetUsers(page, search, sortBy, sortDirection, pageSize);
    }
}