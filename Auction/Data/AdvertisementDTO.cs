using System.ComponentModel.DataAnnotations;

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
        [MaxLength(100)]
        public string Title { get; set; }

        [Range(0, 1000000)]
        public decimal StartingPrice { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        [Range(0, 100)]
        public int PopularityPercent { get; set; }
    }
}
