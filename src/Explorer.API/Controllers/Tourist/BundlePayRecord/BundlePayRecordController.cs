using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.BundlePayRecord;
using Explorer.Payments.API.Public.BundlePayRecord;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.BundlePayRecord
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/bundlepayrecord")]
    public class BundlePayRecordController : BaseApiController
    {
        private readonly ITourBundleService _tourBundleService; // za prikaz svih bundle-ova
        private readonly IBundlePayRecordService _bundlePayRecordService;  //za kupovinu

        public BundlePayRecordController(ITourBundleService tourBundleSErvice, IBundlePayRecordService bundlePaymentRecordService)
        {
            _tourBundleService = tourBundleSErvice;
            _bundlePayRecordService = bundlePaymentRecordService;
        }



        [HttpPost("{tourBundleId:int}/{turistId:int}")]   //vidi u publictourpointrequestController
        public ActionResult<BundlePayRecordDto> TourBundlePurchase(int tourBundleId, int touristId)  //da li mi treba povratna vr uopste
        {
            var result = _bundlePayRecordService.BundlePurchase(tourBundleId, touristId);
            return CreateResponse(result);
        }

    }
}
