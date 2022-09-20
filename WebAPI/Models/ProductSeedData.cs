using System.Collections.Generic;
using System.Linq;
using WebAPI.Models.Entities;

namespace WebAPI.Models
{
  public static class ProductSeedData
  {
    public static void PopulateWebApiData(WebAPIDbContext context)
    {
      if(!context.Products.Any())
      {
        context.Products.AddRange(
        new Product { Name="Kayak", Description="A boat for one person", Price=275.00m, Category="Watersports"},
        new Product { Name="Unsteady Chair", Description="Secretly give your opponent a disadvantage", Price=29.95m, Category="Chess"},
        new Product { Name="Lifejacket", Description="Protective and fashionable", Price=48.95m, Category="Watersports"},
        new Product { Name="Soccer ball", Description="FIFA-approved size and weight", Price=19.50m, Category="Soccer"},
        new Product { Name="Spalding Ball", Description="NBA official Basketball", Price=160.00m, Category="Basketball"},
        new Product { Name="Corner flags", Description="Give your playing field that professional touch", Price=34.95m, Category="Soccer"},
        new Product { Name="Stadium", Description="Flat-packed 35,000-seat stadium", Price=79500.00m, Category="Soccer"},
        new Product { Name="Thinking cap", Description="Improve your brain efficiency by 75%", Price=16.00m, Category="Chess"},
        new Product { Name="Ring Net", Description="NBA size ring nets", Price=60.00m, Category="Basketball"},
        new Product { Name="Human Chess", Description="A fun game for the whole family", Price=75.00m, Category="Chess"},
        new Product { Name="Bling-bling King", Description="Gold-plated, diamond-studded King", Price=1200.00m, Category="Chess"},
        new Product { Name="Dark Night", Description="Titanium-plated Knight", Price=800.00m, Category="Chess"},
        new Product { Name="Shoe", Description="Studded shoes", Price=950.00m, Category="Soccer"},
        new Product { Name="Basketball Boards", Description="Full size NBA size Boards", Price=2160.00m, Category="Basketball"},
        new Product { Name="Jersey", Description="Air Jersey", Price=1200.00m, Category="Soccer"},
        new Product { Name="Scooter", Description="A water bike for one or two people", Price=4275.00m, Category="Watersports"},
        new Product { Name="Fox 40 whistle", Description="NBA Referres Whistel", Price=160.00m, Category="Basketball"},
        new Product { Name="Surfboard", Description="Surfboard for surfing on water", Price=495.00m, Category="Watersports"}
        );
      }
      context.SaveChanges();
    
    }
  }
}