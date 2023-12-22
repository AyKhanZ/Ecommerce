namespace ASP_Project.Areas.Identity.Data.Models;
public class ProductListViewModel
{
    public IEnumerable<Product> Products { get; set; }
    public ProductFilter Filter { get; set; }
    public Pager Pager { get; set; }
}