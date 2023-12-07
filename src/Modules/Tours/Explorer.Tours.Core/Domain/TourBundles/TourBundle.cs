using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourBundle
{
    public class TourBundle: Entity
    {
        public String Name { get; init; }

        public double Price { get; init; }

        public List<int> TourIds {  get; init; }

        public TourBundleStatus Status { get; init; }
        public TourBundle() { }
    }
}
