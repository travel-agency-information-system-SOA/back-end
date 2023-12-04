using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.TourBundle;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourBundleService: CrudService<TourBundleDto, TourBundle>, ITourBundleService
    {
        private readonly ITourService _tourService;
        public TourBundleService(ICrudRepository<TourBundle> repository, IMapper mapper, ITourService tourService) : base(repository, mapper) {
            this._tourService = tourService;
        }

        public Result<PagedResult<TourDTO>> GetToursByBundle(List<int> tourIds)
        {
            List<TourDTO> tours = new List<TourDTO>();

            foreach (int tourId in tourIds) {
                var tourResult= this._tourService.GetTourByTourId(tourId);

                if (tourResult.IsSuccess) {
                    tours.Add(tourResult.Value);
                }
            }

            var filteredPagedResult = new PagedResult<TourDTO>(tours, tours.Count());

            return Result.Ok(filteredPagedResult);
        }
    }

    
}
