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
	public interface ITourService
	{
		Result<TourDTO> Create(TourDTO tourDto);

		Result Delete(int id);

		Result<PagedResult<TourDTO>> GetByUserId(int userId, int page, int pageSize);

		Result<TourDTO> Update(TourDTO tourDto);

		Result SetTourCharacteristic(int tourId, int distance, TimeSpan duration, string transposrtType);

		
	}
}
