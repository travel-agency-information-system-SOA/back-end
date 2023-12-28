using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class SocialEncounterService : CrudService<SocialEncounterDto, SocialEncounter>, ISocialEncounterService
    {
        private readonly IEncounterExecutionService _encounterExecutionService;
        private readonly IEncounterService _encounterService;
        private readonly IUserPositionService _userPositionService;
        public SocialEncounterService(ICrudRepository<SocialEncounter> crudRepository, IEncounterExecutionService encounterExecutionService, IEncounterService encounterService, IUserPositionService userPositionService, IMapper mapper) : base(crudRepository, mapper)
        {
            _encounterExecutionService = encounterExecutionService;
            _encounterService = encounterService;
            _userPositionService = userPositionService;
        }

        public void CheckSocialEncounter(int encounterId)
        {
            SocialEncounterDto socialEncounter = GetSocialEncounter(Convert.ToInt32(encounterId));
            EncounterDto encounterDto = _encounterService.GetEncounter(Convert.ToInt32(encounterId));
            List<EncounterExecutionDto> executions = _encounterExecutionService.GetExecutionsByEncounter(Convert.ToInt32(encounterDto.Id));
           
            int count = 0;
            List<long> usersInside = new List<long>();
            foreach(EncounterExecutionDto execution in executions) 
            {
                UserPositionDto position = _userPositionService.GetByUserId(Convert.ToInt32(execution.UserId), 0, 0).Value;

                if (_encounterService.CalculateDistance(position.Latitude, position.Longitude, encounterDto.Latitude, encounterDto.Longitude) <= socialEncounter.DistanceTreshold)
                {
                    count++;
                    usersInside.Add(position.UserId);
                }
            }

            if(count >= socialEncounter.TouristsRequiredForCompletion) 
            {
                foreach(long id in usersInside)
                {
                    _encounterExecutionService.CompleteEncounter(Convert.ToInt32(id));
                }
            }
        }

        public SocialEncounterDto GetSocialEncounter(int encounterId) 
        {
            List<SocialEncounter> encounters = new List<SocialEncounter>();
            encounters = CrudRepository.GetPaged(0,0).Results.ToList();
            foreach(var encounter  in encounters)
            {
                if(encounter.EncounterId == encounterId)
                {
                    return MapToDto(encounter);
                }
            }

            return null;
        }




        public long GetId(long encounterId)
        {
            // Assuming EncounterId is a property in your SocialEncounterDto
            var pagedResult = CrudRepository.GetPaged(1, int.MaxValue);  // Fetch all records

            var socialEncounter = pagedResult.Results.FirstOrDefault(s => s.EncounterId == encounterId);

            if (socialEncounter != null)
            {
                return socialEncounter.Id;
            }

            // Handle the case when EncounterId doesn't match any SocialEncounter
            // You might want to throw an exception or return a specific value based on your requirements.
            // For simplicity, let's return -1 indicating not found.
            return -1;
        }
    }
}
