using CMS.Interfaces.User;
using CMS.Models.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers;

public class BaseController(IAuthenticationService authenticationService) : Controller
{
    public BaseViewModel PopulateBaseViewModel(BaseViewModel model)
    {
        var user = authenticationService.GetAuthenticatedUser();

        if (user == null)
        {
            return model;
        }

        model.UserName = user.FullName;
        
        return model;
    }
}