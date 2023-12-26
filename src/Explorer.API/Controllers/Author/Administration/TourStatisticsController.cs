using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;

namespace Explorer.API.Controllers.Author.Administration
{
    [Route("api/administration/tourStatistics")]
    public class TourStatisticsController : BaseApiController
    {
        private ITourStatisticsService _tourStatisticsService;
        public TourStatisticsController(ITourStatisticsService tourStatisticsService) {
            _tourStatisticsService = tourStatisticsService;
        }

        [HttpGet("getPurchasedNumber")]
        public int GetNumberOfPurchaseByAuthor(int authorId)
        {
            var result = _tourStatisticsService.GetNumberOfPurchaseByAuthor(authorId);
          
            return result;
        }

        [HttpGet("getCompletedNumber")]
        public int GetNumberOfCompletedByAuthor(int authorId)
        {
            var result = _tourStatisticsService.GetNumberOfCompletedByAuthor(authorId);

            return result;
        }

        [HttpGet("getStartedNumber")]
        public int GetNumberOfStartedByAuthor(int authorId)
        {
            var result = _tourStatisticsService.GetNumberOfStartedByAuthor(authorId);

            return result;
        }

    }
}
