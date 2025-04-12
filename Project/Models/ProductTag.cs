namespace Project.Models
{
    public class ProductTag
    {
        public int ProductID { get; set; }
        public int TagId { get; set; }
        public required Product Product { get; set; }
        public required Tag Tag { get; set; }
    }
}
