public class Brand
{
    public int BrandId { get; set; }
    public required string Name { get; set; }

    public virtual ICollection<Product>? Products { get; set; }
}
