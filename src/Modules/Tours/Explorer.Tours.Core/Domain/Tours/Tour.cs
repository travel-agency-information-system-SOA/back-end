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
        public string Name { get; private set; }

        public DifficultyLevel DifficultyLevel { get; private set; }

        public string Description { get; private set; }

        public List<string> Tags { get; private set; }


        public TourStatus Status { get;  set; }

        public int Price { get; private set; }

        public int GuideId { get; private set; }

        public DateTime? PublishedDateTime { get; private set; }


        public ICollection<TourPoint> TourPoints { get; } = new List<TourPoint>();

        public ICollection<TourCharacteristic> TourCharacteristics { get; } = new List<TourCharacteristic>();
        public ICollection<TourReview> TourReviews { get; }= new List<TourReview>();

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
            PublishedDateTime = null; 
            Price = 0;
            GuideId = guideId;


        }

        public void UpdateTour(string name, DifficultyLevel difficultyLevel, string? description, int guideId, TourStatus status)
        {
            Name=name;
			DifficultyLevel = difficultyLevel;
            Description = description;
            GuideId = guideId;
            Status = status;


		}

        public void setCharacteristic(double distance, double duration, TransportType transportType)
        {
            TourCharacteristic characteristic = TourCharacteristics.FirstOrDefault(c => c.TransportType == transportType);
            if (characteristic != null) 
            {
                TourCharacteristics.Remove(characteristic);
            }
            TourCharacteristics.Add(new TourCharacteristic(distance, duration, transportType));
             
        }

        public void Publish(Tour tour)
        {
            if (Status != TourStatus.Draft)
            {
                throw new InvalidOperationException("Only draft tours can be published.");
            }

            tour.Status= TourStatus.Published;
            tour.PublishedDateTime = DateTime.UtcNow;



        }



    }


}
