public class Brand
{
    public int BrandId { get; set; }
    public required string Name { get; set; }

    public ICollection<Product>? Products { get; set; }
}
