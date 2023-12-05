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

        public async Task<IActionResult> Index(string searchText = "",int pg = 1)
        {
            List<Product> products;
            if (searchText != "" && searchText != null) products = await _dbContext.Products.Include(p => p.ProductImages).Where(p=>p.Name.Contains(searchText)).ToListAsync();
            else products = await _dbContext.Products.Include(p => p.ProductImages).ToListAsync();

            const int pageSize = 10;
            if (pg < 1)
                pg = 1;

            int recsCount = products.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = products.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
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