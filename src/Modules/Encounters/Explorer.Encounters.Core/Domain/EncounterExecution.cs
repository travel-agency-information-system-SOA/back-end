using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounters.Core.Domain;

public class EncounterExecution : Entity
{
    public long UserId { get; init; }
    public long EncounterId { get; init; }
    public DateTime? CompletionTime { get; init;}
    public bool IsCompleted { get; init; }
    public EncounterExecution() { }

    public EncounterExecution(long userId, long encounterId, DateTime? completionTime, bool isCompleted)
    {
        UserId = userId;
        EncounterId = encounterId;
        CompletionTime = completionTime;
        IsCompleted = isCompleted;
    }
}
