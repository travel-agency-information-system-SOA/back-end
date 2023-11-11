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

namespace Explorer.Stakeholders.Core.UseCases;

public class UserPositionService : CrudService<UserPositionDto, UserPosition>, IUserPositionService
{
    public UserPositionService(ICrudRepository<UserPosition> repository, IMapper mapper) : base(repository, mapper) { }
}