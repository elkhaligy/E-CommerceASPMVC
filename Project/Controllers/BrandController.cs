using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Contract;
using Project.Models;

namespace Project.Controllers
{   
    [Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<IActionResult> Index()
        {
            var brands = await _brandRepository.GetAllAsync();
            return View(brands);
        }

        public async Task<IActionResult> Details(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            return View(brand);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _brandRepository.Add(brand);
                await _brandRepository.SaveChangesAsync();
                TempData["SuccessMessage"] = "Brand created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Brand brand)
        {
            if (id != brand.BrandId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _brandRepository.Update(brand);
                await _brandRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            try
            {
                _brandRepository.Delete(brand);
                await _brandRepository.SaveChangesAsync();
                TempData["DeletedSuccessMessage"] = "Brand deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["DeletedFailureMessage"] = $"An error occurred while deleting the brand. Please try again. {ex.Message}";
                return RedirectToAction(nameof(Index));

            }

        }


    }
}
