using Humanizer;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Project.Contract;
using Project.DTO;
using Project.Models;
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
        private readonly ApplicationContext _context;
        public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService, ApplicationContext context)
        {
            _productService = productService;
            _categoryService = categoryService;
            _BrandService = brandService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            var brands = await _BrandService.GetBrandsAsync();
            var ViewModel = new CreateProductViewModel
            {
                Categories = categories,
                Brands = brands,
            };
            return View("Add", ViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddProductAsync(product);
                foreach (var file in product.ImageFiles)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    var imageBytes = ms.ToArray();
                    var productImage = new ProductImage
                    {
                        ProductId = product.ProductId,
                        ImageData = imageBytes
                    };
                    await _context.ProductImages.AddAsync(productImage);
                }
                await _context.SaveChangesAsync();
                return Content("Product added successfully");

            }
            else
            {
                var categories = await _categoryService.GetCategoriesAsync();
                var brands = await _BrandService.GetBrandsAsync();
                var ViewModel = new CreateProductViewModel
                {
                    Categories = categories,
                    Brands = brands,
                    Product = product,
                };
                return View("Add", ViewModel);
            }
                //var dTO = new ProductCreateDTO
                //{
                //    Name = productViewModel.Product.Name,
                //    BrandId = productViewModel.Product.BrandId,
                //    CategoryId = productViewModel.Product.CategoryId,
                //    AdminId = productViewModel.Product.AdminId,
                //};

                await _productService.AddProductAsync(product);
            // I need to return product added successfully


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
