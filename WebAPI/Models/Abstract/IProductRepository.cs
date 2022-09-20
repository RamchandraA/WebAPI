using System.Collections.Generic;
using System.Threading.Tasks;

using WebAPI.Models.Entities;

namespace WebAPI.Models.Abstract
{
  public interface IProductRepository
  {
    Task<Product> AddProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task<bool> RemoveProductAsync(int productId);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(int productId);
    Task<IEnumerable<Product>> GetProductByCategoryAsync(string category);
  }
}