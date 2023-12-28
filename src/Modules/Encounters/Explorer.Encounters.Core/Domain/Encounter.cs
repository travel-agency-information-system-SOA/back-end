using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain;

public enum EncounterStatus { ACTIVE, DRAFT, ARCHIVED };
public enum EncounterType { SOCIAL, LOCATION, MISC };

public class Encounter : Entity
{
    public string Name { get; init; }
    public string Description { get; init; }
    public int XpPoints { get; init; }
    public EncounterStatus Status { get; init; }
    public EncounterType Type { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public bool ShouldBeApproved { get; init; }

    public Encounter(string name, string description, int xpPoints, EncounterStatus status, EncounterType type, double latitude, double longitude, bool shouldBeApproved)
    {
        Name = name;
        Description = description;
        XpPoints = xpPoints;
        Status = status;
        Type = type;
        Latitude = latitude;
        Longitude = longitude;
        ShouldBeApproved = shouldBeApproved;
    }


}
