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

			_dbSpeedShipContext.Categories.Add(category);
			_dbSpeedShipContext.SaveChanges();
			return RedirectToAction("Index","Category");
		}
	}
}
