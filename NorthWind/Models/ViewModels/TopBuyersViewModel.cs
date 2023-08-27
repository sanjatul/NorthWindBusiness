namespace NorthWind.Models.ViewModels
{
    public class TopBuyersViewModel
    {
        public List<TopBuyers> TopBuyersList { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}
