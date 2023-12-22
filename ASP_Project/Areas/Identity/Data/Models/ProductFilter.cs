namespace ASP_Project.Areas.Identity.Data.Models;

public class ProductFilter
{
    public string SearchText { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public List<string> RAMs { get; set; }
}