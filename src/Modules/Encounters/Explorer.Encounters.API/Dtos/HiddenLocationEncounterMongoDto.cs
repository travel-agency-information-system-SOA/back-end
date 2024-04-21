using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class HiddenLocationEncounterMongoDto
    {
        public string Id { get; set; }
        public EncounterMongoDto Encounter { get; set; }
        public string ImageURL { get; set; }
        public double ImageLatitude { get; set; }
        public double ImageLongitude { get; set; }
        public double DistanceTreshold { get; set; }
    }
}
