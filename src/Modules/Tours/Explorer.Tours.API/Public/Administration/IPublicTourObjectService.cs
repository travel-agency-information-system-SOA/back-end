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
    public interface IPublicTourObjectService
    {
        Result<PagedResult<PublicTourObjectDto>> GetPaged(int page, int pageSize);
        Result<PublicTourObjectDto> CreatePublicTourObjectAndAcceptRequest(int requestId, int tourObjectId, string comment);
        Result<PublicTourObjectDto> Update(PublicTourObjectDto publicTourObject);
        Result Delete(int id);
        Result<PagedResult<PublicTourObjectDto>> GetTourObjectByTourId(int tourId);
        Result<PublicTourObjectDto> Get(int id);
    }
}
