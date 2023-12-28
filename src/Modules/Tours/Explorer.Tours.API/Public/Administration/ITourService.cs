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
        Result<TourDTO> GetTourByTourId(int id);
       

        TourDTO GetTourById(int id);

        Result<TourDTO> Update(TourDTO tourDto);

        Result<PagedResult<TourDTO>> GetByRange(double lat, double lon, int range, int type, int page, int pageSize);

        Result<PagedResult<TourDTO>> GetByLevelAndPrice(string level, int price, int page, int pageSize);

        Result<PagedResult<TourDTO>> GetPublished();

		Result SetTourCharacteristic(int tourId, double distance, double duration, string transposrtType);
        Result Publish(int tourId);
		Result ArchiveTour(int tourId);

		Result DeleteAggregate(int id);

		Result<TourDTO> Get(int id);
		Result<PagedResult<TourDTO>> GetAll(int page, int pageSize);
		Result<PagedResult<TourDTO>> GetAllPublishedByAuthor(int id, int page, int pageSize);
		Result<PagedResult<TourDTO>> FilterToursByPublicTourPoints(PublicTourPointDto[] publicTourPoints, int page, int pageSize);
		long GetLastTourId(int page, int pageSize);
	}
}
