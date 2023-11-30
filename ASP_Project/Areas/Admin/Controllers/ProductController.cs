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
public class ProductController : Controller
{
	private IValidator<Product> _productValidator { get; set; }

	private readonly UserContext _dbContext;

	public ProductController(UserContext context, IValidator<Product> validator)
	{
		_dbContext = context;
		_productValidator = validator;
	}

	public async Task<IActionResult> Index()
	{
		var products = await _dbContext.Products
			.Include(v => v.ProductImages)
			.Include(p => p.Category).ToListAsync();
		return View(products);
	}

	public IActionResult Create()
	{
		return View();
	}
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(Product? product, IFormFileCollection ProductImages)
	{
		ModelState.Clear();

		List<ProductImage> productImages = new List<ProductImage>();
		foreach (var file in ProductImages)
		{
			if (file != null && file.Length > 0)
			{
				var productImage = new ProductImage();

				using (var memoryStream = new MemoryStream())
				{
					await file.CopyToAsync(memoryStream);
					var imageBytes = memoryStream.ToArray();
					productImage = new ProductImage
					{
						ImageData = imageBytes,
					};
					productImages.Add(productImage);
				}
			}
			else return NotFound();
		}

		product.ProductImages = productImages;

		var result = await _productValidator.ValidateAsync(product);

		if (!result.IsValid)
		{
			result.AddToModelState(ModelState);  
			return View(product);
		}

		await _dbContext.Products.AddAsync(product);
		await _dbContext.SaveChangesAsync();

		TempData["success"] = "Product created successfully";
		return RedirectToAction("Index", "Product");
	}

	public async Task<IActionResult> Edit(int id)
	{
		var product = await _dbContext.Products.FindAsync(id);

		if (product == null) return NotFound();
		product.ProductImages = await _dbContext.ProductImages.Where(pi => pi.ProductId == id).ToListAsync();

		return View(product);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(Product product, IFormFileCollection ProductImages)
	{
		ModelState.Clear();

		List<ProductImage> productImages = new();
		foreach (var file in ProductImages)
		{
			if (file != null && file.Length > 0)
			{
				var productImage = new ProductImage();

				using (var memoryStream = new MemoryStream())
				{
					await file.CopyToAsync(memoryStream);
					var imageBytes = memoryStream.ToArray();
					productImage.ImageData = imageBytes;
					productImages.Add(productImage);
				}
			}
		}
		product.ProductImages = _dbContext.ProductImages.Where(pi => pi.ProductId == product.Id).ToList();

		var result = await _productValidator.ValidateAsync(product);

		if (!result.IsValid)
		{
			result.AddToModelState(ModelState);
			foreach (var image in product.ProductImages)
			{
				_dbContext.ProductImages.Remove(image);
			}
			product.ProductImages.Clear();
			return View(product);
		}

		if (ProductImages.Count() == 0) return RedirectToAction("Index", "Product");

		foreach (var image in product.ProductImages)
		{
			_dbContext.ProductImages.Remove(image);
		}

		product.ProductImages.Clear();

		await _dbContext.SaveChangesAsync();

		product.ProductImages = productImages;

		_dbContext.Products.Update(product);
		await _dbContext.SaveChangesAsync();

		TempData["success"] = "Product updated successfully";
		return RedirectToAction("Index", "Product");
	}


	public async Task<IActionResult> Delete(int id)
	{
		var product = await _dbContext.Products.FindAsync(id);

		if (product == null) return NotFound();
		product.ProductImages = await _dbContext.ProductImages.Where(pi => pi.ProductId == id).ToListAsync();

		return View(product);
	}

	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeletePost(int id)
	{
		var product = await _dbContext!.Products!.FindAsync(id);

		if (product == null) return NotFound();
		_dbContext.Products.Remove(product);
		await _dbContext.SaveChangesAsync();
		TempData["success"] = "Product was deleted succsessfully";

		return RedirectToAction("Index");
	}
}