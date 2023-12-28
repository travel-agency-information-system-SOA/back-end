using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingCartDtos;
using Explorer.Payments.API.Public.ShoppingCart;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases.ShoppingCarts
{
    public class TourPurchaseTokenService : CrudService<TourPurchaseTokenDto, TourPurchaseToken>, ITourPurchaseTokenService
    {
        private readonly ITourService _tourService;
        public TourPurchaseTokenService(ICrudRepository<TourPurchaseToken> crudRepository, IMapper mapper,ITourService tourService) : base(crudRepository, mapper)
        {
            this._tourService = tourService;
        }

        /*public Result<List<TourDTO>> GetPurchasedTours(int touristId)
        {
            //dobaviti sve tokene
            var tokens = CrudRepository.GetPaged(1, int.MaxValue).Results.Where(token => token.TouristId == touristId);
            //za svaki token izbaciti njegovu turu
            var purchacedTours = new List<TourDTO>();
            foreach (var token in tokens)
            {
                TourDTO tourDto = _internalTourService.GetTourByTourId(token.IdTour).Value;
                purchacedTours.Add(tourDto);
            }
            //dodati u enum stanje koje proverava da li je poceta ili ne ( ili kako vec treba)
            return purchacedTours;

        }*/

        //added for tourStatistics
        public List<TourDTO> GetAllPurchasedToursByAuthor(int authorId)
       {
           
           var tokens = CrudRepository.GetPaged(1, int.MaxValue).Results;
           
           var purchacedTours = new List<TourDTO>();
           foreach (var token in tokens)
           {
               TourDTO tourDto = _tourService.GetTourByTourId(token.IdTour).Value;
                if (tourDto.UserId==authorId) {
                    purchacedTours.Add(tourDto);
                }
           }

           return purchacedTours;

       }


    }
}
