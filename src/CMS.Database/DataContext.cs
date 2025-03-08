using CMS.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CMS.Database;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Name)
                .HasComputedColumnSql("CONCAT(FirstName, ' ', LastName)", stored: true);
        });
    }
}