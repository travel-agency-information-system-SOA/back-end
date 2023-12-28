using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourExecutionsDTO;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourExecuting;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.UseCases.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourExecuting
{
    public class TourPointExecutionService : CrudService<TourPointExecutionDto, TourPointExecution>, ITourPointExecutionService
    {
        public TourPointExecutionService(ICrudRepository<TourPointExecution> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }

        public bool isTourPointCompletedByTourist(int touristId,int tourPointId) {
            var tourPointExecutions = CrudRepository.GetPaged(0, 0).Results;

            foreach (TourPointExecution tourPointEx in tourPointExecutions ) {
                if (tourPointEx.ТоurExecution.UserId == touristId && tourPointEx.TourPointId == tourPointId && tourPointEx.Completed==true) {
                    return true;
                }
            }
            return false;
        }

        public int getMaxCompletedTourPointPerTourist(int touristId,List<long> tourExecutions)
        {
            var tourPointExecutions = CrudRepository.GetPaged(0, 0).Results;
            int max = 0;
            List<TourPointExecution> filteredTourPointExecutions = new List<TourPointExecution>();
            foreach (int id in tourExecutions)
            {
                foreach (TourPointExecution tourPointEx in tourPointExecutions)
                {
                    if (tourExecutions.Contains(tourPointEx.TourExecutionId))
                    {
                        //samo za tourExecutions od konkretne ture
                        filteredTourPointExecutions.Add(tourPointEx);
                    }


                }

                
            }

            foreach (TourPointExecution tourPointEx in filteredTourPointExecutions) {
                if (tourPointEx.Completed == true && tourPointEx.ТоurExecution.UserId == touristId) {
                    if (tourPointEx.TourPointId > max) {
                        max = tourPointEx.TourPointId;
                    }
                }
            }
            return max;

        }

    }
}
