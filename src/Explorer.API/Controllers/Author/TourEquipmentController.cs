using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{

    [Route("api/administration/tourequipment")]
    public class TourEquipmentController : BaseApiController
    {
        private readonly ITourEquipmentService _tourEquipmentService;

        public TourEquipmentController(ITourEquipmentService tourEquipmentService)
        {
            _tourEquipmentService = tourEquipmentService;
        }

        [HttpPost]
        public ActionResult AddEquipmentToTour([FromBody] TourEquipmentDto tourEquipment)
        {
            var result = _tourEquipmentService.AddEquipmentToTourAsync(tourEquipment.TourId, tourEquipment.EquipmentId);

            

           return CreateResponse(result.Result);
        }

        [HttpGet("{tourId:int}")]
        public ActionResult<List<TourEquipmentDto>> GetTourEquipment(long tourId)
        {
            var result = _tourEquipmentService.GetTourEquipmentAsync(tourId);
            return CreateResponse(result.Result);

        }

        [HttpDelete("{tourId:int}/{equipmentId:int}")]
        public ActionResult RemoveEquipmentFromTour(long tourId, long equipmentId)
        {
            var result = _tourEquipmentService.RemoveEquipmentFromTourAsync(tourId, equipmentId);
            return CreateResponse(result.Result);

        }
    }
}
