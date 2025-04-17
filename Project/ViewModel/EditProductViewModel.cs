using Project.Models;

namespace Project.ViewModel
{
    public class EditProductViewModel
    {
        public Product? Product { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
        public IEnumerable<Brand>? Brands { get; set; }
        public IEnumerable<Admin>? Admins { get; set; }
    }
} 