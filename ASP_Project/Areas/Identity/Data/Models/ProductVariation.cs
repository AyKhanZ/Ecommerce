namespace ASP_Project.Areas.Identity.Data.Models
{
    public enum Color
    {
        None,
        Multicolor,
        White,
        Black,
        Gray,
        Beige,
        Cream,
        Red,
        Burgundy,
        Purple,
        Pink,
        Blue,
        Azure,
        Yellow,
        Orange,
        Brown,
        Green,
        Turquoise,
        Khaki,
        Ecru,
        Gold,
        Silver,
    }
    public class ProductVariation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public Color Color { get; set; }
        public uint Quantity { get; set; }
        public short? Discount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    
        public ShoppingCartItem ShoppingCartItem { get; set; } //test//
        
        public List<ProductImage> ProductImages { get; set; }
    }
}