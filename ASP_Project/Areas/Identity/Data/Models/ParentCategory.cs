namespace ASP_Project.Areas.Identity.Data.Models;

public class ParentCategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Category>? Categories { get; set; }
}