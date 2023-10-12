namespace ASP_Project.Areas.Identity.Data.Models
{
    public class ShoppingCartItem
    {
        //public int Id { get; set; }
        public short Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    
        public int ProductVariationId { get; set; }
        public ProductVariation ProductVariation { get; set; }
    
        public string ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

    }
}