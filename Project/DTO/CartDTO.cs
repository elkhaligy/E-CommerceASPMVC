namespace Project.DTO
{
    public class CartDTO
    {
        public int CartId { get; set; }
        public virtual ICollection<CartItemDTO>? CartItems { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
