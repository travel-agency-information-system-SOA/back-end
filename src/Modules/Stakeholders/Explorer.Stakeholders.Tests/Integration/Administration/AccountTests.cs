using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class AccountTests : BaseStakeholdersIntegrationTest
    {
        public AccountTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Get_all_accounts()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            //Act
            var result = ((ObjectResult)controller.GetAllAccounts().Result)?.Value as List<AccountDto>;

            //Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(7);

        }

        [Fact]
        public void Block_account()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedAccount = new AccountDto
            {
                UserId = -12,
                Username = "autor2@gmail.com",
                Password = "autor2",
                Role = "author",
                Email = "autor2@gmail.com",
                IsActive = true,
            };

            //Act
            var result = ((ObjectResult)controller.BlockOrUnblock(updatedAccount).Result)?.Value as AccountDto;

            //Assert - Response
            result.ShouldNotBeNull();
            result.UserId.ShouldBe(-12);

            //Assert - Database
            var storedEntity = dbContext.Users.FirstOrDefault(u => u.Id == result.UserId);
            storedEntity.ShouldNotBeNull();
            storedEntity.IsActive.ShouldBe(false);

        }

        [Fact]
        public void Unblock_account()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedAccount = new AccountDto
            {
                UserId = -11,
                Username = "autor1@gmail.com",
                Password = "autor1",
                Role = "author",
                Email = "autor1@gmail.com",
                IsActive = false,
            };

            //Act
            var result = ((ObjectResult)controller.BlockOrUnblock(updatedAccount).Result)?.Value as AccountDto;

            //Assert - Response
            result.ShouldNotBeNull();
            result.UserId.ShouldBe(-11);

            //Assert - Database
            var storedEntity = dbContext.Users.FirstOrDefault(u => u.Id == result.UserId);
            storedEntity.ShouldNotBeNull();
            storedEntity.IsActive.ShouldBe(true);

        }


        private static AccountManagementController CreateController(IServiceScope scope)
        {
            return new AccountManagementController(scope.ServiceProvider.GetRequiredService<IAccountManagementService>());
        }
    }
}
