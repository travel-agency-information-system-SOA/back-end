using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public;

public interface IHiddenLocationEncounterService
{
    Result<PagedResult<HiddenLocationEncounterDto>> GetPaged(int page, int pageSize);
    Result<HiddenLocationEncounterDto> Create(HiddenLocationEncounterDto encounter);
    Result<HiddenLocationEncounterDto> Update(HiddenLocationEncounterDto encounter);
    Result Delete(int id);
}
