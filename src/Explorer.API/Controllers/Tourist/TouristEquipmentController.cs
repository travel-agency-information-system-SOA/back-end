using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/touristEquipment")]  
    public class TouristEquipmentController : BaseApiController
    {
        private readonly ITouristEquipmentService _touristEquipmentService;
        private readonly IEquipmentService _equipmentService;
        public TouristEquipmentController(ITouristEquipmentService equipmentService,IEquipmentService service)
        {
            _touristEquipmentService = equipmentService;
            _equipmentService = service;
        }

        [HttpGet]
        public ActionResult<ObservableCollection<TouristEquipmentDto>> GetTouristEquipment([FromQuery] TouristEquipmentDto touristEquipment)
        {
            var result = _touristEquipmentService.GetTouristEquipment(touristEquipment.TouristId);
            return CreateResponse(result);
        }

        [HttpGet("/lala")]
        public ActionResult<ObservableCollection<TouristEquipmentDto>> GetOtherEquipment([FromQuery] TouristEquipmentDto touristEquipment)
        {
            var result = _touristEquipmentService.GetOtherEquipment(touristEquipment.TouristId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TouristEquipmentDto> Create([FromBody] TouristEquipmentDto equipment)
        {
            var result = _touristEquipmentService.Create(equipment);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<TouristEquipmentDto> Update([FromBody] TouristEquipmentDto equipment)
        {
            var result = _touristEquipmentService.Update(equipment);
            return CreateResponse(result);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _touristEquipmentService.Delete(id);
            return CreateResponse(result);
        }
    
    }
}
