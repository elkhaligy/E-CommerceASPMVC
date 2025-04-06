using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Project.Contract;
using Project.DTO;
using Project.Services;
using Project.ViewModel;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _BrandService;
        public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _BrandService = brandService;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            var brands= await _BrandService.GetBrandsAsync();
            var ViewModel = new CreateProductViewModel
            {
                Categories = categories,
                Brands = brands,
                Product = new ProductCreateDTO(),
            };
            return View("Add", ViewModel);
        }
        [HttpPost]
        public async Task <IActionResult> Add(CreateProductViewModel productViewModel)
        {
            var dTO = new ProductCreateDTO
            {
                Name=productViewModel.Product.Name,
                BrandId=productViewModel.Product.BrandId,
                CategoryId=productViewModel.Product.CategoryId,
                AdminId=productViewModel.Product.AdminId,
            };
          
                await _productService.AddProductAsync(dTO);
                return RedirectToAction("Show");

            
            // await _productService.AddProductAsync(dTO);

            // Redirect to the Show page after successfully adding the product
            //   return RedirectToAction("Show");
            //foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            //{
            //    Console.WriteLine(error.ErrorMessage);
            //}
            //var categories = await _categoryService.GetCategoriesAsync();
            //var brands = await _BrandService.GetBrandsAsync();
            //var ViewModel = new CreateProductViewModel
            //{
            //    Categories = categories,
            //    Brands = brands,
            //    Product = dTO,
            //};
            //return View(ViewModel);

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
