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
    public virtual Admin? Admin { get; set; }
    public virtual Category? Category { get; set; }
    public virtual Brand? Brand { get; set; }
    public virtual ICollection<Review>? Reviews { get; set; }
    public virtual ICollection<ProductTag>? ProductTags { get; set; }
    public virtual ICollection<ProductImage>? Images { get; set; } = new List<ProductImage>();
    public virtual ICollection<CartItem>? CartItems { get; set; }
    public virtual ICollection<OrderItem>? Items { get; set; }
    public virtual ICollection<Tag>? Tags { get; set; }
    [NotMapped]
    public virtual List<IFormFile>? ImageFiles { get; set; } = new List<IFormFile>();

}
