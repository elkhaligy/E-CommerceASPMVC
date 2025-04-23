using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Contract;
using Project.DTO;
using Project.Models;
using Project.Services;
using Project.ViewModel;
using YourNamespace.Filters;

namespace Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly ApplicationContext _context;

        public ProductController(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IBrandRepository brandRepository,
            IAdminRepository adminRepository,
            ApplicationContext context)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _adminRepository = adminRepository;
            _context = context;
        }
        [AdminAuthorize]
        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            var categories = await _categoryRepository.GetAllAsync();
            var brands = await _brandRepository.GetAllAsync();
            var viewModel = new ProductViewModel
            {
                Products = products,
            Categories = categories,
                Brands = brands
            };
            return View(viewModel);
        }
        public async Task<IActionResult> GridView()
        {
            var products = await _productRepository.GetAllAsync();
            var categories = await _categoryRepository.GetAllAsync();
            var brands = await _brandRepository.GetAllAsync();
            var viewModel = new ProductViewModel
            {
                Products = products,
            Categories = categories,
                Brands = brands
            };
            return View(viewModel);
        }
        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            // I want to return the nice details page that will contain the product details and the images
            // So I need first to make the product details chtml page and make it take a model
            // the model it will take is actually the product itself
            // from there we the all the product details we will need 
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return View("NotFound404");
                }
                return View(product);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [AdminAuthorize]
        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateProductViewModel
            {
                Categories = await _categoryRepository.GetAllAsync(),
                Brands = await _brandRepository.GetAllAsync(),
                Admins = await _adminRepository.GetAllAsync()
            };
            return View(viewModel);
        }
        [AdminAuthorize]
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
                    Brands = await _brandRepository.GetAllAsync(),
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
                        Brands = await _brandRepository.GetAllAsync(),
                        Admins = await _adminRepository.GetAllAsync()
                    };
                    return View(viewModel);
                }

                _productRepository.Add(product);
                await _productRepository.SaveChangesAsync();    

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
                ModelState.AddModelError("", $"An error occurred while creating the product. Please try again. {ex.Message}");
                var viewModel = new CreateProductViewModel
                {
                    Product = product,
                    Categories = await _categoryRepository.GetAllAsync(),
                    Brands = await _brandRepository.GetAllAsync(),
                    Admins = await _adminRepository.GetAllAsync()
                };
                return View(viewModel);
            }
        }
        [AdminAuthorize]
        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new EditProductViewModel
            {
                Product = product,
                Categories = await _categoryRepository.GetAllAsync(),
                Brands = await _brandRepository.GetAllAsync(),
                Admins = await _adminRepository.GetAllAsync()
            };
            return View(viewModel);
        }
        [AdminAuthorize]
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
                    Brands = await _brandRepository.GetAllAsync(),
                    Admins = await _adminRepository.GetAllAsync()
                };
                return View(viewModel);
            }

            try
            {
                _productRepository.Update(product);
                await _productRepository.SaveChangesAsync();
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
                ModelState.AddModelError("", $"An error occurred while updating the product. Please try again. {ex.Message} ");
                var viewModel = new EditProductViewModel
                {
                    Product = product,
                    Categories = await _categoryRepository.GetAllAsync(),
                    Brands = await _brandRepository.GetAllAsync(),
                    Admins = await _adminRepository.GetAllAsync()
                };
                return View(viewModel);
            }
        }
        [AdminAuthorize]
        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                _productRepository.Delete(product);
                await _productRepository.SaveChangesAsync();
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
                TempData["ErrorMessage"] = $"An error occurred while deleting the product. Please try again. {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Product/Search
        public async Task<IActionResult> Search(string searchTerm)
        {

            var products = await _productRepository.SearchByNameAsync(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            var viewMode = new ProductViewModel
            {
                Products = products,
                Categories = await _categoryRepository.GetAllAsync(),
                Brands = await _brandRepository.GetAllAsync(),
            };
            return View("GridView", viewMode);
        }

        // GET: Product/Filter
        public async Task<IActionResult> Filter(int? categoryId, int? brandId)
        {
            var products = await _productRepository.FilterByCategoryAndBrandAsync(categoryId, brandId);
            var viewMode = new ProductViewModel
            {
                Products = products,
                Categories = await _categoryRepository.GetAllAsync(),
                Brands = await _brandRepository.GetAllAsync(),
                SelectedCategoryId = categoryId,
                SelectedBrandId = brandId
            };
            return View("Index", viewMode);
        }
    }
}
