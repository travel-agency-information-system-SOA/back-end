using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Explorer.API.Controllers.Author.Authoring
{
    [Route("api/administration/tour")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }


        [HttpPost]
        public ActionResult<TourDTO> Create([FromBody] TourDTO tour)
        {
            tour.Status = "Draft";
            tour.Price = 0;

            var result = _tourService.Create(tour);

            return CreateResponse(result);
        }


        [HttpGet("search/{lat:double}/{lon:double}/{ran:int}")]
        //[AllowAnonymous]
        public ActionResult<PagedResult<TourDTO>> GetByRange(double lat, double lon, int ran, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetByRange(lat, lon, ran, page, pageSize);
            return CreateResponse(result);
        }


        
        //[Authorize(Policy = "authorPolicy")]

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
       
        //[Authorize(Policy = "authorPolicy")] ostaviti ovako, jer i administrator updatuje turu
        [HttpPut("{id:int}")]
        public ActionResult<TourDTO> Update([FromBody] TourDTO tourDto)
        {
            var result = _tourService.Update(tourDto);
            return CreateResponse(result);
        }

        
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

        [HttpGet("onetour/{id:int}")]

        public ActionResult<TourDTO> getTourByTourId(int id)
        {
            var result = _tourService.GetTourByTourId(id);
            return CreateResponse(result);
        }

        //[Authorize(Policy = "touristPolicy")]
        [HttpGet("allTours")]
        public ActionResult<PagedResult<TourReviewDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize) {
            var result = _tourService.GetAll(page, pageSize);
            return CreateResponse(result);
        }



        [Authorize(Policy = "touristPolicy")]
        [HttpGet("filteredTours")]
        public ActionResult<PagedResult<TourDTO>> FilterToursByPublicTourPoints(
        [FromQuery] string publicTourPoints,
        [FromQuery] int page,
        [FromQuery] int pageSize)
        {
            try
            {
                
                var publicTourPointsArray = JsonConvert.DeserializeObject<PublicTourPointDto[]>(publicTourPoints);

                
              

                var result = _tourService.FilterToursByPublicTourPoints(publicTourPointsArray, page, pageSize);
                return CreateResponse(result);
            }
            catch (JsonException ex)
            {
          
                Console.WriteLine($"Error deserializing publicTourPoints: {ex.Message}");

                
                return BadRequest("Invalid publicTourPoints format");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Unexpected error: {ex.Message}");

                return StatusCode(500, "Internal server error");
            }

			


		}

		[Authorize(Policy = "touristPolicy")]
		[HttpGet("lastId")]
        public long GetLastTourId([FromQuery] int page,[FromQuery] int pageSize)
        {
            return _tourService.GetLastTourId(page, pageSize);
        }


	}
}
