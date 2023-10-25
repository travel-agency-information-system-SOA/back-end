using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;

namespace Explorer.API.Controllers.Author.Administration
{
        [Authorize(Policy = "authorPolicy")]
        [Route("api/administration/tourkeypoint")]
        public class TourKeyPointController : BaseApiController
        {
            private readonly ITourKeyPointService _tourKeyPointService;

            public TourKeyPointController(ITourKeyPointService tourKeyPointService)
            {
            _tourKeyPointService = tourKeyPointService;
            }

            [HttpPost]
            public ActionResult AddKeyPointToTour([FromBody] TourKeyPointDto tourKeyPoint)
            {
                var result = _tourKeyPointService.AddKeyPointToTourAsync(tourKeyPoint.TourId, tourKeyPoint.PointId);



                return CreateResponse(result.Result);
            }

            [HttpGet("{tourId:int}")]
            public ActionResult<List<TourKeyPointDto>> GetTourKeyPoint(long tourId)
            {
                var result = _tourKeyPointService.GetTourKeyPointAsync(tourId);
                return CreateResponse(result.Result);

            }

            [HttpDelete("{tourId:int}/{pointId:int}")]
            public ActionResult RemoveKeyPointFromTour(long tourId, long pointId)
            {
                var result = _tourKeyPointService.RemoveKeyPointFromTourAsync(tourId, pointId);
                return CreateResponse(result.Result);

            }
        }
}
