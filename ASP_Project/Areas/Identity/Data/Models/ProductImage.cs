namespace ASP_Project.Areas.Identity.Data.Models;

public class ProductImage
{
    public int Id { get; set; }
    
    public byte[]? ImageData { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } 
}