using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/accounts")]
    public class AccountManagementController : BaseApiController
    {
        private readonly IAccountManagementService _accountManagementService;

        public AccountManagementController(IAccountManagementService accountManagementService)
        {
            _accountManagementService = accountManagementService;
        }

        [HttpGet]
        public ActionResult<List<AccountDto>> GetAllAccounts()
        {
            var result = _accountManagementService.GetAllAccounts();
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<AccountDto> BlockOrUnblock([FromBody] AccountDto account)
        {
            var result = _accountManagementService.BlockOrUnblock(account);
            return CreateResponse(result);
        }
    }
}
