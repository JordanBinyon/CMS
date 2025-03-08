using System.Linq.Expressions;
using System.Reflection;
using CMS.Interfaces.User;
using CMS.Models.Services.User;
using CMS.Models.Web.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers;

public class UsersController(IUserService userService) : Controller
{
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<PaginatedResponse<PaginatedUser>>> GetUsers(int page = 1, string search = "",
        string sortBy = "name", string sortDirection = "asc")
    {
        const int pageSize = 11;

        List<UserModel> users = await userService.GetUsers();
        IQueryable<PaginatedUser> query = users.Select(x => new PaginatedUser()
        {
            Name = x.Name,
            Email = x.Email
        }).AsQueryable();

        // Apply search filter
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(u => u.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                     u.Email.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        // Apply sorting
        query = sortDirection.ToLower() == "desc"
            ? query.OrderByDescending(GetSortProperty<PaginatedUser>(sortBy))
            : query.OrderBy(GetSortProperty<PaginatedUser>(sortBy));

        int totalItems = query.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        List<PaginatedUser> data = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return new PaginatedResponse<PaginatedUser>
        {
            Data = data,
            TotalPages = totalPages,
            CurrentPage = page
        };
    }

    // Generic method to dynamically get sort property using reflection
    private Expression<Func<T, object>> GetSortProperty<T>(string propertyName)
    {
        PropertyInfo? property = typeof(T).GetProperty(propertyName,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (property == null)
        {
            throw new ArgumentException($"Property {propertyName} not found on type {typeof(T).Name}");
        }

        ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
        MemberExpression propertyAccess = Expression.Property(parameter, property);
        UnaryExpression conversion = Expression.Convert(propertyAccess, typeof(object));

        return Expression.Lambda<Func<T, object>>(conversion, parameter);
    }
}