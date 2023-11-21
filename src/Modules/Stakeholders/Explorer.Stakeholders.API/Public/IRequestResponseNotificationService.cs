using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IRequestResponseNotificationService
    {
        Result<PagedResult<RequestResponseNotificationDto>> GetByAuthorId(int authorId, int page, int pageSize);
        Result<RequestResponseNotificationDto> Create(RequestResponseNotificationDto notification);
        Result Delete(int id);
    }
}
