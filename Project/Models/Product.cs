using Project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    public int ProductId { get; set; }
    //[Required(ErrorMessage ="You should add product name")]
    //[StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]

    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public int AdminId { get; set; }
    public Admin? Admin { get; set; }
    public Category? Category { get; set; }
    public Brand? Brand { get; set; }
    public ICollection<Review>? Reviews { get; set; }
    public ICollection<ProductTag>? ProductTags { get; set; }
    public ICollection<ProductImage>? Images { get; set; }
    public ICollection<CartItem>? CartItems { get; set; }
    public ICollection<OrderItem>? Items { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    [NotMapped]
    public required List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();

}
