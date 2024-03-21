using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Explorer.API.Controllers.Author.Administration
{
    [Route("api/administration/publicTourPoint")]
    public class PublicTourPointController : BaseApiController
    {
        private readonly IPublicTourPointService _tourPointService;
        private readonly IInternalTourPointRequestService _internalTourPointRequestService;
        private readonly HttpClient _httpClient = new HttpClient();

        public PublicTourPointController(IPublicTourPointService tourPointService, IInternalTourPointRequestService internalService)
        {
            _tourPointService = tourPointService;
            _internalTourPointRequestService = internalService;  
        }

        [HttpGet]
        public ActionResult<PagedResult<TourPointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourPointService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost("createPublicTourPoint/{requestId:int}/{tourPointId:int}/{comment}")]
        public async Task<ActionResult<PublicTourPointDto>> CreatePublicTourPointAndAcceptRequest(int requestId, int tourPointId,string comment)
        {
            /* var result = _tourPointService.CreatePublicTourPointAndAcceptRequest(requestId, tourPointId,comment);
             return CreateResponse(result);*/
            var acceptedRequest = _internalTourPointRequestService.AcceptRequest(requestId, comment);

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:3000/publicTourPoint/setPublicTourPoint/{tourPointId}");
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    PublicTourPointDto tourPoint = JsonConvert.DeserializeObject<PublicTourPointDto>(responseContent);
                    return Ok(tourPoint);
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

