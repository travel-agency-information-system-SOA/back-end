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

        private readonly ICrudRepository<Problem> _repository;
        private readonly IMapper _mapper;
        //public ProblemService(ICrudRepository<Problem> repository, IMapper mapper) : base(repository, mapper)
        //{
        //    _repository = repository;
        //    _mapper = mapper;
        //}
    
        //public Result<PagedResult<ProblemDto>> GetByUserId(int page, int pageSize, int id)
        //{
        //    var allProblems= GetPaged(page, pageSize).Value;
        //    var filteredProblems = allProblems.Results.Where(problem => problem.Id == id).ToList();
        //    var pagedResult=new PagedResult<ProblemDto>(filteredProblems, filteredProblems.Count);
        //    return Result.Ok(pagedResult);
        //}
    }
}