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

namespace Explorer.Encounters.Core.UseCases;

public class HiddenLocationEncounterService : CrudService<HiddenLocationEncounterDto, HiddenLocationEncounter> , IHiddenLocationEncounterService
{
    public HiddenLocationEncounterService(ICrudRepository<HiddenLocationEncounter> crudRepository, IMapper mapper) : base(crudRepository, mapper)
    {
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
}
