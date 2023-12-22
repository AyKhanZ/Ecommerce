using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Models;
using ASP_Project.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

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

        if (filter.RAMs != null && filter.RAMs.Any())
        {
            productsQuery = productsQuery.Where(p => filter.RAMs.Contains(p.RAM));
        }

        List<Product> products = await productsQuery.ToListAsync();

        const int pageSize = 10;

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
    
}