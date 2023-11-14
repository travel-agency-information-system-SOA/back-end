using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class UserPosition : Entity
    {
        public long UserId { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        


        public UserPosition(long userId, double latitude, double longitude)
        {
            UserId = userId;
            Latitude = latitude;
            Longitude = longitude;
            Validate();
        }

        public UserPosition() { }

        private void Validate()
        {
            if (UserId < 0)
                throw new ArgumentException("UserId must be a positive integer.");
        }

        public void UpdateFrom(UserPosition updatedPosition)
        {
            this.UserId = updatedPosition.UserId;
            this.Latitude = updatedPosition.Latitude;
            this.Longitude = updatedPosition.Longitude;

        }

    }
}
