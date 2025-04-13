namespace Project.Models
{
    public class ProductTag
    {
        public int ProductID { get; set; }
        public int TagId { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Tag? Tag { get; set; }
    }
}
