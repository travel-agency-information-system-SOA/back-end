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

namespace Explorer.Encounters.Core.UseCases;

public class EncounterExecutionService:CrudService<EncounterExecutionDto, EncounterExecution>, IEncounterExecutionService
{
    public EncounterExecutionService(ICrudRepository<EncounterExecution> crudRepository, IMapper mapper) : base(crudRepository, mapper)
    {
    }

}
