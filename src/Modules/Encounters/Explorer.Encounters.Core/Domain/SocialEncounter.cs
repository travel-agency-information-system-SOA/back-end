using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{


    public class SocialEncounter : Entity
    {
        public long EncounterId { get; init; }
        public int TouristsRequiredForCompletion { get; init; }
        public double DistanceTreshold { get; init; }
        public List<long> TouristIDs { get; init; }
    
        public SocialEncounter(long encounterId, int touristsRequiredForCompletion, double distanceTreshold, List<long>? touristIDs)
        {
            EncounterId = encounterId;
            TouristsRequiredForCompletion = touristsRequiredForCompletion;
            DistanceTreshold = distanceTreshold;
            TouristIDs = touristIDs ?? new List<long>();
        }
    }
}
