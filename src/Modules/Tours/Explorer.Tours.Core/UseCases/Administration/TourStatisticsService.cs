using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Public.ShoppingCart;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourExecuting;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.UseCases.Authoring;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourStatisticsService: ITourStatisticsService
    {
        private readonly ITourPurchaseTokenService _tourPurchaseTokenService;
        private readonly IMapper _mapper;
        private readonly ITourExecutionService _tourExecutionService;

        public TourStatisticsService(ITourPurchaseTokenService tourPurchaseTokenService, IMapper mapper, ITourExecutionService tourExecutionService) { 
            this._tourPurchaseTokenService = tourPurchaseTokenService;
            this._mapper = mapper;
            this._tourExecutionService = tourExecutionService;
        }
        
        //getNumberOfPurchasedToursByAuthor
        public int GetNumberOfPurchaseByAuthor(int authorId) {

            List<TourDTO> purchasedTours = new List<TourDTO>();
            purchasedTours = _tourPurchaseTokenService.GetAllPurchasedToursByAuthor(authorId);
            purchasedTours=purchasedTours.Distinct().ToList();
            return purchasedTours.Count;

        }

        public int GetNumberOfCompletedByAuthor(int authorId) {
            List<TourDTO> purchasedTours = new List<TourDTO>();
            purchasedTours = _tourPurchaseTokenService.GetAllPurchasedToursByAuthor(authorId);
            int count = 0;
            foreach (var tour in purchasedTours)
            {
                if (_tourExecutionService.IsTourFinished(tour.Id)) {
                    count++;
                }
            }
            return count;

        }

        public int GetNumberOfStartedByAuthor(int authorId)
        {
            List<TourDTO> purchasedTours = new List<TourDTO>();
            purchasedTours = _tourPurchaseTokenService.GetAllPurchasedToursByAuthor(authorId);
            int count = 0;
            foreach (var tour in purchasedTours)
            {
                if (_tourExecutionService.IsTourStarted(tour.Id))
                {
                    count++;
                }
            }
            return count;

        }

        public int GetNumberOfPurchaseByTour(int authorId,int tourId)
        {
            int count = 0;

            List<TourDTO> purchasedTours = new List<TourDTO>();
            purchasedTours = _tourPurchaseTokenService.GetAllPurchasedToursByAuthor(authorId);
            foreach (TourDTO tour in purchasedTours) {
                if (tour.Id==tourId) {
                    count++;
                }
            }
            return count;

        }

        public int GetNumberOfStartedByTour(int authorId,int tourId)
        {
            List<TourDTO> purchasedTours = new List<TourDTO>();
            purchasedTours = _tourPurchaseTokenService.GetAllPurchasedToursByAuthor(authorId);
            int count = 0;
            foreach (var tour in purchasedTours)
            {
                if (_tourExecutionService.IsTourStarted(tour.Id) && tour.Id==tourId)
                {
                    count++;
                }
            }
            return count;

        }

        public int GetNumberOfCompletedByTour(int authorId,int tourId)
        {
            List<TourDTO> purchasedTours = new List<TourDTO>();
            purchasedTours = _tourPurchaseTokenService.GetAllPurchasedToursByAuthor(authorId);
            int count = 0;
            foreach (var tour in purchasedTours)
            {
                if (_tourExecutionService.IsTourFinished(tour.Id) && tour.Id==tourId)
                {
                    count++;
                }
            }
            return count;

        }

    }
}
