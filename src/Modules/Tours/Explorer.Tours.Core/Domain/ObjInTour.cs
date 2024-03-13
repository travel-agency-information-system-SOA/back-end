using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class ObjInTour : Entity
    {
        public int IdObject { get; init; }
        public int IdTour { get; init; }

        public ObjInTour(int idObject, int idTour)
        {
            IdObject = idObject;
            IdTour = idTour;
        }
    }
}
