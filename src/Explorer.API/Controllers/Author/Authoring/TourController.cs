using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Authoring
{
   // [Authorize(Policy = "authorPolicy")] **** premjesteno za svaku metodu posebno, zbog dodatne metode za turistu
    [Route("api/administration/tour")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpPost]
        public ActionResult<TourDTO> Create([FromBody] TourDTO tour)
        {

            tour.Status = "Draft";
            tour.Price = 0;

            var result = _tourService.Create(tour);

            return CreateResponse(result);

        }
        
        [Authorize(Policy = "authorPolicy")]
        [HttpGet("{userId:int}")]
        public ActionResult<PagedResult<TourDTO>> GetByUserId(int userId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetByUserId(userId, page, pageSize);
            return CreateResponse(result);
        }
        
        [Authorize(Policy = "authorPolicy")]
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourService.Delete(id);
            return CreateResponse(result);
        }
       
        [Authorize(Policy = "authorPolicy")]
        [HttpPut("{id:int}")]
        public ActionResult<TourDTO> Update([FromBody] TourDTO tourDto)
        {
            var result = _tourService.Update(tourDto);
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpPut("caracteristics/{id:int}")]
        public ActionResult AddCaracteristics(int id, [FromBody] TourCharacteristicDTO tourCharacteristic)
        {
            var result = _tourService.SetTourCharacteristic(id, tourCharacteristic.Distance, tourCharacteristic.Duration, tourCharacteristic.TransportType);
            return CreateResponse(result);
        }

        [HttpPut("publish/{tourId:int}")]
        public ActionResult Publish(int tourId)
        {
            var result = _tourService.Publish(tourId);
            return CreateResponse(result);
        }

       
        [Authorize(Policy = "authorPolicy")]
        [HttpPut("archive/{id:int}")]
        public ActionResult ArchiveTour(int id)
        {
            var result = _tourService.ArchiveTour(id);
            return CreateResponse(result);
        }
       
        [Authorize(Policy = "authorPolicy")]
        [HttpDelete("deleteAggregate/{id:int}")]
        public ActionResult DeleteAggregate(int id)
        {
            var result = _tourService.DeleteAggregate(id);
            return CreateResponse(result);
        }
        //dodato za tourReview
        [Authorize(Policy = "touristPolicy")]
        [HttpGet("allTours")]
        public ActionResult<PagedResult<TourReviewDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize) {
            var result = _tourService.GetAll(page, pageSize);
            return CreateResponse(result);
        }
    }
}
