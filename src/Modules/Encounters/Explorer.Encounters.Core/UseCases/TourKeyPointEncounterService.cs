using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class TourKeyPointEncounterService : CrudService<TourKeyPointEncounterDto, TourKeyPointEncounter>, ITourKeyPointEncounterService
    {
        public TourKeyPointEncounterService(ICrudRepository<TourKeyPointEncounter> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }

        public int GetEncounterIdByTourPoint(int tourPointId) {
            var tourKeyPointEncounters = CrudRepository.GetPaged(0, 0).Results;
            foreach (var encounter in tourKeyPointEncounters) {
                if (encounter.KeyPointId==tourPointId) {
                    return encounter.EncounterId;
                }
            }

            return 0;
        }
    }
}
