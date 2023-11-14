using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourExecutionsDTO;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.TourExecuting
{
    public interface ITourExecutionService
    {
        public Result<TourExecutionDto> GetById(int tourExecutionId);
        public void UpdatePosition(int tourExecutionId, int longitude, int latitude);
        public void CompleteTourPoint(int tourExecutionId, int tourPointId);
    }
}
