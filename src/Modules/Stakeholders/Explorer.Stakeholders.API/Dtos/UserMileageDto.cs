using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class UserMileageDto
    {
        public int Id { get; set; }
        public double Mileage { get; set; }
        public double MileageByMonth { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }

    }
}
