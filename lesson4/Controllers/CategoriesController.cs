using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Net;
using lesson4.Services;
namespace lesson4.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await categoryService.GetCategoriesAsync();
    }

    [HttpPost]
    public async Task<Response<string>> AddAsync(Category category)
    {
        return await categoryService.AddCategoryAsync(category);
    }

    [HttpGet("{categoryId}")]
    public async Task<Response<Category>> GetCategorySubcategories(int categoryId)
    {
        return await categoryService.GetCategoryByIdAsync(categoryId);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(Category category)
    {
        return await categoryService.UpdateAsync(category);
    }
    [HttpDelete("{categoryId}")]
    public async Task<Response<string>> DeleteAsync(int categoryId)
    {
        return await categoryService.DeleteAsync(categoryId);
    }
}