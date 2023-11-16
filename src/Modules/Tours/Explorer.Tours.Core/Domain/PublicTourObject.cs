using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class PublicTourObject : Entity
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
        public ObjectCategory Category { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }

        public PublicTourObject(string name, string description, string imageUrl, double latitude, double longitude)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Latitude = latitude;
            Longitude = longitude;
            Category = ObjectCategory.Other;
        }
    }
}
