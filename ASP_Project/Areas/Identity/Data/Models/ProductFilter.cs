namespace ASP_Project.Areas.Identity.Data.Models;

public class ProductFilter
{
    public string SearchText { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public List<string> RAMs { get; set; }
    public List<OperatingSystemEnum> OperatingSystems { get; set; }
    public List<Producer> Producers { get; set; }
    public bool? NFC { get; set; } 
    public int? NumberOfSIMCards { get; set; }
}