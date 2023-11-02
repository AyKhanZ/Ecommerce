namespace ASP_Project.Areas.Identity.Data.Models;

public class ShoppingCartItem
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    
    public short Quantity { get; set; }
    public decimal TotalPrice { get; set; }

    public string ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; }

}