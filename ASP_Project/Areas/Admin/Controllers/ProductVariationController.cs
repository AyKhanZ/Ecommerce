//using ASP_Project.Areas.Identity.Data.DbContexts;
//using ASP_Project.Areas.Identity.Data.Extensions;
//using ASP_Project.Areas.Identity.Data.Models;
//using FluentValidation;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace ASP_Project.Areas.Admin.Controllers;
//[Area("Admin")]
//[Authorize(Roles = "Admin")]
//public class ProductVariationController : Controller
//{
//    private IValidator<ProductVariation> _productVariationValidator { get; set; }

//    private readonly UserContext _dbContext;

//    public ProductVariationController(UserContext context, IValidator<ProductVariation> validator)
//    {
//        _dbContext = context;
//        _productVariationValidator = validator;
//    }

//    public async Task<IActionResult> Index()
//    {
//        var productVariations = await _dbContext.ProductVariations.ToListAsync();
//        var productImages = await _dbContext.ProductImages.ToListAsync();

//        foreach (var productVariation in productVariations)
//        {
//            productVariation.Product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productVariation.ProductId);
//        }

//        ViewBag.ProductImages = productImages;

//        return View(productVariations);
//    }
//    public IActionResult Create()
//    {
//        return View();
//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Create(int productId, ProductVariation productVariation, IFormFileCollection ProductImages)
//    {
//        ModelState.Clear(); 

//        productVariation.ProductId = productId;

//        productVariation.Name = $"{productVariation.Color} {productVariation.RAM} {productVariation.InternalMemory}";
         
//        List<ProductImage> productImages = new List<ProductImage>();

//        foreach (var file in ProductImages)
//        {
//            if (file != null && file.Length > 0)
//            {
//                var productImage = new ProductImage();

//                using (var memoryStream = new MemoryStream())
//                {
//                    await file.CopyToAsync(memoryStream);
//                    var imageBytes = memoryStream.ToArray();
//                    productImage = new ProductImage
//                    {
//                        ImageData = imageBytes,
//                        ProductVariationId = productVariation.Id
//                    };
//                    productImages.Add(productImage);
//                }
//            }
//            else return NotFound();
//        }

//        productVariation.ProductImages = productImages;
//        var result = await _productVariationValidator.ValidateAsync(productVariation);

//        if (!result.IsValid)
//        {
//            result.AddToModelState(ModelState);
//            return View(productVariation);
//        }

//        await _dbContext.ProductVariations.AddAsync(productVariation);
//        await _dbContext.SaveChangesAsync();

//        var newProductImages = productImages.Select(pi => new ProductImage
//        {
//            ImageData = pi.ImageData,
//            ProductVariationId = pi.ProductVariationId
//        }).ToList(); // Создаем новую коллекцию

//        await _dbContext.ProductImages.AddRangeAsync(newProductImages);
//        await _dbContext.SaveChangesAsync();

//        TempData["success"] = "Product variation created succsessfully";
//        return RedirectToAction("Index", "ProductVariation");
//    }

//    public async Task<IActionResult> Edit(int id)
//    {
//        var productVariation = await _dbContext.ProductVariations
//            .Include(pv => pv.ProductImages)
//            .FirstOrDefaultAsync(pv => pv.Id == id);

//        if (productVariation == null) return NotFound();

//        ViewBag.ProductImages = productVariation.ProductImages;
//        return View(productVariation);
//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Edit(ProductVariation productVariation, IFormFileCollection files)
//    {
//        if (true) // add validation
//        {
//            var existingProductVariation = await _dbContext.ProductVariations
//                .FirstOrDefaultAsync(pv => pv.Id == productVariation.Id);

//            if (existingProductVariation == null) return NotFound();

//            existingProductVariation.Price = productVariation.Price;
//            existingProductVariation.Quantity = productVariation.Quantity;
//            existingProductVariation.Discount = productVariation.Discount;

//            foreach (var file in files)
//            {
//                if (file != null && file.Length > 0)
//                {
//                    using (var memoryStream = new MemoryStream())
//                    {
//                        await file.CopyToAsync(memoryStream);
//                        var imageBytes = memoryStream.ToArray();

//                        var productImage = new ProductImage
//                        {
//                            ImageData = imageBytes,
//                            ProductVariationId = existingProductVariation.Id
//                        };

//                        await _dbContext.ProductImages.AddAsync(productImage);
//                        existingProductVariation.ProductImages.Add(productImage);
//                    }
//                }
//            }

//            _dbContext.ProductVariations.Update(existingProductVariation);
//            await _dbContext.SaveChangesAsync();
//            var productImages = await _dbContext.ProductImages.ToListAsync();
//            ViewBag.ProductImages = productImages;
//            TempData["success"] = "Product Variation updated successfully";

//            return View(productVariation);

//        }

//    }
//}