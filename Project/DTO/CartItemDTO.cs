namespace Project.DTO
{
    public class CartItemDTO
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImagePath { get; set; }
        public decimal Price { get; set; }
    }
}
