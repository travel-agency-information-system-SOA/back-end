using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class TouristXP : Entity
    {
        public int Experience { get; set; }
        public int Level { get; set; }
        public int TouristId { get; init; }

        public TouristXP() { }
        public TouristXP(int experience, int level, int id) 
        {
            Experience = experience;
            Level = level;
            TouristId = id;
            Validate();
        }

        private void Validate()
        {
            if (Experience < 0) throw new ArgumentException("Field can't be a negative number");
            if (Level < 0) throw new ArgumentException("Field can't be a negative number");
            if (TouristId == 0) throw new ArgumentException("Field required");
        }

        public void AddExperience(int ammount) 
        {
            Experience += ammount;
        }
        
        public void LevelUp(int level)
        {
            Level = level;
        }
    }
}
