namespace ASP_Project.Areas.Identity.Data.Models;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int ParentCategoryId { get; set; }
    public ParentCategory ParentCategory { get; set; }

    public List<Product>? Products { get; set; }
}