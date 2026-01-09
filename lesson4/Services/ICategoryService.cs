public interface ICategoryService
{
    Task<Response<string>> AddCategoryAsync(Category category);
    Task<List<Category>> GetCategoriesAsync();
    Task<Response<Category>> GetCategoryByIdAsync(int categoryId);
    Task<Response<string>> UpdateAsync(Category category);
    Task<Response<string>> DeleteAsync(int categoryId);
}