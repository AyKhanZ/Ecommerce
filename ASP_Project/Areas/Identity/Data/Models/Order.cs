namespace ASP_Project.Areas.Identity.Data.Models
{
    public enum DeliveryState
    {
        Pending,
        Shipped,
        Delivered,
        Cancelled
    }
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DeliveryState DeliveryState { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    
        public List<OrderProduct> OrderProducts { get; set; }

    }
}
