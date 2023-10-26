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
        private readonly ICrudRepository<Problem> _repository;
        private readonly IMapper _mapper;
        public ProblemService(ICrudRepository<Problem> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        //ovo ne znam dal treba
        public Result<ProblemDto> Report(ProblemDto dto)
        {
            throw new NotImplementedException();
        }
        public Result<List<ProblemDto>> GetByUserId(int userId, int page, int pageSize)
        {
            var allProblems=CrudRepository.GetPaged(page, pageSize);
            List<Problem>filteredProblem=allProblems.Results.Where(problem=>problem.Id==userId).ToList();
            return MapToDto(filteredProblem);
        }

        public Result<List<ProblemDto>> GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}