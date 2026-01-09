using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Net;
using lesson4.Services;

namespace lesson4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductAttributesController(IProductAttributeService productAttributeService) : ControllerBase
{
    [HttpGet]
    public async Task<List<ProductAttribute>> GetProductAttributesAsync()
    {
        return await productAttributeService.GetProductAttributesAsync();
    }

    [HttpPost]
    public async Task<Response<string>> AddAsync(ProductAttribute productAttribute)
    {
        return await productAttributeService.AddProductAttributeAsync(productAttribute);
    }

    [HttpGet("{productAttributeId}")]
    public async Task<Response<ProductAttribute>> GetProductAttributeByIdAsync(int productAttributeId)
    {
        return await productAttributeService.GetProductAttributeByIdAsync(productAttributeId);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(ProductAttribute productAttribute)
    {
        return await productAttributeService.UpdateAsync(productAttribute);
    }

    [HttpDelete("{productAttributeId}")]
    public async Task<Response<string>> DeleteAsync(int productAttributeId)
    {
        return await productAttributeService.DeleteAsync(productAttributeId);
    }
}
