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
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual OrderStatus? Status { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
