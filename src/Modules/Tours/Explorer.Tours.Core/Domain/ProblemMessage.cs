using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class ProblemMessage : Entity
    {
        public string Content { get; init; }
        public bool IsRead { get; init; }
        public int IdProblem { get; init; }

        public int IdSender { get; init; }

        public ProblemMessage(string content, bool isRead, int idProblem, int idSender)
        {
            Content = content;
            IsRead = isRead;
            IdProblem = idProblem;
            IdSender = idSender;
        }
    }
}
