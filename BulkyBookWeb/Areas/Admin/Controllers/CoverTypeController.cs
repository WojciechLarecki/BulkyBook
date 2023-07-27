using BulkyBook.DataAccess.Repositories.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class CoverTypeController : Controller
{
    readonly IUnitOfWork _db;
    public CoverTypeController(IUnitOfWork unit)
    {
        _db = unit;
    }
    public IActionResult Index()
    {
        var coverTypes = _db.CoverTypeRepository.GetAll();
        return View(coverTypes);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType coverType)
    {
        if (ModelState.IsValid)
        {
            _db.CoverTypeRepository.Add(coverType);
            TempData["success"] = "Cover type has been created!";
            _db.Save();

            return RedirectToAction(nameof(Index));
        }

        return View(coverType);
    }

    public IActionResult Edit(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var coverType = _db.CoverTypeRepository.GetFirstOrDefault(c => c.Id == id);

        if (coverType is null)
            return NotFound();

        return View(coverType);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType coverType)
    {
        if (ModelState.IsValid)
        {
            _db.CoverTypeRepository.Update(coverType);
            TempData["success"] = "CoverType has been edited!";
            _db.Save();

            return RedirectToAction(nameof(Index));
        }

        return View(coverType);
    }

    public IActionResult Delete(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var category = _db.CoverTypeRepository.GetFirstOrDefault(c => c.Id == id);

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

        var coverType = _db.CoverTypeRepository.GetFirstOrDefault(c => c.Id == id);

        if (coverType is null)
            return NotFound();

        _db.CoverTypeRepository.Remove(coverType);
        TempData["success"] = "CoverType has been deleted!";
        _db.Save();

        return RedirectToAction(nameof(Index));
    }
}
