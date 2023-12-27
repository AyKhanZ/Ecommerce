namespace ASP_Project.Areas.Identity.Data.Models;

public class Favorite
{
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public List<Product>? FavoriteProducts { get; set; }
}