using CMS.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CMS.Database;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}