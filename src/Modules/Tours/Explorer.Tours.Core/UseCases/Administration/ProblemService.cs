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
        public ProblemService(ICrudRepository<Problem> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<PagedResult<ProblemDto>> GetByTouristId(int userId, int page, int pageSize)
        {
            var allProblems = CrudRepository.GetPaged(page, pageSize);
            var filteredProblems = allProblems.Results.Where(prob => prob.IdTourist == userId);
            var filteredPagedResult = new PagedResult<Problem>(filteredProblems.ToList(), filteredProblems.Count());
            return MapToDto(filteredPagedResult);
        }

        public Result<PagedResult<ProblemDto>> GetByTourId(int tourId, int page, int pageSize)
        {
            var allProblems = CrudRepository.GetPaged(page, pageSize);
            var filteredProblems = allProblems.Results.Where(prob => prob.IdTour == tourId);
            var filteredPagedResult = new PagedResult<Problem>(filteredProblems.ToList(), filteredProblems.Count());
            return MapToDto(filteredPagedResult);
        }
    }
}