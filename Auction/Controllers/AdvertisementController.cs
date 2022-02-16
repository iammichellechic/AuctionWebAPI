using Auction.Data;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //surfa till api/advertisement
    public class AdvertisementController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdvertisementController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]  // R
        public IEnumerable<AdvertisementDTO> Get()
        {
            return _context.Ads.Select(a => new AdvertisementDTO
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                StartingPrice = a.StartingPrice,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                PopularityPercent = a.PopularityPercent

            });
        }


        [HttpGet]  // R 24
        [Route("{id}")]
        public IActionResult GetSingleAd(int id)
        {
            var advertisement = _context.Ads.Where(a => a.Id == id).Select(a => new AdvertisementDTO
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                StartingPrice = a.StartingPrice,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                PopularityPercent = a.PopularityPercent

            }).FirstOrDefault();
            if (advertisement == null)
                return NotFound();
            return Ok(advertisement);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AdvertisementDTO> Create(AdvertisementNewDTO model)
        {

            var ad = new Advertisement
            {
                Title = model.Title,
                StartingPrice = model.StartingPrice,
                EndDate = model.EndDate,
                StartDate = DateTime.UtcNow,
                Description = model.Description,
                PopularityPercent = model.PopularityPercent
            };
            _context.Ads.Add(ad);
            _context.SaveChanges();
            int id = ad.Id;

            //map
            var obj = new AdvertisementDTO();
            obj.Title = ad.Title;
            obj.Description = ad.Description;
            obj.StartingPrice = ad.StartingPrice;
            obj.EndDate = ad.EndDate;
            obj.StartDate = ad.StartDate;
            obj.PopularityPercent = ad.PopularityPercent;


            return CreatedAtAction(nameof(GetSingleAd), new { id = id }, obj);

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AdvertisementDTO model)
        {

            var ad = _context.Ads.FirstOrDefault(a => a.Id == id);
            if (ad == null) return NotFound();
            ad.Title = model.Title;
            ad.Description = model.Description;
            ad.StartingPrice = model.StartingPrice;
            ad.EndDate = model.EndDate;
            ad.StartDate = model.StartDate;
            ad.PopularityPercent = model.PopularityPercent;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ad = _context.Ads.FirstOrDefault(a => a.Id == id);
            if (ad == null) return NotFound();

            _context.Ads.Remove(ad);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
