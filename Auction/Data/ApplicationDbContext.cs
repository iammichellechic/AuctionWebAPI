using Microsoft.EntityFrameworkCore;

namespace Auction.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }
       public DbSet<Advertisement> Ads { get; set; }
    }
}
