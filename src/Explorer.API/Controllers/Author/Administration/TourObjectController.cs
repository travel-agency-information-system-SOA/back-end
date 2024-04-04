using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Explorer.API.Controllers.Author.Administration
{

    [Route("api/administration/object")]
    public class ObjectController : BaseApiController
    {
        private readonly ITourObjectService _objectService;
		private readonly HttpClient _httpClient = new HttpClient();

		public ObjectController(ITourObjectService objectService)
        {
            _objectService = objectService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourObjectDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _objectService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public async Task<ActionResult<TourObjectDto>> Create([FromBody] TourObjectDto tourObject)
        {
			// Serijalizujemo objekat u JSON
			string json = JsonConvert.SerializeObject(tourObject);
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
			try
			{
				// Šaljemo POST zahtev na mikroservis
				HttpResponseMessage response = await _httpClient.PostAsync("http://tours:3000/tourObjects/create", content);

				// Proveravamo status odgovora
				if (response.IsSuccessStatusCode)
				{
					// Ako je odgovor uspešan, čitamo sadržaj odgovora
					string responseContent = await response.Content.ReadAsStringAsync();

					// Deserijalizujemo JSON odgovor u TourDTO objekat
					TourObjectDto createdObject = JsonConvert.DeserializeObject<TourObjectDto>(responseContent);

					// Vraćamo OK rezultat sa kreiranim turizmom
					return Ok(createdObject);
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

        [HttpPut("{id:int}")]
        public ActionResult<TourObjectDto> Update([FromBody] TourObjectDto tourObject)
        {
            var result = _objectService.Update(tourObject);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _objectService.Delete(id);
            return CreateResponse(result);
        }

		[HttpGet("{id:int}")]

		public ActionResult<TourObjectDto> GetById(int id)
		{
			var result = _objectService.Get(id);
			return CreateResponse(result);
		}
	}
}
