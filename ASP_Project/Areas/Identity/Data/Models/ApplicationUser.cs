using Microsoft.AspNetCore.Identity;

namespace ASP_Project.Areas.Identity.Data.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    
    //public int ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    
    //public Favorite Favorite { get; set; }  // test //
    
    public List<OnlinePayment>? OnlinePayments { get; set; }
    public List<Order>? Orders { get; set; }
    public List<Address>? Addresses { get; set; }
}