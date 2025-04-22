namespace Project.Models
{
    public class Admin
    {
        public int Id { get; set; }

        // User Info (could be linked with ASP.NET Identity)
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        // Relationships
        public virtual ICollection<Product>? Products { get; set; }  // Admin manages products

        // You could also track actions performed by the admin, e.g., logs, etc.
        public virtual ICollection<AdminActionLog>? ActionLogs { get; set; }
    }

}
