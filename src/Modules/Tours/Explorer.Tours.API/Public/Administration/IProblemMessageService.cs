using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration;

public interface IProblemMessageService
{
    Result<PagedResult<ProblemMessageDto>> GetPaged(int page, int pageSize);
    Result<ProblemMessageDto> Create(ProblemMessageDto problem);
    Result<ProblemMessageDto> Update(ProblemMessageDto problem);
    Result Delete(int id);
    Result<PagedResult<ProblemMessageDto>> GetAllByProblemId(int id, int page, int pageSize);
    bool IsThereNewMessages(int loggedUserId, int problemId, int page, int pageSize);
}
