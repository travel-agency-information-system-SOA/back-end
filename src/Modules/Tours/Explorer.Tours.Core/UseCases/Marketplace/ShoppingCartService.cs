using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Marketplace;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Marketplace
{
    public class ShoppingCartService : CrudService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
    {
       
        public ShoppingCartService(ICrudRepository<ShoppingCart> repository, IMapper mapper) : base(repository,mapper)
        {
        }


        /// ovo lepo popuniti 
        public Result<PagedResult<ShoppingCartDto>> GetAll(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Result<PagedResult<ShoppingCartDto>> GetByUserId(int touristId, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        Result<ShoppingCartDto> IShoppingCartService.Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
