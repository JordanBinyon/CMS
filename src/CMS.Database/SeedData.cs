using CMS.Helpers;
using CMS.Models.Database;

namespace CMS.Database;

public static class SeedData
{
    public static void SeedDatabase(DataContext context)
    {
        context.Database.EnsureCreated();

        // Check if any users exist
        if (context.Users.Any()) 
            return;
        
        // Add a test user
        context.Users.Add(new User
        {
            FirstName = "Jordan",
            LastName = "Binyon",
            Email = "jordan.binyon@spirelabs.co.uk",
            Password = PasswordHasher.HashPassword("Password"),
            Created = DateTimeOffset.Now
        });

        // Save changes to the database
        context.SaveChanges();
    }
}