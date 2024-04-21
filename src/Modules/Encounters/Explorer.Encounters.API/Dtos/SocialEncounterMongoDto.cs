using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Explorer.Encounters.API.Dtos
{
    public class SocialEncounterMongoDto
    {
        public string Id { get; set; }
        public EncounterMongoDto Encounter{ get; set; }
        public int TouristsRequiredForCompletion { get; set; }
        public double DistanceTreshold { get; set; }
        public List<long> TouristIDs { get; set; }
    }
}
