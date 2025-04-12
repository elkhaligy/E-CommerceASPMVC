namespace Project.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentMethodId { get; set; }
        public DateTime PaymentDate { get; set; }
        public Order? Order { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
    }
}
