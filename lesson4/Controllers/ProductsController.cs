using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Net;
using lesson4.Services;

namespace lesson4.Controllers;

[ApiController]
[Route("lesson4/product/")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<List<Product>> GetProductsAsync()
    {
        return await productService.GetProductsAsync();
    }

    [HttpPost]
    public async Task<Response<string>> AddAsync(Product product)
    {
        return await productService.AddProductAsync(product);
    }

    [HttpGet("{productId}")]
    public async Task<Response<Product>> GetProductByIdAsync(int productId)
    {
        return await productService.GetProductByIdAsync(productId);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(Product product)
    {
        return await productService.UpdateAsync(product);
    }

    [HttpDelete("{productId}")]
    public async Task<Response<string>> DeleteAsync(int productId)
    {
        return await productService.DeleteAsync(productId);
    }
}
