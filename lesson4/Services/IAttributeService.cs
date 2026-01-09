public interface IAttributeService
{
    Task<Response<string>> AddAttributeAsync(Attribute attribute);
    Task<List<Attribute>> GetAttributesAsync();
    Task<Response<Attribute>> GetAttributeByIdAsync(int attributeId);
    Task<Response<string>> UpdateAsync(Attribute attribute);
    Task<Response<string>> DeleteAsync(int attributeId);
}