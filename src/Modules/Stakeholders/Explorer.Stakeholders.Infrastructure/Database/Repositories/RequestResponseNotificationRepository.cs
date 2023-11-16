using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class RequestResponseNotificationRepository : IRequestResponseNotificationRepository
    {
        private readonly DbSet<RequestResponseNotification> _requestResponseNotification;

        public RequestResponseNotificationRepository(StakeholdersContext context)
        {
            _requestResponseNotification = context.RequestResponseNotifications;
        }
        public List<RequestResponseNotification> GetByAuthorId(int authorId)
        {
            return _requestResponseNotification.Where(notification => notification.AuthorId == authorId).ToList();
        }
    }
}
