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
    }
}
