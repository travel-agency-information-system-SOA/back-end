using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Follower : Entity
    {
        public long UserId { get; init; }
        public long FollowerId { get; init; }
        public string FollowerUsername { get; init; }


        public Follower(long userId, long followerId, string followerUsername)
        {
            UserId = userId;
            FollowerId = followerId;
            FollowerUsername = followerUsername;
            Validate();
        }

        private void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
            if (string.IsNullOrWhiteSpace(FollowerUsername)) throw new ArgumentException("Invalid Username");
        }

    }
}
