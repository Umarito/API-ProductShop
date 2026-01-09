public interface IProductService
{
    
    Task<Response<string>> AddProductAsync(Product product);
    Task<List<Product>> GetProductsAsync();
    Task<Response<Product>> GetProductByIdAsync(int productId);
    Task<Response<string>> UpdateAsync(Product product);
    Task<Response<string>> DeleteAsync(int productId);
}