namespace Auction.Data
{
    public class AdvertisementDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal StartingPrice { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PopularityPercent { get; set; }
    }

    public class AdvertisementNewDTO
    {
        public string Title { get; set; }
        public decimal StartingPrice { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PopularityPercent { get; set; }
    }
}
