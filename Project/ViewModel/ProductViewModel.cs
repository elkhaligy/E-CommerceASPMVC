using Project.DTO;
using Project.Models;

namespace Project.ViewModel
{
    public class ProductViewModel
    {
        public int CurrentPage { get; set; }
        public PagedResult<Product> PagedProducts { get; set; }
        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
        public IEnumerable<Brand>? Brands { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int? SelectedBrandId { get; set; }
        public string? SearchTerm { get; set; }
        public string? SortOrder { get; set; }
        public int? PageNumber { get; set; }
        public int? TotalItems { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

    }
}
