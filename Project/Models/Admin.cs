namespace Project.Models
{
    public class Admin
    {
        public int Id { get; set; }

        // User Info (could be linked with ASP.NET Identity)
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // Relationships
        public ICollection<Product> Products { get; set; }  // Admin manages products

        // You could also track actions performed by the admin, e.g., logs, etc.
        public ICollection<AdminActionLog> ActionLogs { get; set; }
    }

}
