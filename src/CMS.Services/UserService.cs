using CMS.Database;
using CMS.Helpers;
using CMS.Interfaces.User;
using CMS.Models.Database;
using CMS.Models.Services;
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

        if (user == null || PasswordHasher.VerifyPassword(password, user.Password))
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
}