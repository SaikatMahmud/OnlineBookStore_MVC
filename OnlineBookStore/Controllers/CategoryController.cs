using BookStore.DataAccess.Data;
using BookStore.DataAccess.Interfaces;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;


namespace OnlineBookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategory _category;
        public CategoryController(ICategory category)
        {
            _category = category;
        }
        public IActionResult Index()
        {
            List<Category> categories = _category.GetAll().ToList();
            //IQueryable<Category> categories = _db.Categories.AsQueryable();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("Name", "The Display Order can not be same");
            //}
            if (ModelState.IsValid)
            {
                _category.Add(obj);
                _category.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = _category.Get(u => u.CategoryId == id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("Name", "The Display Order can not be same");
            //}
            if (ModelState.IsValid)
            {
                _category.Update(obj);
                _category.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Category? obj = _category.Get(u => u.CategoryId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _category.Remove(obj);
            _category.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}