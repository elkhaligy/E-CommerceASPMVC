namespace Project.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }

        public ICollection<ProductTag> ProductTags { get; set; }
        public ICollection<Product> Products { get; set; }
    }

}
