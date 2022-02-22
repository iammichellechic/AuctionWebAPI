using Auction.Data;
using Microsoft.AspNetCore.JsonPatch;
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

        /// <summary>
        /// Lists all the ads available
        /// </summary>
        /// <returns></returns>

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

        /// <summary>
        /// Returns a specific ad
        /// </summary>
        /// <returns></returns>

        [HttpGet]  // R 24
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Adds a new ad
        /// </summary>
        /// <returns></returns>
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument model)
        {
            var ad = _context.Ads.FirstOrDefault(a => a.Id == id);

            if (ad == null) return NotFound();
            //ad.Title = model.Title;
            //ad.Description = model.Description;
            //ad.StartingPrice = model.StartingPrice;
            //ad.EndDate = model.EndDate;
            //ad.StartDate = model.StartDate;
            //ad.PopularityPercent = model.PopularityPercent;
            model.ApplyTo(ad);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
