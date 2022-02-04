using System.ComponentModel.DataAnnotations;

namespace Auction.Data
{
    public class Advertisement
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }
        public decimal StartingPrice { get; set; }
     
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PopularityPercent { get; set; }
    }
}
