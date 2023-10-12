namespace ASP_Project.Areas.Identity.Data.Models
{
    public class OrderStateHistory
    {
        public int Id { get; set; }
        public DeliveryState State { get; set; }
        public DateTime DateChanged { get; set; }

        public int OrderProductId { get; set; }
        public OrderProduct OrderProduct { get; set; }
    }
}