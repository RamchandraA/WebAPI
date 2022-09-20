using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using WebAPI.Models.Abstract;
using WebAPI.Models.Entities;

namespace WebAPI.Models.Concrete
{
  public class SqlProductRepository : IProductRepository
  {
    private readonly WebAPIDbContext _context;
    private readonly ILogger<SqlProductRepository> _logger;

    private void LogInfo(string message) => _logger.LogInformation($"--- {message} ---");
    private void LogError(string message) => _logger.LogError($"--- {message} ---");

    public SqlProductRepository(WebAPIDbContext context, ILogger<SqlProductRepository> logger)
    {
      _context = context;
      _logger = logger;
    }
    public async Task<Product> AddProductAsync(Product product)
    {
      //await _context.Products.AddAsync(product);
      _context.Products.Add(product);
      int recEffected = await _context.SaveChangesAsync();
      if (recEffected == 1)
      {
        LogInfo($"SqlProductRepository.AddProductAsync, New Product - Id: '{product.ProductId}', ProductName: '{product.Name}', Added Successfully");
        return product;
      }
      return null;
    }

    public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string category) => await _context.Products.Where(p => p.Category == category).ToListAsync();
    public async Task<Product> GetProductByIdAsync(int productId) => await _context.Products.FindAsync(productId);
    public async Task<IEnumerable<Product>> GetProductsAsync() => await _context.Products.ToListAsync();

    public async Task<bool> RemoveProductAsync(int productId)
    {
      var prod = await GetProductByIdAsync(productId);
      _context.Products.Remove(prod);
      int recEffected = await _context.SaveChangesAsync();
      if (recEffected == 1)
      {
        LogInfo($"SqlProductRepository.DeleteProductAsync, Product with - Id: '{prod.ProductId}', ProductName: '{prod.Name}', Deleted Successfully");
        return true;
      }
      return false;
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
      _context.Entry<Product>(product).State = EntityState.Modified;
      int recEffected = await _context.SaveChangesAsync();
      if (recEffected == 1)
      {
        LogInfo($"SqlProductRepository.UpdateProductAsync, Product with - Id: '{product.ProductId}', Updated Successfully");
      }
      return product;
    }
  }
}