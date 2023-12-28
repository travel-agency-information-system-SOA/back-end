using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class FollowerService : CrudService<FollowerDto, Follower>, IFollowerService
    {
        private readonly IFollowerRepository _repository;
        public FollowerService(ICrudRepository<Follower> repository, IFollowerRepository followerRepository, IMapper mapper) : base(repository, mapper)
        {
            _repository = followerRepository;
        }

        public Result<List<FollowerDto>> GetByUserId(int userId)
        {
            try
            {
                var result = _repository.GetByUserId(userId);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
