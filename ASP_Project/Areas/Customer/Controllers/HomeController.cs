using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Models;
using ASP_Project.Areas.Identity.Data.Validators;
using ASP_Project.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASP_Project.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserContext _dbContext;

    public HomeController(ILogger<HomeController> logger, UserContext context)
    {
        _logger = logger;
        _dbContext = context;
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

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    } 
    public async Task<IActionResult> Open(int id)
	{
		var product = await _dbContext.Products.FindAsync(id);

		if (product == null) return NotFound();
		product.ProductImages = await _dbContext.ProductImages.Where(pi => pi.ProductId == id).ToListAsync();

		return View(product);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Buy(Product? product,int count)
	{
		TempData["success"] = "Product bought successfully";
		return RedirectToAction("Index", "Home");
	}
}