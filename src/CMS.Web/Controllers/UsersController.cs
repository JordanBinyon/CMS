using System.Linq.Expressions;
using System.Reflection;
using CMS.Models.Web.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers;

public class UsersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    private readonly List<PaginatedUser> _users =
    [
        new() { Name = "John Smith", Email = "john.smith@example.com" },
        new() { Name = "Emma Johnson", Email = "emma.johnson@example.com" },
        new() { Name = "Michael Brown", Email = "michael.brown@example.com" },
        new() { Name = "Sophie Taylor", Email = "sophie.taylor@example.com" },
        new() { Name = "Alex Wilson", Email = "alex.wilson@example.com" },
        new() { Name = "Julia Davis", Email = "julia.davis@example.comaverylongdomainname.com" },
        new() { Name = "Thomas Clark", Email = "thomas.clark@example.com" },
        new() { Name = "Olivia Lewis", Email = "olivia.lewis@example.com" },
        new() { Name = "William Walker", Email = "william.walker@example.com" },
        new() { Name = "Grace Hall", Email = "grace.hall@example.com" },
        new() { Name = "John Smith", Email = "john.smith@example.com" },
        new() { Name = "Emma Johnson", Email = "emma.johnson@example.com" },
        new() { Name = "Michael Brown", Email = "michael.brown@example.com" },
        new() { Name = "Sophie Taylor", Email = "sophie.taylor@example.com" },
        new() { Name = "Alex Wilson", Email = "alex.wilson@example.com" },
        new() { Name = "Julia Davis", Email = "julia.davis@example.comaverylongdomainname.com" },
        new() { Name = "Thomas Clark", Email = "thomas.clark@example.com" },
        new() { Name = "Olivia Lewis", Email = "olivia.lewis@example.com" },
        new() { Name = "William Walker", Email = "william.walker@example.com" },
        new() { Name = "Grace Hall", Email = "grace.hall@example.com" },
        new() { Name = "John Smith", Email = "john.smith@example.com" },
        new() { Name = "Emma Johnson", Email = "emma.johnson@example.com" },
        new() { Name = "Michael Brown", Email = "michael.brown@example.com" },
        new() { Name = "Sophie Taylor", Email = "sophie.taylor@example.com" },
        new() { Name = "Alex Wilson", Email = "alex.wilson@example.com" },
        new() { Name = "Julia Davis", Email = "julia.davis@example.comaverylongdomainname.com" },
        new() { Name = "Thomas Clark", Email = "thomas.clark@example.com" },
        new() { Name = "Olivia Lewis", Email = "olivia.lewis@example.com" },
        new() { Name = "William Walker", Email = "william.walker@example.com" },
        new() { Name = "Grace Hall", Email = "grace.hall@example.com" },
        new() { Name = "John Smith", Email = "john.smith@example.com" },
        new() { Name = "Emma Johnson", Email = "emma.johnson@example.com" },
        new() { Name = "Michael Brown", Email = "michael.brown@example.com" },
        new() { Name = "Sophie Taylor", Email = "sophie.taylor@example.com" },
        new() { Name = "Alex Wilson", Email = "alex.wilson@example.com" },
        new() { Name = "Julia Davis", Email = "julia.davis@example.comaverylongdomainname.com" },
        new() { Name = "Thomas Clark", Email = "thomas.clark@example.com" },
        new() { Name = "Olivia Lewis", Email = "olivia.lewis@example.com" },
        new() { Name = "William Walker", Email = "william.walker@example.com" },
        new() { Name = "Grace Hall", Email = "grace.hall@example.com" },
        new() { Name = "John Smith", Email = "john.smith@example.com" },
        new() { Name = "Emma Johnson", Email = "emma.johnson@example.com" },
        new() { Name = "Michael Brown", Email = "michael.brown@example.com" },
        new() { Name = "Sophie Taylor", Email = "sophie.taylor@example.com" },
        new() { Name = "Alex Wilson", Email = "alex.wilson@example.com" },
        new() { Name = "Julia Davis", Email = "julia.davis@example.comaverylongdomainname.com" },
        new() { Name = "Thomas Clark", Email = "thomas.clark@example.com" },
        new() { Name = "Olivia Lewis", Email = "olivia.lewis@example.com" },
        new() { Name = "William Walker", Email = "william.walker@example.com" },
        new() { Name = "Grace Hall", Email = "grace.hall@example.com" },
        new() { Name = "John Smith", Email = "john.smith@example.com" },
        new() { Name = "Emma Johnson", Email = "emma.johnson@example.com" },
        new() { Name = "Michael Brown", Email = "michael.brown@example.com" },
        new() { Name = "Sophie Taylor", Email = "sophie.taylor@example.com" },
        new() { Name = "Alex Wilson", Email = "alex.wilson@example.com" },
        new() { Name = "Julia Davis", Email = "julia.davis@example.comaverylongdomainname.com" },
        new() { Name = "Thomas Clark", Email = "thomas.clark@example.com" },
        new() { Name = "Olivia Lewis", Email = "olivia.lewis@example.com" },
        new() { Name = "William Walker", Email = "william.walker@example.com" },
        new() { Name = "Grace Hall", Email = "grace.hall@example.com" },
        new() { Name = "John Smith", Email = "john.smith@example.com" },
        new() { Name = "Emma Johnson", Email = "emma.johnson@example.com" },
        new() { Name = "Michael Brown", Email = "michael.brown@example.com" },
        new() { Name = "Sophie Taylor", Email = "sophie.taylor@example.com" },
        new() { Name = "Alex Wilson", Email = "alex.wilson@example.com" },
        new() { Name = "Julia Davis", Email = "julia.davis@example.comaverylongdomainname.com" },
        new() { Name = "Thomas Clark", Email = "thomas.clark@example.com" },
        new() { Name = "Olivia Lewis", Email = "olivia.lewis@example.com" },
        new() { Name = "William Walker", Email = "william.walker@example.com" },
        new() { Name = "Grace Hall", Email = "grace.hall@example.com" },
        new() { Name = "John Smith", Email = "john.smith@example.com" },
        new() { Name = "Emma Johnson", Email = "emma.johnson@example.com" },
        new() { Name = "Michael Brown", Email = "michael.brown@example.com" },
        new() { Name = "Sophie Taylor", Email = "sophie.taylor@example.com" },
        new() { Name = "Alex Wilson", Email = "alex.wilson@example.com" },
        new() { Name = "Julia Davis", Email = "julia.davis@example.comaverylongdomainname.com" },
        new() { Name = "Thomas Clark", Email = "thomas.clark@example.com" },
        new() { Name = "Olivia Lewis", Email = "olivia.lewis@example.com" },
        new() { Name = "William Walker", Email = "william.walker@example.com" },
        new() { Name = "Grace Hall", Email = "grace.hall@example.com" },
        new() { Name = "John Smith", Email = "john.smith@example.com" },
        new() { Name = "Emma Johnson", Email = "emma.johnson@example.com" },
        new() { Name = "Michael Brown", Email = "michael.brown@example.com" },
        new() { Name = "Sophie Taylor", Email = "sophie.taylor@example.com" },
        new() { Name = "Alex Wilson", Email = "alex.wilson@example.com" },
        new() { Name = "Julia Davis", Email = "julia.davis@example.comaverylongdomainname.com" },
        new() { Name = "Thomas Clark", Email = "thomas.clark@example.com" },
        new() { Name = "Olivia Lewis", Email = "olivia.lewis@example.com" },
        new() { Name = "William Walker", Email = "william.walker@example.com" },
        new() { Name = "Grace Hall", Email = "grace.hall@example.com" },
        new() { Name = "John Smith", Email = "john.smith@example.com" },
        new() { Name = "Emma Johnson", Email = "emma.johnson@example.com" },
        new() { Name = "Michael Brown", Email = "michael.brown@example.com" },
        new() { Name = "Sophie Taylor", Email = "sophie.taylor@example.com" },
        new() { Name = "Alex Wilson", Email = "alex.wilson@example.com" },
        new() { Name = "Julia Davis", Email = "julia.davis@example.comaverylongdomainname.com" },
        new() { Name = "Thomas Clark", Email = "thomas.clark@example.com" },
        new() { Name = "Olivia Lewis", Email = "olivia.lewis@example.com" },
        new() { Name = "William Walker", Email = "william.walker@example.com" },
        new() { Name = "Grace Hall", Email = "grace.hall@example.com" }
    ];

    [HttpGet]
    public ActionResult<PaginatedResponse<PaginatedUser>> GetUsers(int page = 1, string search = "", string sortBy = "name", string sortDirection = "asc")
    {
        const int pageSize = 10;
        var query = _users.AsQueryable();

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

        var totalItems = query.Count();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        var data = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

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
        PropertyInfo? property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        
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