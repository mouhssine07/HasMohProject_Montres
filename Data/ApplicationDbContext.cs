using Microsoft.EntityFrameworkCore;
namespace HasMohProject.Models;  // Adjust based on where your models are located

public class ApplicationDbContext : DbContext
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    
    public DbSet<User> Users { get; set; }  
    public DbSet<Product> Products { get; set; }  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
  
    }
}

