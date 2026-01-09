using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;

public class ProductService(ApplicationDBContext applicationDbContext) : IProductService
{
    private readonly ApplicationDBContext _dbContext = applicationDbContext;

    public async Task<Response<string>> AddProductAsync(Product product)
    {
        using var conn = _dbContext.Connection();
        var query = "insert into products(name,price,description,category_id) values(@name,@price,@des,@cId)";
        var res = await conn.ExecuteAsync(query, new { cId = product.CategoryId,name = product.Name,des = product.Description,price = product.Price });
        return res == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong!")
            : new Response<string>(HttpStatusCode.OK, "Product added successfully!");
    }

    public async Task<Response<string>> DeleteAsync(int productId)
    {
        try
        {
            using var context = _dbContext.Connection();
            var query = "delete from products where id = @id";
            var res = await context.ExecuteAsync(query, new { id = productId });
            return res == 0
                ? new Response<string>(HttpStatusCode.InternalServerError, "Product data not deleted!")
                : new Response<string>(HttpStatusCode.OK, "Product data successfully deleted!");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<Product>> GetProductByIdAsync(int productId)
    {
        try
        {
            using var conn = _dbContext.Connection();
            var query = "select * from products where id = @id";
            var result = await conn.QueryFirstOrDefaultAsync<Product>(query, new { id = productId });
            return result == null
                ? new Response<Product>(HttpStatusCode.InternalServerError, "Product not found!")
                : new Response<Product>(HttpStatusCode.OK, "Product found!", result);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<Product>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        using var conn = _dbContext.Connection();
        var query = "select * from products";
        var res = await conn.QueryAsync<Product>(query);
        return res.ToList();
    }

    public async Task<Response<string>> UpdateAsync(Product product)
    {
        try
        {
            using var context = _dbContext.Connection();
            var query = "update products set category_id = @cId,name = @name,description = @des,price = @price where id = @id";
            var result = await context.ExecuteAsync(query, new{cId = product.CategoryId,name = product.Name,id = product.Id,des = product.Description,price = product.Price});
            return result == 0
                ? new Response<string>(HttpStatusCode.InternalServerError, "Product data not updated!")
                : new Response<string>(HttpStatusCode.OK, "Product data successfully updated!");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}
