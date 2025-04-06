using Project.DTO;

namespace Project.ViewModel
{
    public class CreateProductViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public ProductCreateDTO Product { get; set; }
    }
}
