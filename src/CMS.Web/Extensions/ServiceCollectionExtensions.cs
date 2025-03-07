using System.Reflection;

namespace CMS.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesAutomatically(this IServiceCollection services, params Assembly[] assemblies)
    {
        // Get all types from the provided assemblies
        var allTypes = assemblies.SelectMany(a => a.GetTypes());

        // Find interfaces
        var interfaces = allTypes
            .Where(t => t.IsInterface)
            .ToList();

        // Find concrete implementations
        var implementations = allTypes
            .Where(t => t.IsClass && !t.IsAbstract)
            .ToList();

        foreach (var impl in implementations)
        {
            // Find the first interface that matches the convention (e.g., IUserService -> UserService)
            var matchingInterface = impl.GetInterfaces()
                .FirstOrDefault(i => i.Name == $"I{impl.Name}" && interfaces.Contains(i));

            if (matchingInterface != null)
            {
                // Register with Scoped lifetime (adjust as needed)
                services.AddScoped(matchingInterface, impl);
            }
        }

        return services;
    }
}