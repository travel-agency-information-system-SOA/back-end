using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class UserPositionDto
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
    }
}

