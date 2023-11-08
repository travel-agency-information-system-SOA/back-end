using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IProblemMessageService
    {
        Result<PagedResult<ProblemMessageDto>> GetPaged(int page, int pageSize);
        Result<ProblemMessageDto> Create(ProblemMessageDto problem);
        Result<ProblemMessageDto> Update(ProblemMessageDto problem);
        Result Delete(int id);
        Result<ProblemMessageDto> Get(int id);
    }
}
