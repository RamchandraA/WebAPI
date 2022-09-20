using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Entities;

namespace WebAPI.Models
{
  public class WebAPIDbContext : DbContext
  {
    public WebAPIDbContext(DbContextOptions<WebAPIDbContext> dbContextOptions) : base (dbContextOptions) {}

    public DbSet<Product> Products {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }
  }
}