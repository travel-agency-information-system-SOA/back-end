using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourRepository
	{
		PagedResult<Tour> GetByUserId(int userId, int page, int pageSize);

        PagedResult<Tour> GetAllPublished(int page, int pageSize);

        public Tour GetByTourId(int tourId);

        PagedResult<Tour> GetAll( int page, int pageSize);

		

		Tour GetById(int tourId);
        Result DeleteAgreggate(int id);



    }
}
