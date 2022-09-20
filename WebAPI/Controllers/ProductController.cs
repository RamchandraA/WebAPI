using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Authorization;

using WebAPI.Models.Abstract;
using WebAPI.Models.Entities;

namespace WebAPI.Controllers
{
  [ApiController]
  [Produces("application/json")] // content-type: application/json
  [Route("api/[controller]")] // attribute based routing
  public class ProductController : ControllerBase
  {
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductController> _logger;

    private void LogInfo(string message) => _logger.LogInformation($"--- {message} ---");
    private void LogError(string message) => _logger.LogError($"--- {message} ---");
    public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
    {
      _productRepository = productRepository;
      _logger = logger;
      // LogInfo($"From ProductController Ctor");
    }

    [HttpGet, Route("")] //http(s)://localhost:500(0/1)/api/product
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Get()
    {
      var products = await _productRepository.GetProductsAsync();
      // LogInfo($"ProductController.Get(): {products.Count()}");
      if (products != null)
      {
        return Ok(products);
      }
      return NoContent();
    }

    // [HttpGet, Route("")] // This will cause a Ambiguous error
    [HttpGet, Route("id/{id}")] //http(s)://localhost:500(0/1)/api/product/id/2
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Get(int id)
    {
      var product = await _productRepository.GetProductByIdAsync(id);
      if (product != null)
      {
        return Ok(product);
      }
      return NoContent();
    }

    [HttpGet, Route("category/{category}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Get(string category)
    {
      var productsByCategoryList = await _productRepository.GetProductByCategoryAsync(category);
      if (productsByCategoryList != null)
      {
        return Ok(productsByCategoryList);
      }
      return BadRequest($"No Product found for the given Category: '{category}'");
    }


    [HttpPost, Route("")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
      var newProduct = await _productRepository.AddProductAsync(product);
      if (newProduct != null)
      {
        return Ok(newProduct);
      }
      return BadRequest($"New Product could not be created....");
    }

    [HttpPut, Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put([FromBody] Product product)
    {
      var updatedProduct = await _productRepository.UpdateProductAsync(product);
      if (updatedProduct != null)
      {
        return Ok(updatedProduct);
      }
      return BadRequest($"Could not update the Product....");
    }

    [HttpDelete, Route("id/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
      bool deleteFlag = await _productRepository.RemoveProductAsync(id);
      if (deleteFlag)
      {
        return Ok($"Product with the Id: '{id}', deleted successfully");
      }
      return BadRequest($"No Product exists with the Id: '{id}'");
    }

  }
}