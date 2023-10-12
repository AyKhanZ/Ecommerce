using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Extensions;
using ASP_Project.Areas.Identity.Data.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Area("Admin")]
[Authorize(Roles = "Admin")]

public class ProductVariationController : Controller
{
    private readonly UserContext _dbContext;

    public ProductVariationController(UserContext context)
    {
        _dbContext = context;

    }


    public IActionResult Index()
    {
        var productVariations = _dbContext.ProductVariations.ToList();
        var productImages = _dbContext.ProductImages.ToList();

        foreach (var productVariation in productVariations)
        {
            productVariation.Product = _dbContext.Products.FirstOrDefault(p => p.Id == productVariation.ProductId);
        }

        ViewBag.ProductImages = productImages;

        if (productVariations != null)
        {
            return View(productVariations);
        }
        throw new ArgumentException("Product is not valid...");
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int productId, ProductVariation productVariation, IFormFileCollection files)
    {
        productVariation.ProductId = productId;


        Console.WriteLine(productVariation);

        _dbContext.ProductVariations.Add(productVariation);
        _dbContext.SaveChanges();

        foreach (var file in files)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();

                    var productImage = new ProductImage
                    {
                        ImageData = imageBytes,
                        Url = "https://i.pinimg.com/originals/bb/73/ab/bb73ab989322760bcf3c0f64e549f7a3.jpg", //delete it 
                        ProductVariationId = productVariation.Id
                    };

                    _dbContext.ProductImages.Add(productImage);


                    _dbContext.SaveChanges();

                }
            }
            else
                return NotFound();
        }
        return View(productVariation);

    }

    public IActionResult Edit(int id)
    {
        var productVariation = _dbContext.ProductVariations.Find(id);

        if (productVariation == null)
        {
            return NotFound();
        }

        return View(productVariation);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductVariation productVariation)
    {
        //var result = await _productValidator.ValidateAsync(product);

        //if (result.IsValid)
        //{
        //	_dbContext.Products.Update(product);
        //	_dbContext.SaveChanges();
        //	TempData["success"] = "Product updated succsessfully";

        //	return RedirectToAction("Index", "Product");
        //}
        //result.AddToModelState(this.ModelState);

        //return View(product);
        _dbContext.ProductVariations.Update(productVariation);
        _dbContext.SaveChanges();
        TempData["success"] = "Product Variation updated succsessfully";

        return View(); //RedirectToAction("Index", "ProductVaritation");
    }
}