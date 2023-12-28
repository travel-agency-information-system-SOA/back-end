using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases;

public class EncounterExecutionService:CrudService<EncounterExecutionDto, EncounterExecution>, IEncounterExecutionService
{
    private readonly IEncounterExecutionRepository _encounterExecutionRepository;
    private readonly ITouristXPService _touristXPService;
    private readonly IEncounterService _encounterService;
    private readonly ITourKeyPointEncounterService _keyPointEncounterService;
    public EncounterExecutionService(ICrudRepository<EncounterExecution> crudRepository, IEncounterService encounterService, IEncounterExecutionRepository encounterExecutionRepository, ITouristXPService touristXPService, IMapper mapper,ITourKeyPointEncounterService keyPointEncounterService) : base(crudRepository, mapper)
    {
        _encounterExecutionRepository = encounterExecutionRepository;
        _touristXPService = touristXPService;
        _encounterService = encounterService;
        _keyPointEncounterService = keyPointEncounterService;
    }

    public List<EncounterExecutionDto> GetExecutionsByEncounter(int encounterId)
    {
        List<EncounterExecution> executions = new List<EncounterExecution>();
        executions = CrudRepository.GetPaged(0,0).Results.ToList();

        List<EncounterExecutionDto> executionsDto = new List<EncounterExecutionDto>();
        foreach(var execution  in executions)
        {
            if(execution.EncounterId == encounterId && execution.IsCompleted == false)
            {
                executionsDto.Add(MapToDto(execution));
            }
        }


        return executionsDto;
    }

    public EncounterExecutionDto GetExecution(int executionId) 
    { 
        List<EncounterExecution> executions = new List<EncounterExecution>();
        executions = CrudRepository.GetPaged(0,0).Results.ToList();
        foreach (var execution in executions)
        {
            if (execution.Id == executionId)
            {
                return MapToDto(execution);
            }
        }

        return null;
    }

    public Result<EncounterExecutionDto> GetExecutionByUser(int userId)
    {
        List<EncounterExecution> executions = new List<EncounterExecution>();
        executions = CrudRepository.GetPaged(0, 0).Results.ToList();
        foreach (var execution in executions)
        {
            if (execution.UserId == userId && execution.IsCompleted==false) 
            {
                return MapToDto(execution);
            }
        }

        return null;
    }

    public Result<EncounterExecutionDto> CompleteEncounter(long userId)
    {
        EncounterExecutionDto execution = GetExecutionByUser(Convert.ToInt32(userId)).Value;
        execution.CompletionTime = DateTime.UtcNow;
        execution.IsCompleted = true;
        EncounterDto encounterDto = new EncounterDto();
        encounterDto = _encounterService.GetEncounter(Convert.ToInt32(execution.EncounterId));
        _touristXPService.AddExperience(Convert.ToInt32(userId), encounterDto.XpPoints);
        _encounterExecutionRepository.Update(MapToDomain(execution));
        return execution;
    }

    public List<EncounterExecutionDto> GetAllExecutionsByEncounter(int encounterId)
    {
        List<EncounterExecution> executions = new List<EncounterExecution>();
        executions = CrudRepository.GetPaged(0, 0).Results.ToList();

        List<EncounterExecutionDto> executionsDto = new List<EncounterExecutionDto>();
        foreach (var execution in executions)
        {
            if (execution.EncounterId == encounterId)
            {
                executionsDto.Add(MapToDto(execution));
            }
        }


        return executionsDto;
    }

    //za statistiku dodato

    public bool IsEncounterForTourPointCompleted(int touristId, int tourPointId) {

        //valja svaki tourPoint ima samo jedan encounter...trebalo bi
        var encounterIdForTourPoint = _keyPointEncounterService.GetEncounterIdByTourPoint(tourPointId);
       List<EncounterExecution> completedEncounters = new List<EncounterExecution>();
        var executions = CrudRepository.GetPaged(0, 0).Results.ToList();

        foreach (var execution in executions)
        {
            if (execution.UserId == touristId && execution.IsCompleted == true)
            {
                completedEncounters.Add(execution);
            }
        }

        foreach (var execution in completedEncounters) {
            if (execution.Id == encounterIdForTourPoint) {
                return true;
            }
        }
        return false;
    }
}
