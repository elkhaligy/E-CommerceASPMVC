namespace Project.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public required string Name { get; set; }

        public virtual ICollection<ProductTag>? ProductTags { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }

}
