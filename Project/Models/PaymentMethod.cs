namespace Project.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public required string Name { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }

    }
}
