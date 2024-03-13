using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class FollowerMessageDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long FollowerId { get; set; }
        public string FollowerUsername { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}
