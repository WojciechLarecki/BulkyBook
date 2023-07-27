using BulkyBook.DataAccess.Repositories.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _db;
    public CategoryController(IUnitOfWork database)
    {
        _db = database;
    }
    public IActionResult Index()
    {
        var categories = _db.CategoryRepository.GetAll();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("DisplayOrder", "Name is the same as display order.");
        }
        if (ModelState.IsValid)
        {
            _db.CategoryRepository.Add(category);
            TempData["success"] = "Temp data is here!";
            _db.Save();

            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    public IActionResult Edit(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var category = _db.CategoryRepository.GetFirstOrDefault(c => c.Id == id);

        if (category is null)
            return NotFound();

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("DisplayOrder", "Name is the same as display order.");
        }
        if (ModelState.IsValid)
        {
            _db.CategoryRepository.Update(category);
            TempData["success"] = "Temp data is here!";
            _db.Save();

            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    public IActionResult Delete(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var category = _db.CategoryRepository.GetFirstOrDefault(c => c.Id == id);

        if (category is null)
            return NotFound();

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var category = _db.CategoryRepository.GetFirstOrDefault(c => c.Id == id);

        if (category is null)
            return NotFound();

        _db.CategoryRepository.Remove(category);
        TempData["success"] = "Temp data is here!";
        _db.Save();

        return RedirectToAction(nameof(Index));
    }
}
