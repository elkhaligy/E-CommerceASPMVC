namespace Project.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string PhoneNumber { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Address>? Addresses { get; set; }
        public Cart? Cart { get; set; }

    }
}
