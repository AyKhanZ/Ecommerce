using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Extensions;
using ASP_Project.Areas.Identity.Data.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

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

    // public IActionResult Index()
    // {
    //     var products = _dbContext.Products.ToList();
    //     var productimages = _dbContext.ProductImages.ToList(); //try it
    //     
    //     foreach (var product in products)
    //     {
    //         product.Category = _dbContext.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
    //     }
    //     if (products != null)
    //     {
    //         return View(products, productimages);
    //     }
    //     throw new ArgumentException("Product is not valid...");
    // }
    
    public IActionResult Index()
    {
        var products = _dbContext.Products.ToList();
        //var productImages = _dbContext.ProductImages.ToList();

        foreach (var product in products)
        {
            product.Category = _dbContext.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
        }

        //ViewBag.ProductImages = productImages;

        if (products != null)
        {
            return View(products);
        }
        throw new ArgumentException("Product is not valid...");
    }


    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product? product)
    {
        var result = await _productValidator.ValidateAsync(product);

        if (result.IsValid)
        {
            _dbContext.Products.Add(product); // make async
            _dbContext.SaveChanges();
            // Handle file uploads
            // foreach (var file in files)
            // {
            //     if (file != null && file.Length > 0)
            //     {
            //         using (var memoryStream = new MemoryStream())
            //         {
            //             await file.CopyToAsync(memoryStream);
            //             var imageBytes = memoryStream.ToArray();
            //
            //             // Create a new ProductImage entity and set the ImageData property
            //             var productImage = new ProductImage
            //             {
            //                 ImageData = imageBytes,
            //                 Url = "https://i.pinimg.com/originals/bb/73/ab/bb73ab989322760bcf3c0f64e549f7a3.jpg",
            //                 ProductId = product.Id // Set the ProductId to link it to the product
            //             };
            //
            //             Console.WriteLine(product.ProductImages);
            //             _dbContext.ProductImages.Add(productImage);
            //             Console.WriteLine(product.ProductImages);
            //             
            //
            //             _dbContext.SaveChanges();
            //         }
            //     }
            // }
        }

        result.AddToModelState(this.ModelState);

        return View(product);
    }


    public IActionResult Edit(int id)
    {
        var product = _dbContext.Products.Find(id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Product product)
    {
        var result = await _productValidator.ValidateAsync(product);

        if (result.IsValid)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
            TempData["success"] = "Product updated succsessfully";

            return RedirectToAction("Index", "Product");
        }
        result.AddToModelState(this.ModelState);

        return View(product);
    }


    public IActionResult Delete(int id)
    {
        var product = _dbContext!.Products!.Find(id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int id)
    {
        var product = _dbContext!.Products!.Find(id);

        if (product == null)
        {
            return NotFound();
        }
        _dbContext.Products.Remove(product);
        _dbContext.SaveChanges();
        TempData["success"] = "Product was deleted succsessfully";

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> AddVariation(int id)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product != null)
        {
            return RedirectToAction("Create", "ProductVariation", new { productId = id });
        }
        
        return NotFound();
    }
    
}