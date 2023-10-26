using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{

    public class ClubService : CrudService<ClubDto, Club>, IClubService
    {
        public ClubService(ICrudRepository<Club> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<PagedResult<ClubDto>> GetByUserId(int userId, int page, int pageSize)
        {
            var allClubs = CrudRepository.GetPaged(page, pageSize);
            var filteredClubs = allClubs.Results.Where(club => club.OwnerId == userId);
            var filteredPagedResult = new PagedResult<Club>(filteredClubs.ToList(), filteredClubs.Count());
            return MapToDto(filteredPagedResult);

        }
    }
}
