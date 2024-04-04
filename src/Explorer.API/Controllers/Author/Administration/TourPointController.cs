using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.UseCases.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Explorer.API.Controllers.Author.Administration
{

   // [Authorize(Policy = "authorAndAdminPolicy")]

    [Route("api/administration/tourPoint")] 
    public class TourPointController : BaseApiController
    {
        private readonly ITourPointService _tourPointService;
        private readonly HttpClient _httpClient = new HttpClient();

        public TourPointController(ITourPointService tourPointService)
        {
            _tourPointService = tourPointService;
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpGet]
        public async Task<ActionResult<PagedResult<TourPointDto>>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            //var result = _tourPointService.GetPaged(page, pageSize);
            //return CreateResponse(result);
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("http://tours:3000/tourPoint/getAll");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    List<TourPointDto> tourPoints = JsonConvert.DeserializeObject<List<TourPointDto>>(responseContent);
                    PagedResult<TourPointDto> pagedResult = new PagedResult<TourPointDto>(tourPoints, tourPoints.Count);
                    return Ok(pagedResult);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error occurred while getting tour points.");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
            }
        }

        [HttpPost]  //ovo je za dovanje kljucne tacke
        public async Task<ActionResult<PagedResult<TourPointDto>>> Create([FromBody] TourPointDto tourPoint)
        {
            /*var result = _tourPointService.Create(tourPoint);
            Console.WriteLine("rezultat id:" + result.Value.Id);
            return CreateResponse(result);*/
  
            string json = JsonConvert.SerializeObject(tourPoint);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("http://tours:3000/tourPoint/create", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    TourPointDto createdTourPoint = JsonConvert.DeserializeObject<TourPointDto>(responseContent);
                    return Ok(createdTourPoint);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error occurred while creating tour point.");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
            }
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpPut("{id:int}")]
        public ActionResult<TourPointDto> Update([FromBody] TourPointDto tourPoint)
        {
            var result = _tourPointService.Update(tourPoint);
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourPointService.Delete(id);
            return CreateResponse(result);
        }

        [Authorize(Policy = "touristAuthorPolicy")]
        [HttpGet("{tourId:int}")]

		public ActionResult<List<TourPointDto>> GetTourPointsByTourId(int tourId)
		{
			var result = _tourPointService.GetTourPointsByTourId(tourId);
			return CreateResponse(result);
		}

        [HttpGet("getById/{id:int}")]
        public async  Task<ActionResult<TourPointDto>>  GetTourPointById(int id)
        {
            // var result = _tourPointService.Get(id);
            // return CreateResponse(Result.Ok(result));
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"http://tours:3000/tourPoint/getById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    TourPointDto tourPoint = JsonConvert.DeserializeObject<TourPointDto>(responseContent);
                    return Ok(tourPoint);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound("Tour point not found.");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error occurred while getting tour point.");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error occurred while sending request: {ex.Message}");
            }
        }
    }
}
