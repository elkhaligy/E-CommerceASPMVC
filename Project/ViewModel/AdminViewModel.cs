namespace Project.ViewModel
{
    public class AdminViewModel
    {
         public int Id { get; set; }

        // User Info (could be linked with ASP.NET Identity)
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
    }
}
