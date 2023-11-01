using BookStore.DataAccess.Data;
using BookStore.DataAccess.Interfaces;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineBookStore.Areas.Admin.Controllers.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.Product.GetAll().ToList();
            //IQueryable<Product> categories = _db.Categories.AsQueryable();
            return View(products);
        }
        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.CategoryId.ToString()
            //});

            //ViewData["CategoryList"] = categoryList;
            //ViewBag.CategoryList = categoryList;
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.CategoryId.ToString()
                }),
                Product = new Product()
            };
            if(id == null || id==0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.ProductId == id);
                return View(productVM); 
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("Name", "The Display Order can not be same");
            //}
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        string oldImgPath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImgPath))
                        { 
                           System.IO.File.Delete(oldImgPath);  
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.Product.ImageUrl = @"\images\product\" + fileName;
                }
                if (obj.Product.ProductId == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                    TempData["success"] = "Product created successfully";
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                    TempData["success"] = "Product updated successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                //ViewData["CategoryList"] = categoryList;
                //ViewBag.CategoryList = categoryList;
                obj.CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.CategoryId.ToString()
                });
                return View(obj);
            }
        }
        //public IActionResult Create()
        //{
        //    IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
        //    {
        //        Text = i.Name,
        //        Value = i.CategoryId.ToString()
        //    });
        //    //ViewData["CategoryList"] = categoryList;
        //    //ViewBag.CategoryList = categoryList;
        //    ProductVM productVM = new()
        //    {
        //        CategoryList = categoryList,
        //        Product = new Product()
        //    };
        //    return View(productVM);
        //}
        //[HttpPost]
        //public IActionResult Create(ProductVM obj)
        //{
        //    //if (obj.Name == obj.DisplayOrder.ToString())
        //    //{
        //    //    ModelState.AddModelError("Name", "The Display Order can not be same");
        //    //}
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.Add(obj.Product);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Product created successfully";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        //ViewData["CategoryList"] = categoryList;
        //        //ViewBag.CategoryList = categoryList;
        //        obj.CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
        //        {
        //            Text = i.Name,
        //            Value = i.CategoryId.ToString()
        //        });
        //        return View(obj);
        //    }
        //}

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product Product = _unitOfWork.Product.Get(u => u.ProductId == id);
        //    return View(Product);
        //}
        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{
        //    //if (obj.Name == obj.DisplayOrder.ToString())
        //    //{
        //    //    ModelState.AddModelError("Name", "The Display Order can not be same");
        //    //}
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.Update(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Product updated successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.ProductId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}