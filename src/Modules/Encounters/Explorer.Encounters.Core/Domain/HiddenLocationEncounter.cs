using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HiddenLocationEncounter : Entity
{
    public string ImageURL { get; init; }
    public double ImageLatitude { get; init; }
    public double ImageLongitude { get; init; }
    public double DistanceTreshold { get; init; }
    public long EncounterId { get; init; }


    public HiddenLocationEncounter() { }

    public HiddenLocationEncounter(string imageURL, double imageLatitude, double imageLongitude, double distanceTreshold, long encounterId)
    {
        ImageURL = imageURL;
        ImageLatitude = imageLatitude;
        ImageLongitude = imageLongitude;
        DistanceTreshold = distanceTreshold;
        EncounterId = encounterId;
    }
}
