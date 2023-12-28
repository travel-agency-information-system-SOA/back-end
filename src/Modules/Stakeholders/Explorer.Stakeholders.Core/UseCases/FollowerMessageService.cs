using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class FollowerMessageService : CrudService<FollowerMessageDto, FollowerMessage>, IFollowerMessageService
    {
        private readonly IFollowerMessageRepository _repository;
        public FollowerMessageService(ICrudRepository<FollowerMessage> repository, IFollowerMessageRepository followerRepository, IMapper mapper) : base(repository, mapper)
        {
            _repository = followerRepository;
        }

        public Result<List<FollowerMessageDto>> GetByFollowerId(int followerId)
        {
            try
            {
                var result = _repository.GetByFollowerId(followerId);
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

        public Result<FollowerMessageDto> MarkAsRead(FollowerMessageDto messageDto)
        {
            try
            {
                messageDto.IsRead = true;
                return Update(messageDto);
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

