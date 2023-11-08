using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class Tour : Entity
    {
        public string Name { get; init; }

        public DifficultyLevel DifficultyLevel { get; init; }

        public string Description { get; init; }

        public List<string> Tags { get; init; }

        public TourStatus Status { get; init; }

        public int Price { get; init; }

        public int GuideId { get; init; }

        public ICollection<TourPoint> TourPoints { get; } = new List<TourPoint>();

        public ICollection<TourCharacteristic> TourCharacteristics { get; } = new List<TourCharacteristic>();

        public Tour(string name, DifficultyLevel difficultyLevel, string? description, int guideId)
        {
            if (string.IsNullOrWhiteSpace(name) || guideId == 0) throw new ArgumentException("Field empty.");

            if (!Enum.IsDefined(typeof(DifficultyLevel), difficultyLevel))
            {
                throw new ArgumentException("Invalid DifficultyLevel value.");
            }
            Name = name;
            DifficultyLevel = difficultyLevel;
            Description = description;
            Status = TourStatus.Draft;
            Price = 0;
            GuideId = guideId;

        }

        public void setCharacteristic(int distance, TimeSpan duration, TransportType transportType)
        {
            TourCharacteristic characteristic = TourCharacteristics.FirstOrDefault(c => c.TransportType == transportType);
            if (characteristic != null) 
            {
                TourCharacteristics.Remove(characteristic);
            }
            TourCharacteristics.Add(new TourCharacteristic(distance, duration, transportType));
             
        }
    }
}
