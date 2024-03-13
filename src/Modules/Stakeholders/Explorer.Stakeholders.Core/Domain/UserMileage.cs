using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class UserMileage : Entity
    {
        public int UserId { get; init; }
        public double Mileage { get; set; }
        public double MileageByMonth { get; set; }

        public UserMileage(int userId, double mileage, double mileageByMonth)
        {
            UserId = userId;
            Mileage = mileage;
            MileageByMonth = mileageByMonth;
        }

        public UserMileage()
        {

        }


    }
}
