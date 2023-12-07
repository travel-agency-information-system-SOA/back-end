using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class TourSale : Entity
    {
        public List<int> TourIds { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public int SalePercentage { get; init; }
        public int AuthorId { get; init; }
        public TourSale(List<int> tourIds, DateTime startDate, DateTime endDate, int salePercentage, int authorId) 
        {
            if(salePercentage < 0 || salePercentage > 100 || DateTime.Today > startDate || endDate < startDate || (endDate - startDate).Days >= 14) throw new ArgumentOutOfRangeException("Invalid Value.");
            TourIds = tourIds;
            StartDate = startDate;
            EndDate = endDate;
            SalePercentage = salePercentage;
            AuthorId = authorId;
        }
    }
}
