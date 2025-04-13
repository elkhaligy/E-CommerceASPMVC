namespace Project.Models
{
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public string? Status { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
