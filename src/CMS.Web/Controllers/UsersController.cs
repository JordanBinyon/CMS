using System.Linq.Expressions;
using System.Reflection;
using CMS.Models.Web.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers;

public class UsersController : Controller
{
    [Authorize]
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
        new() { Name = "Johnny Smithson", Email = "johnny.smith1@example.com" },
        new() { Name = "Emmy Johnston", Email = "emmy.johnson2@example.com" },
        new() { Name = "Mike Browning", Email = "mike.brown3@example.com" },
        new() { Name = "Sophia Taylors", Email = "sophia.taylor4@example.com" },
        new() { Name = "Alexander Wilson", Email = "alexander.wilson5@example.com" },
        new() { Name = "Julie Davidson", Email = "julie.davis6@example.comaverylongdomainname.com" },
        new() { Name = "Tom Clarkson", Email = "tom.clark7@example.com" },
        new() { Name = "Liv Lewis", Email = "liv.lewis8@example.com" },
        new() { Name = "Will Walker", Email = "will.walker9@example.com" },
        new() { Name = "Gracie Halliday", Email = "gracie.hall10@example.com" },
        new() { Name = "Jonathan Smith", Email = "jonathan.smith11@example.com" },
        new() { Name = "Emily Johnson", Email = "emily.johnson12@example.com" },
        new() { Name = "Mikey Brown", Email = "mikey.brown13@example.com" },
        new() { Name = "Sofie Taylor", Email = "sofie.taylor14@example.com" },
        new() { Name = "Alexa Wilson", Email = "alexa.wilson15@example.com" },
        new() { Name = "Juliana Davis", Email = "juliana.davis16@example.comaverylongdomainname.com" },
        new() { Name = "Tommy Clark", Email = "tommy.clark17@example.com" },
        new() { Name = "Olive Lewis", Email = "olive.lewis18@example.com" },
        new() { Name = "Liam Walker", Email = "liam.walker19@example.com" },
        new() { Name = "Gracelyn Hall", Email = "gracelyn.hall20@example.com" },
        new() { Name = "John Smith Jr", Email = "john.smith21@example.com" },
        new() { Name = "Emma Jonson", Email = "emma.jonson22@example.com" },
        new() { Name = "Michael Browne", Email = "michael.browne23@example.com" },
        new() { Name = "Sophie Tayler", Email = "sophie.tayler24@example.com" },
        new() { Name = "Alex Wilsen", Email = "alex.wilsen25@example.com" },
        new() { Name = "Julia Davies", Email = "julia.davies26@example.comaverylongdomainname.com" },
        new() { Name = "Thomas Clarke", Email = "thomas.clarke27@example.com" },
        new() { Name = "Olivia Lewes", Email = "olivia.lewes28@example.com" },
        new() { Name = "William Walkers", Email = "william.walkers29@example.com" },
        new() { Name = "Grace Hally", Email = "grace.hally30@example.com" },
        new() { Name = "John Smyth", Email = "john.smyth31@example.com" },
        new() { Name = "Emma Johnsen", Email = "emma.johnsen32@example.com" },
        new() { Name = "Michel Brown", Email = "michel.brown33@example.com" },
        new() { Name = "Sophy Taylor", Email = "sophy.taylor34@example.com" },
        new() { Name = "Alexis Wilson", Email = "alexis.wilson35@example.com" },
        new() { Name = "Juli Davis", Email = "juli.davis36@example.comaverylongdomainname.com" },
        new() { Name = "Thom Clark", Email = "thom.clark37@example.com" },
        new() { Name = "Ollie Lewis", Email = "ollie.lewis38@example.com" },
        new() { Name = "Bill Walker", Email = "bill.walker39@example.com" },
        new() { Name = "Gracey Hall", Email = "gracey.hall40@example.com" },
        new() { Name = "Jon Smith", Email = "jon.smith41@example.com" },
        new() { Name = "Em Johnson", Email = "em.johnson42@example.com" },
        new() { Name = "Mick Brown", Email = "mick.brown43@example.com" },
        new() { Name = "Soph Taylor", Email = "soph.taylor44@example.com" },
        new() { Name = "Al Wilson", Email = "al.wilson45@example.com" },
        new() { Name = "Jules Davis", Email = "jules.davis46@example.comaverylongdomainname.com" },
        new() { Name = "Tom Clarky", Email = "tom.clarky47@example.com" },
        new() { Name = "Livy Lewis", Email = "livy.lewis48@example.com" },
        new() { Name = "Willy Walker", Email = "willy.walker49@example.com" },
        new() { Name = "Grace Halle", Email = "grace.halle50@example.com" },
        new() { Name = "Johnnie Smith", Email = "johnnie.smith51@example.com" },
        new() { Name = "Emmie Johnson", Email = "emmie.johnson52@example.com" },
        new() { Name = "Mikey Browne", Email = "mikey.browne53@example.com" },
        new() { Name = "Sofia Taylor", Email = "sofia.taylor54@example.com" },
        new() { Name = "Alec Wilson", Email = "alec.wilson55@example.com" },
        new() { Name = "Julianna Davis", Email = "julianna.davis56@example.comaverylongdomainname.com" },
        new() { Name = "Thommy Clark", Email = "thommy.clark57@example.com" },
        new() { Name = "Olly Lewis", Email = "olly.lewis58@example.com" },
        new() { Name = "Willie Walker", Email = "willie.walker59@example.com" },
        new() { Name = "Graci Hall", Email = "graci.hall60@example.com" },
        new() { Name = "Jack Smith", Email = "jack.smith61@example.com" },
        new() { Name = "Ella Johnson", Email = "ella.johnson62@example.com" },
        new() { Name = "Max Brown", Email = "max.brown63@example.com" },
        new() { Name = "Sara Taylor", Email = "sara.taylor64@example.com" },
        new() { Name = "Andy Wilson", Email = "andy.wilson65@example.com" },
        new() { Name = "Jenny Davis", Email = "jenny.davis66@example.comaverylongdomainname.com" },
        new() { Name = "Tim Clark", Email = "tim.clark67@example.com" },
        new() { Name = "Lily Lewis", Email = "lily.lewis68@example.com" },
        new() { Name = "Ben Walker", Email = "ben.walker69@example.com" },
        new() { Name = "Rose Hall", Email = "rose.hall70@example.com" },
        new() { Name = "James Smith", Email = "james.smith71@example.com" },
        new() { Name = "Kate Johnson", Email = "kate.johnson72@example.com" },
        new() { Name = "Robert Brown", Email = "robert.brown73@example.com" },
        new() { Name = "Lucy Taylor", Email = "lucy.taylor74@example.com" },
        new() { Name = "David Wilson", Email = "david.wilson75@example.com" },
        new() { Name = "Mary Davis", Email = "mary.davis76@example.comaverylongdomainname.com" },
        new() { Name = "Peter Clark", Email = "peter.clark77@example.com" },
        new() { Name = "Anna Lewis", Email = "anna.lewis78@example.com" },
        new() { Name = "Henry Walker", Email = "henry.walker79@example.com" },
        new() { Name = "Jane Hall", Email = "jane.hall80@example.com" },
        new() { Name = "Sam Smith", Email = "sam.smith81@example.com" },
        new() { Name = "Zoe Johnson", Email = "zoe.johnson82@example.com" },
        new() { Name = "Chris Brown", Email = "chris.brown83@example.com" },
        new() { Name = "Mia Taylor", Email = "mia.taylor84@example.com" },
        new() { Name = "Nick Wilson", Email = "nick.wilson85@example.com" }
    ];

    [HttpGet]
    [Authorize]
    public ActionResult<PaginatedResponse<PaginatedUser>> GetUsers(int page = 1, string search = "", string sortBy = "name", string sortDirection = "asc")
    {
        const int pageSize = 11;
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