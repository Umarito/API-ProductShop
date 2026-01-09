using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Net;
using lesson4.Services;
namespace lesson4.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoryAttributeController(ICategoryAttributeService categoryAttributeService) : ControllerBase
{
    [HttpGet]
    public async Task<List<CategoryAttribute>> GetCategoryAttributesAsync()
    {
        return await categoryAttributeService.GetCategoryAttributesAsync();
    }

    [HttpPost]
    public async Task<Response<string>> AddAsync(CategoryAttribute categoryAttribute)
    {
        return await categoryAttributeService.AddCategoryAttributeAsync(categoryAttribute);
    }

    [HttpGet("{categoryAttributeId}")]
    public async Task<Response<CategoryAttribute>> GetCategoryAttributeSubcategories(int categoryAttributeId)
    {
        return await categoryAttributeService.GetCategoryAttributeByIdAsync(categoryAttributeId);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(CategoryAttribute categoryAttribute)
    {
        return await categoryAttributeService.UpdateAsync(categoryAttribute);
    }
    [HttpDelete("{categoryAttributeId}")]
    public async Task<Response<string>> DeleteAsync(int categoryAttributeId)
    {
        return await categoryAttributeService.DeleteAsync(categoryAttributeId);
    }
}