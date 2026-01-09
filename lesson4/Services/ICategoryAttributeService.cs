public interface ICategoryAttributeService
{
    
    Task<Response<string>> AddCategoryAttributeAsync(CategoryAttribute categoryAttribute);
    Task<List<CategoryAttribute>> GetCategoryAttributesAsync();
    Task<Response<CategoryAttribute>> GetCategoryAttributeByIdAsync(int categoryAttributeId);
    Task<Response<string>> UpdateAsync(CategoryAttribute categoryAttribute);
    Task<Response<string>> DeleteAsync(int categoryAttributeId);
}