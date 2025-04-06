namespace Project.Models
{
    public class PaymentMethod
    {

        public int PaymentMethodId { get; set; }

        public string Name { get; set; }
        public ICollection<Payment> Payments { get; set; }

    }
}
