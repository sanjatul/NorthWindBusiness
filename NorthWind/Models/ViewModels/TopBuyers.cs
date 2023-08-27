namespace NorthWind.Models.ViewModels
{
    public class TopBuyers
    {
        public int OrderId { get; set; }
        public String? CustomerName { get; set; }
        public String? ProductName { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal Amount { get; set; }
    }
}
