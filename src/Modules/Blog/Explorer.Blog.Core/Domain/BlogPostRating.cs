using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Explorer.Blog.Core.Domain
{
    public class BlogPostRating : ValueObject
    {
        public int UserId { get;}
        public DateTime CreationTime { get;}
        public bool IsPositive { get;}

        [JsonConstructor]
        public BlogPostRating(int userId, DateTime creationTime, bool isPositive)
        {
            if (isPositive == null)
                throw new ArgumentException("isPositive cannot be null.");
            if (creationTime == null)
                throw new ArgumentException("Creation time cannot be null.");

            UserId = userId;
            CreationTime = creationTime;
            IsPositive = isPositive;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CreationTime;
            yield return IsPositive;
            yield return UserId;
        }
    }
}
