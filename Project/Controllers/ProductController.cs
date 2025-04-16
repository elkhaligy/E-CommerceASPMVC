using Microsoft.AspNetCore.Mvc;
using Project.Contract;
using Project.DTO;
using Project.Models;
using Project.Services;
using Project.ViewModel;

namespace Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandService _brandService;
        private readonly IAdminRepository _adminRepository;
        private readonly ApplicationContext _context;

        public ProductController(
            IProductService productService,
            ICategoryRepository categoryRepository,
            IBrandService brandService,
            IAdminRepository adminRepository,
            ApplicationContext context)
        {
            _productService = productService;
            _categoryRepository = categoryRepository;
            _brandService = brandService;
            _adminRepository = adminRepository;
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateProductViewModel
            {
                Categories = await _categoryRepository.GetAllAsync(),
                Brands = await _brandService.GetAllBrandsAsync(),
                Admins = await _adminRepository.GetAllAsync()
            };
            return View(viewModel);
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CreateProductViewModel
                {
                    Product = product,
                    Categories = await _categoryRepository.GetAllAsync(),
                    Brands = await _brandService.GetAllBrandsAsync(),
                    Admins = await _adminRepository.GetAllAsync()
                };
                return View(viewModel);
            }

            try
            {
                if (product.ImageFiles == null || !product.ImageFiles.Any())
                {
                    ModelState.AddModelError("ImageFiles", "At least one image is required");
                    var viewModel = new CreateProductViewModel
                    {
                        Product = product,
                        Categories = await _categoryRepository.GetAllAsync(),
                        Brands = await _brandService.GetAllBrandsAsync(),
                        Admins = await _adminRepository.GetAllAsync()
                    };
                    return View(viewModel);
                }

                await _productService.AddProductAsync(product);

                // Handle image uploads
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
                    await _context.ProductImages.AddAsync(productImage); // This needs changing when we implement the productimages service
                }
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while creating the product. Please try again.");
                var viewModel = new CreateProductViewModel
                {
                    Product = product,
                    Categories = await _categoryRepository.GetAllAsync(),
                    Brands = await _brandService.GetAllBrandsAsync(),
                    Admins = await _adminRepository.GetAllAsync()
                };
                return View(viewModel);
            }
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new EditProductViewModel
            {
                Product = product,
                Categories = await _categoryRepository.GetAllAsync(),
                Brands = await _brandService.GetAllBrandsAsync(),
                Admins = await _adminRepository.GetAllAsync()
            };
            return View(viewModel);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, List<int> imagesToDelete)
        {
            if (id != product.ProductId) // This case can only happen if the user manipulates the form
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var viewModel = new EditProductViewModel
                {
                    Product = product,
                    Categories = await _categoryRepository.GetAllAsync(),
                    Brands = await _brandService.GetAllBrandsAsync(),
                    Admins = await _adminRepository.GetAllAsync()
                };
                return View(viewModel);
            }

            try
            {
                await _productService.UpdateProductAsync(product);
                // Handle images to delete
                // Check first if the image to delete list has data
                // iterate through it, you will find the image id
                // use the context to find the image with that specific id
                // remove that image from the db
                if (imagesToDelete != null && imagesToDelete.Any())
                {
                    foreach (var imageId in imagesToDelete)
                    {
                        var image = await _context.ProductImages.FindAsync(imageId);
                        if (image != null)
                        {
                            _context.ProductImages.Remove(image);
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                // Handle new image uploads if any
                if (product.ImageFiles != null && product.ImageFiles.Any())
                {
                    // Add new images
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
                }

                TempData["SuccessMessage"] = "Product updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the product. Please try again.");
                var viewModel = new EditProductViewModel
                {
                    Product = product,
                    Categories = await _categoryRepository.GetAllAsync(),
                    Brands = await _brandService.GetAllBrandsAsync(),
                    Admins = await _adminRepository.GetAllAsync()
                };
                return View(viewModel);
            }
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                await _productService.DeleteProductAsync(product);
                TempData["SuccessMessage"] = "Product deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the product.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Product/Search
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return RedirectToAction(nameof(Index));
            }

            var products = await _productService.SearchProductsByNameAsync(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View("Index", products);
        }
    }
}
