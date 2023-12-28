using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public;

public interface IEncounterService
{
    Result<PagedResult<EncounterDto>> GetPaged(int page, int pageSize);
    Result<EncounterDto> Create(EncounterDto encounter);
    Result<EncounterDto> Update(EncounterDto encounter);
    Result Delete(int id);
    public EncounterDto GetEncounter(int encounterId);
    public double CalculateDistance(double userLat, double userLon, double pointLat, double pointLon);
    public Result<EncounterDto> GetEncounterById(int encounterId);
    public Result<List<EncounterDto>> GetAll();
}
