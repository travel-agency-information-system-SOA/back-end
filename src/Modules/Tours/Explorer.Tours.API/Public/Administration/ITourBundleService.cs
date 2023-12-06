using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ITourBundleService
    {
        Result<TourBundleDto> Create(TourBundleDto tourBundleDto);
        Result<PagedResult<TourBundleDto>> GetPaged(int page, int pageSize);
        Result<TourBundleDto> Update(TourBundleDto tourBundleDto);
        Result Delete(int id);

        Result<PagedResult<TourDTO>> GetToursByBundle(List<int> TourIds);


        Result<PagedResult<TourBundleDto>> GetPublishedBundles(int page, int pageSize);

    }
}
