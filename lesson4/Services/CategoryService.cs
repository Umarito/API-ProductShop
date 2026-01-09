using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
namespace lesson4.Services;
public class CategoryService(ApplicationDBContext applicationDbContext): ICategoryService
{
    private readonly ApplicationDBContext _dbContext = applicationDbContext;

    public async Task<Response<string>> AddCategoryAsync(Category category)
    {
        using var conn = _dbContext.Connection();
        var query = "insert into categories(name, parent_category_id) values(@name, @parentcategoryid)";
        var res = await conn.ExecuteAsync(query, new {name = category.Name, parentcategoryid=category.ParentCategoryId});
        return res==0
        ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong!")
        : new Response<string>(HttpStatusCode.OK, "Category added successfully!");
    }

    public async Task<Response<string>> DeleteAsync(int categoryId)
    {
        try{
        using var context = _dbContext.Connection();
        var query = "delete from categories where id = @id";
        var res = await context.ExecuteAsync(query,new{id=categoryId});
        return res==0
            ?new Response<string>(HttpStatusCode.InternalServerError, "Category data not deleted!")
            :new Response<string>(HttpStatusCode.OK, "Category data successfully deleted!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<Category>> GetCategoryByIdAsync(int categoryId)
    {
        using var conn = _dbContext.Connection();
        var query = "select * from categories where parent_category_id=@parentcategoryid";
        var res = await conn.QueryAsync<Category>(query, new{parentcategoryid=categoryId});
        return new Response<Category>(HttpStatusCode.OK, "The data: ", res.ToList());
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        using var conn = _dbContext.Connection();
        var query = "select * from categories";
        var res = await conn.QueryAsync<Category>(query);
        return res.ToList();
    }

    public async Task<Response<string>> UpdateAsync(Category category)
    {
        try
        {
            using var context = _dbContext.Connection();
            var query = "update categories set name = @name,parent_category_id = @pId where id = @id";
            var result = await context.ExecuteAsync(query, new{name = category.Name,pId = category.ParentCategoryId,id = category.Id});
            return result==0
                ?new Response<string>(HttpStatusCode.InternalServerError, "Category data not updated!")
                :new Response<string>(HttpStatusCode.OK, "Category data successfully updated!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}