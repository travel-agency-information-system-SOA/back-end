using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public;

public interface ISocialEncounterService 
{
    Result<PagedResult<SocialEncounterDto>> GetPaged(int page, int pageSize);
    Result<SocialEncounterDto> Create(SocialEncounterDto socialEncounter);
    Result<SocialEncounterDto> Update(SocialEncounterDto socialEncounter);
    Result Delete(int id);
    public void CheckSocialEncounter(int executionId);
    public SocialEncounterDto GetSocialEncounter(int encounterId);
    public long GetId(long encounterId);
}

