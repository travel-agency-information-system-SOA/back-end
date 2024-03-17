using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace Explorer.API.Controllers.Author.Authoring
{
    [Route("api/administration/tour")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;
        private readonly HttpClient _httpClient = new HttpClient();

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
           
        }


        [HttpPost]
        public async Task<ActionResult<TourDTO>> Create([FromBody] TourDTO tour)
        {
            tour.Status = "Draft";
            tour.Price = 0;
            // var result = _tourService.Create(tour);
            //return CreateResponse(result);

            // Serijalizujemo objekat u JSON
            string json = JsonConvert.SerializeObject(tour);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                // Šaljemo POST zahtev na mikroservis
                HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:3000/tours/create", content);

                // Proveravamo status odgovora
                if (response.IsSuccessStatusCode)
                {
                    // Ako je odgovor uspešan, čitamo sadržaj odgovora
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Deserijalizujemo JSON odgovor u TourDTO objekat
                    TourDTO createdTour = JsonConvert.DeserializeObject<TourDTO>(responseContent);

                    // Vraćamo OK rezultat sa kreiranim turizmom
                    return Ok(createdTour);
                }
                else
                {
                    // Ako je došlo do greške, vraćamo odgovarajući HTTP status
                    return StatusCode((int)response.StatusCode, "Error occurred while creating tour.");
                }
            }
            catch (HttpRequestException ex)
            {
                // Uhvatamo eventualne greške prilikom slanja zahteva
                return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
            }

        }

        [HttpGet("search/{lat:double}/{lon:double}/{ran:int}/{type:int}")]
        //[AllowAnonymous]
        public ActionResult<PagedResult<TourDTO>> GetByRange(double lat, double lon, int ran, int type, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetByRange(lat, lon, ran, type, page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("filter/{level}/{price}")]
        public ActionResult<PagedResult<TourDTO>> GetByLevelAndPrice(string level, int price, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetByLevelAndPrice(level, price, page, pageSize);
            return CreateResponse(result);
        }


        [HttpGet("{userId:int}")]
        public async Task<ActionResult<PagedResult<TourDTO>>> GetByUserId(int userId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                string url = $"http://localhost:3000/tours/getByAuthor/{userId}?page={page}&pageSize={pageSize}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Deserializujete odgovor u List<TourDTO>
                    List<TourDTO> tours = JsonConvert.DeserializeObject<List<TourDTO>>(responseContent);
                    // Kreiranje PagedResult objekta
                    PagedResult<TourDTO> pagedResult = new PagedResult<TourDTO>(tours, tours.Count);
                    // Vraćanje PagedResult<TourDTO> kao rezultat akcije.
                    return Ok(pagedResult);
                }
                else
                {
                    // Ako je došlo do greške, vratite odgovarajući HTTP status
                    return StatusCode((int)response.StatusCode, "Error occurred while fetching tours.");
                }
            }
            catch (HttpRequestException ex)
            {
                // Uhvatite eventualne greške prilikom slanja zahteva
                return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
            }
        }



        [Authorize(Policy = "touristAuthorPolicy")]
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

       
        [Authorize(Policy = "touristAuthorPolicy")]
        [HttpPut("archive/{id:int}")]
        public ActionResult ArchiveTour(int id)
        {
            var result = _tourService.ArchiveTour(id);
            return CreateResponse(result);
        }
       
        [Authorize(Policy = "touristAuthorPolicy")]
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

        [Authorize(Policy = "authorPolicy")]
        [HttpGet("sales/{id:int}")]
        public ActionResult<PagedResult<TourDTO>> GetAllPublishedByAuthor(int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetAllPublishedByAuthor(id, page, pageSize);
            return CreateResponse(result);
        }

        //[Authorize(Policy = "touristPolicy")]
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