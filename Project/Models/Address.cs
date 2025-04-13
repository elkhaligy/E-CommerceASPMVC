namespace Project.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int CustomerId   { get; set; }
        public required string AddressLine {  get; set; }    
        public required string City { get; set; }
        public required string State { get; set; }
        public required string PostalCode { get; set; }
        public required string Country { get; set; }
        public required string Phone { get; set; }
        public virtual Customer? Customer { get; set; }


    }
}
