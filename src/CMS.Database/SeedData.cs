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
            Password = PasswordHasher.HashPassword("password"),
            Created = DateTimeOffset.Now
        });

        // Arrays of sample first and last names
        string[] firstNames = { "James", "Emma", "Liam", "Olivia", "Noah", "Sophia", "William", "Ava", 
            "Michael", "Isabella", "Alexander", "Mia", "Daniel", "Charlotte", "Henry", "Amelia" };
    
        string[] lastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", 
            "Miller", "Davis", "Rodriguez", "Martinez", "Wilson", "Anderson", "Taylor", "Thomas" };

        // Random number generator
        Random random = new Random();
    
        // Generate 100 random users
        for (int i = 0; i < 100; i++)
        {
            string firstName = firstNames[random.Next(firstNames.Length)];
            string lastName = lastNames[random.Next(lastNames.Length)];
            string email = $"{firstName.ToLower()}.{lastName.ToLower()}{random.Next(100, 999)}@example.com";

            context.Users.Add(new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = PasswordHasher.HashPassword("password"), // Using a stronger default password
                Created = DateTimeOffset.Now.AddDays(-random.Next(0, 365)) // Random creation date within last year
            });
        }

        // Save changes to the database
        context.SaveChanges();
    }
}