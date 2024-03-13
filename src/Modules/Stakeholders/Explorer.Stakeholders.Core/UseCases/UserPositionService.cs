using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class UserPositionService : CrudService<UserPositionDto, UserPosition>, IUserPositionService
{
    private readonly IMapper _mapper;
    private readonly IUserPositionRepository _repository;
    public UserPositionService(ICrudRepository<UserPosition> repository, IMapper mapper, IUserPositionRepository userPositionRepository) : base(repository, mapper) {
        _mapper = mapper;
        _repository = userPositionRepository;
    }

    public Result<UserPositionDto> GetByUserId(int userId, int page, int pageSize)
    {
        var execution = _repository.GetByUserId(userId, page, pageSize);
        return MapToDto(execution);
    }
}