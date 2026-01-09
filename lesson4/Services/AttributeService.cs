using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
public class AttributeService(ApplicationDBContext applicationDbContext): IAttributeService
{
    private readonly ApplicationDBContext _dbContext = applicationDbContext;

    public async Task<Response<string>> AddAttributeAsync(Attribute attribute)
    {
        using var conn = _dbContext.Connection();
        var query = "insert into attributes(name) values(@name)";
        var res = await conn.ExecuteAsync(query, new {name = attribute.Name});
        return res==0
        ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong!")
        : new Response<string>(HttpStatusCode.OK, "Attribute added successfully!");
    }

    public async Task<Response<string>> DeleteAsync(int attributeId)
    {
        try{
        using var context = _dbContext.Connection();
        var query = "delete from attributes where id = @id";
        var res = await context.ExecuteAsync(query,new{id=attributeId});
        return res==0
            ?new Response<string>(HttpStatusCode.InternalServerError, "Attribute data not deleted!")
            :new Response<string>(HttpStatusCode.OK, "Attribute data successfully deleted!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<Attribute>> GetAttributeByIdAsync(int attributeId)
    {
        try{
        using var conn = _dbContext.Connection();
        var query = "select * from attributes where id = @id";
        var result = await conn.QueryFirstOrDefaultAsync<Attribute>(query, new{id=attributeId});
        return result==null
                ?new Response<Attribute>(HttpStatusCode.InternalServerError, "Attribute not found!")
                :new Response<Attribute>(HttpStatusCode.OK, "Attribute found!", result);
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<Attribute>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<List<Attribute>> GetAttributesAsync()
    {
        using var conn = _dbContext.Connection();
        var query = "select * from attributes";
        var res = await conn.QueryAsync<Attribute>(query);
        return res.ToList();
    }

    public async Task<Response<string>> UpdateAsync(Attribute attribute)
    {
        try
        {
            using var context = _dbContext.Connection();
            var query = "update attributes set name = @name where id = @id";
            var result = await context.ExecuteAsync(query, new{name = attribute.Name,id = attribute.Id});
            return result==0
                ?new Response<string>(HttpStatusCode.InternalServerError, "Attribute data not updated!")
                :new Response<string>(HttpStatusCode.OK, "Attribute data successfully updated!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}