using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
public class CategoryAttributeService(ApplicationDBContext applicationDbContext): ICategoryAttributeService
{
    private readonly ApplicationDBContext _dbContext = applicationDbContext;

    public async Task<Response<string>> AddCategoryAttributeAsync(CategoryAttribute categoryAttribute)
    {
        using var conn = _dbContext.Connection();
        var query = "insert into category_attributes(category_id,attribute_id) values(@cId, @aId)";
        var res = await conn.ExecuteAsync(query, new {cId = categoryAttribute.CategoryId,aId=categoryAttribute.AttributeId});
        return res==0
        ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong!")
        : new Response<string>(HttpStatusCode.OK, "Category attribute added successfully!");
    }

    public async Task<Response<string>> DeleteAsync(int categoryAttributeId)
    {
        try{
        using var context = _dbContext.Connection();
        var query = "delete from category_attributes where id = @id";
        var res = await context.ExecuteAsync(query,new{id=categoryAttributeId});
        return res==0
            ?new Response<string>(HttpStatusCode.InternalServerError, "Category attribute data not deleted!")
            :new Response<string>(HttpStatusCode.OK, "Category attribute data successfully deleted!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<CategoryAttribute>> GetCategoryAttributeByIdAsync(int categoryAttributeId)
    {
        try{
        using var conn = _dbContext.Connection();
        var query = "select * from category_attributes where id = @id";
        var result = await conn.QueryFirstOrDefaultAsync<CategoryAttribute>(query, new{id=categoryAttributeId});
        return result==null
                ?new Response<CategoryAttribute>(HttpStatusCode.InternalServerError, "Category Attribute not found!")
                :new Response<CategoryAttribute>(HttpStatusCode.OK, "Category Attribute found!", result);
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<CategoryAttribute>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<List<CategoryAttribute>> GetCategoryAttributesAsync()
    {
        using var conn = _dbContext.Connection();
        var query = "select * from category_attributes";
        var res = await conn.QueryAsync<CategoryAttribute>(query);
        return res.ToList();
    }

    public async Task<Response<string>> UpdateAsync(CategoryAttribute categoryAttribute)
    {
        try
        {
            using var context = _dbContext.Connection();
            var query = "update category_attributes set category_id = @cId,attribute_id = @aId where id = @id";
            var result = await context.ExecuteAsync(query, new{cId = categoryAttribute.CategoryId,aId=categoryAttribute.AttributeId,id = categoryAttribute.Id});
            return result==0
                ?new Response<string>(HttpStatusCode.InternalServerError, "Category attribute data not updated!")
                :new Response<string>(HttpStatusCode.OK, "Category attribute data successfully updated!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}