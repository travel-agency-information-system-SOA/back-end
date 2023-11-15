using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class RequestResponseNotificationDto
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Comment { get; set; }
        public DateTime Creation { get; set; }
    }
}
