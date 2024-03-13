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
    public class RequestResponseNotificationService : CrudService<RequestResponseNotificationDto, RequestResponseNotification>, IRequestResponseNotificationService
    {
        public RequestResponseNotificationService(ICrudRepository<RequestResponseNotification> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<PagedResult<RequestResponseNotificationDto>> GetByAuthorId(int authorId, int page, int pageSize)
        {
            var allNotifications = CrudRepository.GetPaged(page, pageSize);
            var filteredNotifications = allNotifications.Results.Where(notifications => notifications.AuthorId == authorId);
            var filteredPagedResult = new PagedResult<RequestResponseNotification>(filteredNotifications.ToList(), filteredNotifications.Count());
            return MapToDto(filteredPagedResult);
        }
    }
}
