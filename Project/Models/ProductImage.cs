namespace Project.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        // The actual image data stored as byte array
        // public required byte[] ImageData { get; set; }


        // Computed property to convert byte array to Base64 string
        // public string? ImageBase64 => ImageData != null ? $"data:image/png;base64,{Convert.ToBase64String(ImageData)}" : null;

        // This is the image path on my server
        // It will be something like that /images/products/product1.jpg
        public required string ImagePath { get; set; }
    }
}
