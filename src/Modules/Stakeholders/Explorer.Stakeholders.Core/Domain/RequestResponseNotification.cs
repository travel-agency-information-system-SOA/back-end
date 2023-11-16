using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class RequestResponseNotification : Entity
    {
        public int AuthorId { get; init; }
        public string Comment { get; init; }
        public DateTime Creation { get; init; }

        public RequestResponseNotification() { }
        public RequestResponseNotification(int authorId, string comment, DateTime creation)
        {
            AuthorId = authorId;
            Comment = comment;
            Creation = creation;
        }
    }
}
