using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class TourKeyPointEncounterDto
    {
        public int Id { get; set; }
        public int EncounterId { get; set; }
        public int KeyPointId { get; set; } 
        public bool IsMandatory { get; set; }
    }
}
