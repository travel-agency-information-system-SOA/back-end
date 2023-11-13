using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class PublicTourPoint : Entity
    {
        public long IdTour { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public string ImageUrl { get; init; }

        public PublicTourPoint() { }

        public PublicTourPoint(long idTour, string name, string? description, double latitude, double longitude, string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            IdTour = idTour;
            Name = name;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            ImageUrl = imageUrl;
        }
    }
}

