using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public enum Status
    {
        Accepted,
        Onhold,
        Rejected
    }
    public class TourPointRequestDto
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int TourPointId { get; set; }
        public Status Status { get; set; }
    }
}
