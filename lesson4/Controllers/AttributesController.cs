using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Net;
using lesson4.Services;
namespace lesson4.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AttributeController(IAttributeService AttributeService) : ControllerBase
{
    [HttpGet]
    public async Task<List<Attribute>> GetAttributesAsync()
    {
        return await AttributeService.GetAttributesAsync();
    }

    [HttpPost]
    public async Task<Response<string>> AddAsync(Attribute Attribute)
    {
        return await AttributeService.AddAttributeAsync(Attribute);
    }

    [HttpGet("{AttributeId}")]
    public async Task<Response<Attribute>> GetAttributeSubcategories(int AttributeId)
    {
        return await AttributeService.GetAttributeByIdAsync(AttributeId);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(Attribute Attribute)
    {
        return await AttributeService.UpdateAsync(Attribute);
    }
    [HttpDelete("{AttributeId}")]
    public async Task<Response<string>> DeleteAsync(int AttributeId)
    {
        return await AttributeService.DeleteAsync(AttributeId);
    }
}