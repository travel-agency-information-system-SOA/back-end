using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.API.Public;

public interface IEncounterExecutionService
{
    Result<PagedResult<EncounterExecutionDto>> GetPaged(int page, int pageSize);
    Result<EncounterExecutionDto> Create(EncounterExecutionDto encounter);
    Result<EncounterExecutionDto> Update(EncounterExecutionDto encounter);
    Result Delete(int id);
}
