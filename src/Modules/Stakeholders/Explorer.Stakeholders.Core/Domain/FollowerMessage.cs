using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class FollowerMessage : Entity
    {
        public long UserId { get; init; }
        public long FollowerId { get; init; }
        public string FollowerUsername { get; init; }
        public string Message { get; init; }
        public bool IsRead { get; set; }

        public FollowerMessage(long userId, long followerId, string followerUsername, string message, bool isRead)
        {
            UserId = userId;
            FollowerId = followerId;
            FollowerUsername = followerUsername;
            Message = message;
            IsRead = isRead;
            Validate();
        }

        private void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
            if (FollowerId == 0) throw new ArgumentException("Invalid UserId");
            if (string.IsNullOrWhiteSpace(FollowerUsername)) throw new ArgumentException("Invalid Username");
        }

        public void MarkAsRead()
        {
            IsRead = true;
        }


    }
}
