public class ProductAttribute : BaseEntity
{
    public int ProductId {get; set;}
    public int AttributeId {get; set;}
    public string Value{get; set;}=null!;
}