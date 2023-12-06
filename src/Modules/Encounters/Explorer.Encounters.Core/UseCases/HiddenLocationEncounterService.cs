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
}
