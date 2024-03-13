using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos;

public class HiddenLocationEncounterDto
{
    public long Id { get; set; } 
    public long EncounterId { get; set; }
    public string ImageURL { get; set; }
    public double ImageLatitude { get; set; }
    public double ImageLongitude { get; set; }
    public double DistanceTreshold { get; set; }
}
