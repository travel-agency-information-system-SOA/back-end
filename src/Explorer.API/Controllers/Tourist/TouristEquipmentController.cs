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

         
        [HttpGet("getTouristEquipment/{touristId:int}")]
        public ActionResult<TouristEquipmentDto> GetTouristEquipment(int touristId)
        {
            var result =  _touristEquipmentService.GetTouristEquipment(touristId); 
            return CreateResponse(result);
        }

        [HttpPost("createTouristEquipment/{id:int}")]
        public ActionResult<TouristEquipmentDto> CreteTouristEquipment(int id)
        {
            var result = _touristEquipmentService.Create(id);
            return CreateResponse(result);
        }

        [HttpPut("addToMyEquipment/{touristId:int}/{equipmentId:int}")]
        public ActionResult<TouristEquipmentDto> AddToMyEquipment(int touristId, int equipmentId)
        {
            var result = _touristEquipmentService.AddToMyEquipment(touristId, equipmentId);
            return CreateResponse(result);
        }


        [HttpPut("deleteFromMyEquipment/{touristId:int}/{equipmentId:int}")]
        public ActionResult<TouristEquipmentDto> DeleteFromMyEquipment(int touristId, int equipmentId)
        {
            var result = _touristEquipmentService.DeleteFromMyEquipment(touristId, equipmentId);
            return CreateResponse(result);
        }
    }
}
