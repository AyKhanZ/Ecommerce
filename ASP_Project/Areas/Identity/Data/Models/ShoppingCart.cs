namespace ASP_Project.Areas.Identity.Data.Models;

public class ShoppingCart
{
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public short TotalQuantity { get; set; }
    public decimal TotalPrice { get; set; }

    public List<ShoppingCartItem>? ShoppingCartItems { get; set; }
}