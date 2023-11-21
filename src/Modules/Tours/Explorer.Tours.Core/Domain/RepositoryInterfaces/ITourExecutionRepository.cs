using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.TourExecutions;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourExecutionRepository
    {
        public PagedResult<TourExecution> GetById(int id, int page, int pageSize);
        public TourExecution GetById(int id);
        public Result Update(TourExecution updatedTourExecution);
        public TourExecution GetByUser(int userId);
        public int Create(int userId, int tourId);
        public void CreatePoint(int executionId, int tourPointId);
        public void CreatePosition(double longitude, double latitude, int executionId);

        PagedResult<TourExecution> GetAll(int page, int pageSize);
    }
}
