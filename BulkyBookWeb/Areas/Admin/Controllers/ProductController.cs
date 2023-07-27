using BulkyBook.DataAccess.Repositories.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _db;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(IUnitOfWork database, IWebHostEnvironment webHostEnvironment)
    {
        _db = database;
        _webHostEnvironment = webHostEnvironment;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Upsert(int? id)
    {
        var viewModel = new UpsertVM
        {
            Product = new Product(),
            CategoryList = _db.CategoryRepository.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }),
            CoverTypeList = _db.CoverTypeRepository.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
        };

        if (id is null || id == 0)
        {
            return View(viewModel);

        }
        else
        {
            var product = _db.ProductRepository.GetFirstOrDefault(p => p.Id == id);
            viewModel.Product = product;
        }

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(UpsertVM viewModel, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                var fileName =  Guid.NewGuid().ToString();
                var uploadPath = Path.Combine(wwwRootPath, @"images\products");
                var extension = Path.GetExtension(file.FileName);

                if (viewModel.Product.ImageUrl != null) 
                {
                    var pathToOldPhoto = Path.Combine(wwwRootPath, viewModel.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(pathToOldPhoto))
                    {
                        System.IO.File.Delete(pathToOldPhoto);
                    }
                }

                using(var stream = new FileStream(Path.Combine(uploadPath, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                viewModel.Product.ImageUrl = @"\images\products\" + fileName + extension;
            }

            if (viewModel.Product.Id == 0)
            {
                _db.ProductRepository.Add(viewModel.Product);
            }
            else
            {
                _db.ProductRepository.Update(viewModel.Product);
            }

            TempData["success"] = "Product created successfully!";
            _db.Save();

            return RedirectToAction(nameof(Index));
        }

        return View(viewModel);
    }

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _db.ProductRepository.GetAll("Category, CoverType");
        return Json(new { data = products });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var product = _db.ProductRepository.GetFirstOrDefault(c => c.Id == id);

        if (product is null)
            return Json(new { success = false, message = "Product not found." });

        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\')); // path of image to delete
        if (System.IO.File.Exists(imagePath))
        {
            System.IO.File.Delete(imagePath);
        }

        _db.ProductRepository.Remove(product);
        _db.Save();

        return Json(new { success = true, message = "Delete successful." });
    }
    #endregion
}
