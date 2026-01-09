using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;

public class ProductAttributeService(ApplicationDBContext applicationDbContext) : IProductAttributeService
{
    private readonly ApplicationDBContext _dbContext = applicationDbContext;

    public async Task<Response<string>> AddProductAttributeAsync(ProductAttribute productAttribute)
    {
        using var conn = _dbContext.Connection();
        var query = "insert into product_attributes(product_id,attribute_id) values(@pId, @aId)";
        var res = await conn.ExecuteAsync(query, new { pId = productAttribute.ProductId, aId = productAttribute.AttributeId });
        return res == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong!")
            : new Response<string>(HttpStatusCode.OK, "Product attribute added successfully!");
    }

    public async Task<Response<string>> DeleteAsync(int productAttributeId)
    {
        try
        {
            using var context = _dbContext.Connection();
            var query = "delete from product_attributes where id = @id";
            var res = await context.ExecuteAsync(query, new { id = productAttributeId });
            return res == 0
                ? new Response<string>(HttpStatusCode.InternalServerError, "Product attribute data not deleted!")
                : new Response<string>(HttpStatusCode.OK, "Product attribute data successfully deleted!");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<ProductAttribute>> GetProductAttributeByIdAsync(int productAttributeId)
    {
        try
        {
            using var conn = _dbContext.Connection();
            var query = "select * from product_attributes where id = @id";
            var result = await conn.QueryFirstOrDefaultAsync<ProductAttribute>(query, new { id = productAttributeId });
            return result == null
                ? new Response<ProductAttribute>(HttpStatusCode.InternalServerError, "Product Attribute not found!")
                : new Response<ProductAttribute>(HttpStatusCode.OK, "Product Attribute found!", result);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<ProductAttribute>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<List<ProductAttribute>> GetProductAttributesAsync()
    {
        using var conn = _dbContext.Connection();
        var query = "select * from product_attributes";
        var res = await conn.QueryAsync<ProductAttribute>(query);
        return res.ToList();
    }

    public async Task<Response<string>> UpdateAsync(ProductAttribute productAttribute)
    {
        try
        {
            using var context = _dbContext.Connection();
            var query = "update product_attributes set product_id = @pId,attribute_id = @aId where id = @id";
            var result = await context.ExecuteAsync(query, new{pId = productAttribute.ProductId,aId = productAttribute.AttributeId,id = productAttribute.Id});
            return result == 0
                ? new Response<string>(HttpStatusCode.InternalServerError, "Product attribute data not updated!")
                : new Response<string>(HttpStatusCode.OK, "Product attribute data successfully updated!");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}
