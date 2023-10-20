using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Club : Entity
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string Image { get; init; }
        public Club(string name, string description, string image)
        {
            Name = name;
            Description = description;
            Image = image;
            Validate();
        }
        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
            if (string.IsNullOrWhiteSpace(Image)) throw new ArgumentException("Invalid Image");
        }
    }
}
