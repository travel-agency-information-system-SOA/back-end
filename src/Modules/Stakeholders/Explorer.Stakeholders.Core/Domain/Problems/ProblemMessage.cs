using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.Problems;

public class ProblemMessage : Entity
{
    public string Content { get; init; }
    public bool IsRead { get; init; }
    public long ProblemId { get; init; }
    public Problem? Problem { get; init; }
    public int IdSender { get; init; }

    public ProblemMessage(string content, bool isRead, long problemId, int idSender)
    {
        Content = content;
        IsRead = isRead;
        ProblemId = problemId;
        IdSender = idSender;
    }
}
