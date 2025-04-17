using Project.Models;

namespace Project.ViewModel
{
    public class ProductViewModel
    {
        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
        public IEnumerable<Brand>? Brands { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int? SelectedBrandId { get; set; }
    }
}
