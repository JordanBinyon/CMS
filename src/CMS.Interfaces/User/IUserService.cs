using CMS.Models.Services;
using CMS.Models.Services.User;

namespace CMS.Interfaces.User;

public interface IUserService
{
    Task<int> CreateUser(string firstName, string lastName, string email, string password);
    
    Task<AuthenticatedUser?> AuthenticateUser(string email, string password);

    Task<List<UserModel>> GetUsers();
}