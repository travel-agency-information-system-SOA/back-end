using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IPublicTourPointService
    {
        Result<PagedResult<PublicTourPointDto>> GetPaged(int page, int pageSize);
        Result<PublicTourPointDto> CreatePublicTourPointAndAcceptRequest(int requestId, int tourPointId,string comment);
        Result<PublicTourPointDto> Update(PublicTourPointDto publicTourPoint);
        Result Delete(int id);
        Result<PagedResult<PublicTourPointDto>> GetTourPointsByTourId(int tourId);
        Result<PublicTourPointDto> Get(int id); 
    }
}
