using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.Problems;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases;

public class ProblemMessageService : CrudService<ProblemMessageDto, ProblemMessage>, IProblemMessageService
{
    public ProblemMessageService(ICrudRepository<ProblemMessage> repository, IMapper mapper) : base(repository, mapper) { }

    public Result<PagedResult<ProblemMessageDto>> GetAllByProblemId(int id, int page, int pageSize)
    {
        var allMessages= CrudRepository.GetPaged(page, pageSize);
        var filteredMessages = allMessages.Results.Where(mess => mess.ProblemId == id);
        var filteredPagedResult = new PagedResult<ProblemMessage>(filteredMessages.ToList(), filteredMessages.Count());
        return MapToDto(filteredPagedResult);
    }

    public bool IsThereNewMessages(int loggedUserId, int problemId, int page, int pageSize)
    {
        var allMessages = CrudRepository.GetPaged(page, pageSize);
        var filteredMessages = allMessages.Results.Where(mess => mess.ProblemId == problemId);
        foreach(ProblemMessage message in filteredMessages)
        {
            if(message.IsRead == false && message.IdSender != loggedUserId)
            {
                return true;
            }
        }
        return false;
    }

}
