using CMS.Database;
using CMS.Interfaces.User;
using CMS.Web.Extensions;
using CMS.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;
ConfigureServices(services);

WebApplication app = builder.Build();
ConfigureApp(app);

SeedDatabase(app);

app.Run();

return;

void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
    services.AddControllersWithViews();
    
    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Login"; // Explicit login page
            options.LogoutPath = "/Logout"; // Explicit logout endpoint
            options.AccessDeniedPath = "/403"; // Handle unauthorized access

            options.Cookie = new CookieBuilder
            {
                Name = ".CMS.Auth", // Unique cookie name
                HttpOnly = true,      // Prevent XSS (default, but explicit)
                SecurePolicy = CookieSecurePolicy.Always, // Force HTTPS
                SameSite = SameSiteMode.Strict, // Stronger CSRF protection
                IsEssential = true    // GDPR consent compliance
            };

            options.ExpireTimeSpan = TimeSpan.FromHours(1); // Session timeout
            options.SlidingExpiration = true; // Renew cookie if active
        });
    
    // Get references to the assemblies - (Use UserService to get the assembly)
    var interfaceAssembly = typeof(IUserService).Assembly; // From CMS.Interfaces
    var serviceAssembly = typeof(CMS.Services.UserService).Assembly;     // From CMS.Services

    // Register services automatically from both assemblies
    builder.Services.AddServicesAutomatically(interfaceAssembly, serviceAssembly);
    
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    services.AddScoped<IAuthenticationService, AuthenticationService>();
}

void ConfigureApp(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();
    app.UseStaticFiles();

    app.MapStaticAssets();
    app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();
}

void SeedDatabase(WebApplication app)
{
    // Seed the database
    using IServiceScope scope = app.Services.CreateScope();
    
    IServiceProvider serviceProvider = scope.ServiceProvider;
        
    try
    {
        DataContext context = serviceProvider.GetRequiredService<DataContext>();
            
        SeedData.SeedDatabase(context);
    }
    catch (Exception ex)
    {
        ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}