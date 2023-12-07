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
    public List<EncounterExecutionDto> GetExecutionsByEncounter(int encounterId);
    public EncounterExecutionDto GetExecution(int encounterId);
    public Result<EncounterExecutionDto> GetExecutionByUser(int userId);
    public void CompleteEncounter(long userId);
    public List<EncounterExecutionDto> GetAllExecutionsByEncounter(int encounterId);
}
