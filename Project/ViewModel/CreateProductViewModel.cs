using Project.DTO;
using Project.Models;

namespace Project.ViewModel
{
    public class CreateProductViewModel
    {
        public IEnumerable<Category>? Categories { get; set; }
        public IEnumerable<Brand>? Brands { get; set; }
        public Product? Product { get; set; }
    }   
}
