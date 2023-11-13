using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourExecutionsDTO;
using Explorer.Tours.API.Public.TourExecuting;
using Explorer.Tours.Core.Domain.TourExecutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourExecuting
{
    public class TourExecutionPositionService : CrudService<TourExecutionPositionDto, TourExecutionPosition>, ITourExecutionPositionService
    {
        public TourExecutionPositionService(ICrudRepository<TourExecutionPosition> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }
    }
}
