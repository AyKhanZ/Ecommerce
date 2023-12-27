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

    public async Task<IActionResult> Index(int pg = 1, [FromQuery] ProductFilter filter = null)
    {
        if (filter == null) filter = new ProductFilter();

        IQueryable<Product> productsQuery = _dbContext.Products
            .Include(p => p.ProductImages);

        if (!string.IsNullOrEmpty(filter.SearchText)) productsQuery = productsQuery.Where(p => p.Name.Contains(filter.SearchText));

        if (filter.MinPrice != null) productsQuery = productsQuery.Where(p => p.Price >= filter.MinPrice);

        if (filter.MaxPrice != null) productsQuery = productsQuery.Where(p => p.Price <= filter.MaxPrice);

        if (filter.RAMs != null && filter.RAMs.Any()) productsQuery = productsQuery.Where(p => filter.RAMs.Contains(p.RAM));

        if (filter.OperatingSystems != null && filter.OperatingSystems.Any()) productsQuery = productsQuery.Where(p => filter.OperatingSystems.Contains(p.OperatingSystemEnum));

        if (filter.Producers != null && filter.Producers.Any()) productsQuery = productsQuery.Where(p => filter.Producers.Contains(p.Producer));

        if (filter.NFC != null) productsQuery = productsQuery.Where(p => p.NFC == filter.NFC);

        if (filter.NumberOfSIMCards != null) productsQuery = productsQuery.Where(p => p.NumberOfSIMCards == filter.NumberOfSIMCards);

        List<Product> products = await productsQuery.ToListAsync();

        const int pageSize = 12;

        if (pg < 1) pg = 1;

        int recsCount = products.Count();

        var pager = new Pager(recsCount, pg, pageSize);

        int recSkip = (pg - 1) * pageSize;

        var data = products.Skip(recSkip).Take(pager.PageSize).ToList();

        var viewModel = new ProductListViewModel { Products = data, Filter = filter, Pager = pager };

        ViewBag.Pager = pager;

        return View(viewModel);
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