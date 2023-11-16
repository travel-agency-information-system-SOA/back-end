using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Problems;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.API.Public.Administration;

namespace Explorer.Tours.Core.UseCases.Administration;

public class ProblemService : CrudService<ProblemDto, Problem>, IProblemService
{
    private readonly IMapper _mapper;
    private readonly IProblemRepository _repository;
    private readonly IProblemMessageService _messageService;
    public ProblemService(ICrudRepository<Problem> repository, IProblemRepository problemRepository, IMapper mapper, IProblemMessageService messageService) : base(repository, mapper)
    {
        _mapper = mapper;
        _repository = problemRepository;
        _messageService = messageService;
    }

    public Result<PagedResult<ProblemDto>> GetByTouristId(int userId, int page, int pageSize)
    {
        var problems = _repository.GetByTouristId(userId, page, pageSize);
        return MapToDto(problems);
    }

    public Result<PagedResult<ProblemDto>> GetByGuideId(int tourId, int page, int pageSize)
    {
        var problems = _repository.GetByGuideId(tourId, page, pageSize);
        return MapToDto(problems);
    }

    public int IsThereUnreadMessages(int userId, int page, int pageSize)
    {
        var problems = _repository.GetProblemsOfUser(userId, page, pageSize);
        foreach (var problem in problems.Results)
        {
            if (_messageService.IsThereNewMessages(userId, (int)problem.Id, page, pageSize))
            {
                return (int)problem.Id;
            }
            
        }
        return 0;
    }
    
    public Result<PagedResult<ProblemDto>> GetUnsolvedProblems(int page, int pageSize)
    {
        var problems = _repository.GetUnsolvedProblems(page, pageSize);
        return MapToDto(problems);
    }

    public Result<ProblemDto> getGuideProblemWithClosestDeadline(int id, int page, int pageSize)
    {
        var problems = _repository.GetByGuideId(id, page, pageSize);

        if (problems.Results.Any())
        {
            var closestProblem = problems.Results.OrderBy(prob => Math.Abs((prob.Deadline - DateTime.Now).TotalSeconds)).First();
            return MapToDto(closestProblem);
        }

        return null;

    }
}