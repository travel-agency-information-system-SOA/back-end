using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourObject : Entity
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
        public ObjectCategory Category { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }

        public TourObject(string name, string description, string imageUrl, ObjectCategory category, double latitude, double longitude)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Category = category;
            Latitude = latitude;
            Longitude = longitude;
        }


    }
}
