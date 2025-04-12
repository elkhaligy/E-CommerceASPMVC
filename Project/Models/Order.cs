namespace Project.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; } 
        public int AddressId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }    
        public int StatusId { get; set; }
        public int PaymentId { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public Payment? Payment { get; set; }
        public OrderStatus? Status { get; set; }
        public Customer? Customer { get; set; }
    }
}
