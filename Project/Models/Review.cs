﻿namespace Project.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int Rating { get; set; }
        public required string Comment {  get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
