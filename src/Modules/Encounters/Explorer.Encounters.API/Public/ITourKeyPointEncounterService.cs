using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public
{
    public interface ITourKeyPointEncounterService
    {
        Result<PagedResult<TourKeyPointEncounterDto>> GetPaged(int page, int pageSize);
        Result<TourKeyPointEncounterDto> Create(TourKeyPointEncounterDto tourKeyPointEncounter);
        Result<TourKeyPointEncounterDto> Update(TourKeyPointEncounterDto tourKeyPointEncounter);
        Result Delete(int id);

        public int GetEncounterIdByTourPoint(int tourPointId);
    }
}
