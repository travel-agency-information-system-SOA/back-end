using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Explorer.Tours.API;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;


namespace Explorer.Tours.Core.UseCases.Administration
{
    public class ProblemService : CrudService<ProblemDto, Problem>, IProblemService
    {
        private readonly IProblemMessageService _messageService;
        public ProblemService(ICrudRepository<Problem> repository, IMapper mapper, IProblemMessageService messageService) : base(repository, mapper)
        {
            _messageService = messageService;     
        }

        public Result<PagedResult<ProblemDto>> GetByTouristId(int userId, int page, int pageSize)
        {
            var allProblems = CrudRepository.GetPaged(page, pageSize);
            var filteredProblems = allProblems.Results.Where(prob => prob.IdTourist == userId);
            var filteredPagedResult = new PagedResult<Problem>(filteredProblems.ToList(), filteredProblems.Count());
            return MapToDto(filteredPagedResult);
        }

        public int IsThereUnreadMessages(int userId, int page, int pageSize)
        {
            var allProblems = CrudRepository.GetPaged(page, pageSize);
            foreach(var problem in allProblems.Results)
            {
                if(problem.IdGuide == userId || problem.IdTourist == userId)
                {
                    if(_messageService.IsThereNewMessages(userId, (int)problem.Id, page, pageSize)){
                        return (int)problem.Id;
                    }
                }
            }
            return 0;
        }

        public Result<PagedResult<ProblemDto>> GetByGuideId(int guideId, int page, int pageSize)
        {
            var allProblems = CrudRepository.GetPaged(page, pageSize);
            var filteredProblems = allProblems.Results.Where(prob => prob.IdGuide == guideId);
            var filteredPagedResult = new PagedResult<Problem>(filteredProblems.ToList(), filteredProblems.Count());
            return MapToDto(filteredPagedResult);
        }

        public Result<PagedResult<ProblemDto>> GetUnsolvedProblems(int page, int pageSize)
        {
            var allProblems = CrudRepository.GetPaged(page, pageSize);
            var filteredProblems = allProblems.Results.Where(prob => prob.IsSolved == false);
            var filteredPagedResult = new PagedResult<Problem>(filteredProblems.ToList(), filteredProblems.Count());
            return MapToDto(filteredPagedResult);
        }
    }
}