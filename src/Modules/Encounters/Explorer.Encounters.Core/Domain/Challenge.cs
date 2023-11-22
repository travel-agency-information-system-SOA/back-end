using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain;

public enum ChallengeStatus { ACTIVE, DRAFT, ARCHIEVED };
public enum ChallengeType { SOCIAL, LOCATION, MISC };

public class Challenge : Entity
{
    public string Title { get; init; }
    public string Description { get; init; }
    public int XpPoints { get; init; }
    public ChallengeStatus Status { get; init; }
    public ChallengeType Type { get; init; }

    public Challenge(string title, string description, int xpPoints, ChallengeStatus status, ChallengeType type)
    {
        Title = title;
        Description = description;
        XpPoints = xpPoints;
        Status = status;
        Type = type;
    }

    public void AddChallenge()
    {

    }
    public void DeleteChallenge()
    {

    }
    public void UpdateChallenge()
    {

    }

}
