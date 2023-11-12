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

		Tour GetById(int id);

		Result DeleteAgreggate(int id);

	}
}
