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
public class ParentCategoryController : Controller
{
    private IValidator<ParentCategory> _parentCategoryValidator { get; set; }

    private readonly UserContext _dbContext;

    public ParentCategoryController(UserContext context, IValidator<ParentCategory> parentCategoryValidator)
    {
        _dbContext = context;
        _parentCategoryValidator = parentCategoryValidator;
    }

    public async Task<IActionResult> Index()
    {
        var parentCategories = await _dbContext.ParentCategories.ToListAsync();
        return View(parentCategories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ParentCategory? parentCategory)
    {
        ModelState.Clear();

        var result = await _parentCategoryValidator.ValidateAsync(parentCategory);

        if (result.IsValid)
        {
            await _dbContext.ParentCategories.AddAsync(parentCategory);
            await _dbContext.SaveChangesAsync();
            TempData["success"] = "Parent category created successfully";
            return RedirectToAction("Index", "ParentCategory");
        }
        result.AddToModelState(ModelState);

        return View(parentCategory);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var parentCategory = await _dbContext.ParentCategories.FindAsync(id);

        if (parentCategory == null) return NotFound();

        return View(parentCategory);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ParentCategory parentCategory)
    {
        ModelState.Clear();
        var result = await _parentCategoryValidator.ValidateAsync(parentCategory);

        var parentCategoryFind = _dbContext.ParentCategories.AsNoTracking().FirstOrDefault(pc => pc.Name == parentCategory.Name);
        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return View(parentCategory);
        }
        _dbContext.ParentCategories.Update(parentCategory);
        await _dbContext.SaveChangesAsync();
        TempData["success"] = "Parent category created successfully";
        return RedirectToAction("Index", "ParentCategory");
    }


    public async Task<IActionResult> Delete(int id)
    {
        var parentCategory = await _dbContext!.ParentCategories!.FindAsync(id);

        if (parentCategory == null) return NotFound();

        return View(parentCategory);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        var parentCategory = await _dbContext!.ParentCategories!.FindAsync(id);

        if (parentCategory == null) return NotFound();

        _dbContext.ParentCategories.Remove(parentCategory);
        await _dbContext.SaveChangesAsync();
        TempData["success"] = "Parent category was deleted successfully";

        return RedirectToAction("Index");
    }
}