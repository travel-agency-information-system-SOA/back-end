using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases;

public class HiddenLocationEncounterService : CrudService<HiddenLocationEncounterDto, HiddenLocationEncounter> , IHiddenLocationEncounterService
{
    private readonly IEncounterExecutionService _executionService;
    private readonly IEncounterService _encounterService;
    private readonly IUserPositionService _userPositionService;

    public HiddenLocationEncounterService(ICrudRepository<HiddenLocationEncounter> crudRepository, IEncounterExecutionService encounterExecutionService, IEncounterService encounterService, IUserPositionService userPositionService, IMapper mapper) : base(crudRepository, mapper)
    {
        _executionService = encounterExecutionService;
        _encounterService = encounterService;
        _userPositionService = userPositionService;
    }
    public long GetId(long encounterId)
    {
        // Assuming EncounterId is a property in your SocialEncounterDto
        var pagedResult = CrudRepository.GetPaged(1, int.MaxValue);  // Fetch all records

        var hiddenLocationEncounter = pagedResult.Results.FirstOrDefault(s => s.EncounterId == encounterId);

        if (hiddenLocationEncounter != null)
        {
            return hiddenLocationEncounter.Id;
        }

        // Handle the case when EncounterId doesn't match any SocialEncounter
        // You might want to throw an exception or return a specific value based on your requirements.
        // For simplicity, let's return -1 indicating not found.
        return -1;
    }

    public Boolean CheckHiddenLocationEncounter(int executionId, int encounterId)
    {
        EncounterExecutionDto execution = _executionService.GetExecution(executionId);
        EncounterDto encounter = _encounterService.GetEncounter(encounterId);
        HiddenLocationEncounterDto hidden = GetHiddenLocationEncounterByEncounter(Convert.ToInt32(encounter.Id));
        UserPositionDto userPosition = _userPositionService.GetByUserId(Convert.ToInt32(execution.UserId), 0, 0).Value;

        if (_encounterService.CalculateDistance(userPosition.Latitude, userPosition.Longitude, hidden.ImageLatitude, hidden.ImageLongitude) <= hidden.DistanceTreshold)
        {
            return true;
        }

        return false;
    }

    public HiddenLocationEncounterDto GetHiddenLocationEncounterByEncounter(int encounterId)
    {
        List<HiddenLocationEncounter> encounters = new List<HiddenLocationEncounter>();
        encounters = CrudRepository.GetPaged(0, 0).Results.ToList();
        foreach (var encounter in encounters)
        {
            if (encounter.EncounterId == encounterId)
            {
                return MapToDto(encounter);
            }
        }

        return null;
    }

    public Result<HiddenLocationEncounterDto> GetHiddenLocationEncounterByEncounterId(int encounterId)
    {
        List<HiddenLocationEncounter> encounters = new List<HiddenLocationEncounter>();
        encounters = CrudRepository.GetPaged(0, 0).Results.ToList();
        foreach (var encounter in encounters)
        {
            if (encounter.EncounterId == encounterId)
            {
                return MapToDto(encounter);
            }
        }
            return null;
    }
}
