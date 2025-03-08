using System.Linq.Expressions;
using System.Reflection;
using CMS.Database;
using CMS.Helpers;
using CMS.Interfaces.User;
using CMS.Models.Database;
using CMS.Models.Services;
using CMS.Models.Services.Pagination;
using CMS.Models.Services.User;
using Microsoft.EntityFrameworkCore;

namespace CMS.Services;

public class UserService(DataContext dataContext) : IUserService
{
    public async Task<int> CreateUser(string firstName, string lastName, string email, string password)
    {
        string hashedPassword = PasswordHasher.HashPassword(password);

        User user = new User()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = hashedPassword,
            Created = DateTimeOffset.Now
        };
        
        dataContext.Users.Add(user);
        
        await dataContext.SaveChangesAsync();
        
        return user.Id;
    }

    public async Task<AuthenticatedUser?> AuthenticateUser(string email, string password)
    {
        var user = await dataContext.Users
            .Where(x => x.Email == email)
            .Select(x => new
            {
                x.Id,
                x.FirstName,
                x.LastName,
                x.Email,
                x.Password
            })
            .SingleOrDefaultAsync();

        if (user == null || !PasswordHasher.VerifyPassword(password, user.Password))
        {
            return null;
        }

        return new AuthenticatedUser()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
        };
    }

    public async Task<List<UserModel>> GetUsers()
    {
        List<UserModel> users = await dataContext.Users
            .Select(x => new UserModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            })
            .ToListAsync();
        
        return users;
    }

    public async Task<PaginatedResponse<PaginatedUser>> GetUsers(int page, string search, string sortBy, string sortDirection, int pageSize)
    {
        IQueryable<User> query = dataContext.Users.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(u => u.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                     u.LastName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                     u.Email.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        query = sortDirection.ToLower() == "desc"
            ? query.OrderByDescending(GetSortProperty<User>(sortBy))
            : query.OrderBy(GetSortProperty<User>(sortBy));

        int totalItems = await query.CountAsync();
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        List<PaginatedUser> data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new PaginatedUser
            {
                Name = x.Name,
                Email = x.Email
            })
            .ToListAsync();

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