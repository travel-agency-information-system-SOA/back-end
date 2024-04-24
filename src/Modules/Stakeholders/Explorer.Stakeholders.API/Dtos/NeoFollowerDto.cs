using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class NeoFollowerDto
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string FollowingUserId { get; set; }
        public string FollowingUsername { get; set; }

    }
}
