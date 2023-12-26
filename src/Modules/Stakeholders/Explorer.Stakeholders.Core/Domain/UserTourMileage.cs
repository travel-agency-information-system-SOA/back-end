using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class UserTourMileage : Entity
{
    public int UserId { get; init; }
    public double TourMileage { get; init; }
    public DateTime Time { get; init; }

    public UserTourMileage(int userId, double tourMileage, DateTime time)
    {
        UserId = userId;
        TourMileage = tourMileage;
        Time = time;
    }

    public UserTourMileage()
    {

    }
}
