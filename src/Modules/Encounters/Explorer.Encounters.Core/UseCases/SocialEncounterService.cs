using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
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
        public SocialEncounterService(ICrudRepository<SocialEncounter> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {

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
