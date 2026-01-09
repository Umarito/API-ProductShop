public interface IProductAttributeService
{
    
    Task<Response<string>> AddProductAttributeAsync(ProductAttribute productAttribute);
    Task<List<ProductAttribute>> GetProductAttributesAsync();
    Task<Response<ProductAttribute>> GetProductAttributeByIdAsync(int productAttributeId);
    Task<Response<string>> UpdateAsync(ProductAttribute product);
    Task<Response<string>> DeleteAsync(int productAttributeId);
}