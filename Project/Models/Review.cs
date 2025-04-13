namespace Project.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public int Rating { get; set; }
        public required string Comment {  get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
