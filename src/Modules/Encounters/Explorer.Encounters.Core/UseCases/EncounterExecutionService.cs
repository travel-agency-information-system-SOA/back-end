using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases;

public class EncounterExecutionService:CrudService<EncounterExecutionDto, EncounterExecution>, IEncounterExecutionService
{
    private readonly IEncounterExecutionRepository _encounterExecutionRepository;
    public EncounterExecutionService(ICrudRepository<EncounterExecution> crudRepository, IEncounterExecutionRepository encounterExecutionRepository, IMapper mapper) : base(crudRepository, mapper)
    {
        _encounterExecutionRepository = encounterExecutionRepository;
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

    public void CompleteEncounter(long userId)
    {
        EncounterExecutionDto execution = GetExecutionByUser(Convert.ToInt32(userId)).Value;
        execution.CompletionTime = DateTime.UtcNow;
        execution.IsCompleted = true;
        
        _encounterExecutionRepository.Update(MapToDomain(execution));

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

}
