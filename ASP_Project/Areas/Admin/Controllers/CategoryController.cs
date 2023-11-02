using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Extensions;
using ASP_Project.Areas.Identity.Data.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Project.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    private IValidator<Category> _categoryValidator { get; set; }

    private readonly UserContext _dbContext;

    public CategoryController(UserContext context, IValidator<Category> validator)
    {
        _dbContext = context;
        _categoryValidator = validator;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _dbContext.Categories.ToListAsync();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category? category)
    {
        ModelState.Clear();
        var result = await _categoryValidator.ValidateAsync(category);

        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return View(category);
        }
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
        TempData["success"] = "Category created succsessfully";
        return RedirectToAction("Index", "Category");
    }


    public async Task<IActionResult> Edit(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);

        if (category == null) return NotFound();

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Category category)
    {
        ModelState.Clear();
        var result = await _categoryValidator.ValidateAsync(category);

        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return View(category);
        }
        var existingCategory = _dbContext.Categories.FirstOrDefault(c => c.Id == category.Id); // Find the existing category by its key value

        if (existingCategory == null) return NotFound();

        // Update the properties of the existing entity
        existingCategory.Name = category.Name; // Update other properties as needed

        await _dbContext.SaveChangesAsync(); // If you have navigation properties, you can update them as well.Save changes

        TempData["success"] = "Category updated successfully";
        return RedirectToAction("Index", "Category");
    }


    public async Task<IActionResult> Delete(int id)
    {
        var category = await _dbContext!.Categories!.FindAsync(id);

        if (category == null) return NotFound();

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        var category = await _dbContext!.Categories!.FindAsync(id);

        if (category == null) return NotFound();

        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();
        TempData["success"] = "Category was deleted succsessfully";

        return RedirectToAction("Index");
    }
}