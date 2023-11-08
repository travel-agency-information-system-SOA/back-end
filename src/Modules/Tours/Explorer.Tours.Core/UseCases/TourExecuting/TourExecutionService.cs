using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourExecutionsDTO;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourExecuting;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourExecuting
{
    public class TourExecutionService : CrudService<TourExecutionDto, TourExecution>, ITourExecutionService
    {
        public TourExecutionService(ICrudRepository<TourExecution> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }
    }
}

