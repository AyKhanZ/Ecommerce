using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Models;
using ASP_Project.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASP_Project.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserContext _dbContext;

        public HomeController(ILogger<HomeController> logger,UserContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _dbContext.Products.Include(p => p.ProductImages).ToListAsync();
            return View(products);
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
}