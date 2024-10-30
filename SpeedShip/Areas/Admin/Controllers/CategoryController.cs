using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpeedShip.DataAccess.Database;
using SpeedShip.DataAccess.Repository;
using SpeedShip.Model.Models;

namespace SpeedShip.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
			_categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryRepository.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            var existingCategory = await _categoryRepository.Get(c => c.CategoryName == category.CategoryName).FirstOrDefaultAsync();

            if (existingCategory != null)
                ModelState.AddModelError("CategoryName", "Category already exist");

            if (!ModelState.IsValid)
                return View();

            category.CreatedBy = "Admin";
            category.DateCreated = DateTime.UtcNow;
			_categoryRepository.Create(category);
			await _categoryRepository.SaveAsync();
            TempData["Created"] = "Successfully created category " + category.CategoryName;
            return RedirectToAction("Index", "Category");
        }

        public async Task<IActionResult> Update(long categoryId)
        {
            var existingCategory = await _categoryRepository.Get(c => c.CategoryId == categoryId).FirstOrDefaultAsync();

			return View(existingCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Category category)
        {
			var existingCategory = await _categoryRepository.Get(c => c.CategoryName == category.CategoryName && c.CategoryId != category.CategoryId).FirstOrDefaultAsync();

			if (existingCategory != null)
                ModelState.AddModelError("CategoryName", "Category already exist");

            if (!ModelState.IsValid)
                return View(category);

            category.UpdatedBy = "Admin";
            category.DateUpdated = DateTime.UtcNow;
			_categoryRepository.Update(category);
			await _categoryRepository.SaveAsync();
            TempData["Updated"] = "Successfully updated category with id " + category.CategoryId;
            return RedirectToAction("Index", "Category");
        }

        public async Task<IActionResult> Delete(long categoryId)
        {
            var existingCategory = await _categoryRepository.Get(c => c.CategoryId == categoryId).FirstOrDefaultAsync();
			return View(existingCategory);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCategory(long categoryId)
        {
            var existingCategory = await _categoryRepository.Get(c => c.CategoryId == categoryId).FirstOrDefaultAsync();

			if (existingCategory == null)
                return RedirectToAction("Index", "Category");

			_categoryRepository.Delete(existingCategory);
			await _categoryRepository.SaveAsync();
            TempData["Deleted"] = "Successfully deleted category " + existingCategory.CategoryName;
            return RedirectToAction("Index", "Category");
        }
    }
}
