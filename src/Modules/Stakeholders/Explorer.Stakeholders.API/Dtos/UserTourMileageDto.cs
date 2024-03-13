using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos;

public class UserTourMileageDto
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public double TourMileage { get; set; }
    public DateTime Time { get; set; }
}
