using Bogus;
using Microsoft.EntityFrameworkCore;


namespace Auction.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public DataInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedData()
        {
            _dbContext.Database.Migrate();
            SeedAdvertisement();
        }

        private void SeedAdvertisement()
        {
            while (_dbContext.Ads.Count() < 100)
            {
                _dbContext.Ads.Add(GenerateAds());
                _dbContext.SaveChanges();
            }
        }

        private Advertisement GenerateAds()
        {
            var testUser = new Faker<Advertisement>()
                .StrictMode(true)
                .RuleFor(e => e.Id, f => 0)
                .RuleFor(e => e.Title, (f, u) => f.Commerce.Product())  
                .RuleFor(e => e.StartDate, (f, u) => f.Date.Past())
                .RuleFor(e => e.EndDate, (f, u) => f.Date.Recent())
                .RuleFor(e => e.Description, (f, u) => f.Commerce.ProductDescription())
                .RuleFor(e => e.PopularityPercent, (f, u) => f.Random.Number(0, 100))
                .RuleFor(e => e.StartingPrice, (f, u) => Convert.ToDecimal(f.Commerce.Price()));

            var person = testUser.Generate(1).First();
            return person;
        }
    }
}
