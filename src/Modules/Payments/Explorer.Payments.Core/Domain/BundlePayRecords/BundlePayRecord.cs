using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.TourBundle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.BundlePayRecords
{
    public class BundlePayRecord : Entity
    {
        public int TouristId { get; set; }
        public int TourBundleId { get; init; }

        public double Price { get; init; }
        public DateTime DateCreated { get; init; }

        public BundlePayRecord(int touristId, int tourBundleId, double price, DateTime dateCreated) 
        {
            TouristId = touristId;
            TourBundleId = tourBundleId;
            Price = price;
            DateCreated = dateCreated;
        }

        public BundlePayRecord() { }
    }
}
