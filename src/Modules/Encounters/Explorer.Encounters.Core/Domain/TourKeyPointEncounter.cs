using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class TourKeyPointEncounter : Entity
    {
        public int EncounterId { get; init; }
        public int KeyPointId { get; init; }
        public bool IsMandatory { get; init; }

        public TourKeyPointEncounter() { }
        public TourKeyPointEncounter(int encounterId, int keyPointId, bool isMandatory)
        {
            EncounterId = encounterId;
            KeyPointId = keyPointId;
            IsMandatory = isMandatory;
        }
    }
}
