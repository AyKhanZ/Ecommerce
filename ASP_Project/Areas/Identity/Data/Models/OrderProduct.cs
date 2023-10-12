namespace ASP_Project.Areas.Identity.Data.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    
        public int ShoppingCartItemId { get; set; }
        public ShoppingCartItem ShoppingCartItem { get; set; }
    
        public List<OrderStateHistory> StateHistories { get; set; }
    }
}