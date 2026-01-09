public class Product: BaseEntity
{
    public string Name {get; set;}=null!;
    public decimal Price {get; set;}
    public string? Description {get; set;}
    public int CategoryId {get; set;}
    public bool IsDeleted {get; set;}=false;
}