using Microsoft.AspNetCore.Mvc;
using SpeedShip.DataAccess.Database;
using SpeedShip.Model.Models;

namespace SpeedShip.Controllers
{
	public class CategoryController : Controller
	{
		private readonly DbSpeedShipContext _dbSpeedShipContext;

		public CategoryController(DbSpeedShipContext dbSpeedShipContext)
		{
			_dbSpeedShipContext = dbSpeedShipContext;
		}

		public IActionResult Index()
		{
			return View(_dbSpeedShipContext.Categories.ToList());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category category)
		{
			Category existingCategory = _dbSpeedShipContext.Categories.FirstOrDefault(c => c.CategoryName == category.CategoryName);

			if (existingCategory != null)
				ModelState.AddModelError("CategoryName", "Category already exist");

			if (!ModelState.IsValid)
				return View();

			category.CreatedBy = "Admin";
			category.DateCreated = DateTime.UtcNow;
			_dbSpeedShipContext.Categories.Add(category);
			_dbSpeedShipContext.SaveChanges();
			return RedirectToAction("Index", "Category");
		}

		public IActionResult Update(long categoryId)
		{
			Category existingCategory = _dbSpeedShipContext.Categories.FirstOrDefault(c => c.CategoryId == categoryId);

			return View(existingCategory);
		}

		[HttpPost]
		public IActionResult Update(Category category)
		{
			Category existingCategory = _dbSpeedShipContext.Categories.FirstOrDefault(c => c.CategoryName == category.CategoryName && c.CategoryId != category.CategoryId);

			if (existingCategory != null)
				ModelState.AddModelError("CategoryName", "Category already exist");

			if (!ModelState.IsValid)
				return View(category);

			category.UpdatedBy = "Admin";
			category.DateUpdated = DateTime.UtcNow;
			_dbSpeedShipContext.Categories.Update(category);
			_dbSpeedShipContext.SaveChanges();
			return RedirectToAction("Index", "Category");
		}

		public IActionResult Delete(long categoryId)
		{
			Category existingCategory = _dbSpeedShipContext.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
			return View(existingCategory);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteCategory(long categoryId)
		{
			Category existingCategory = _dbSpeedShipContext.Categories.FirstOrDefault(c => c.CategoryId == categoryId);

			if (existingCategory == null)
				return RedirectToAction("Index", "Category");

			_dbSpeedShipContext.Categories.Remove(existingCategory);
			_dbSpeedShipContext.SaveChanges();
			return RedirectToAction("Index", "Category");
		}
	}
}
