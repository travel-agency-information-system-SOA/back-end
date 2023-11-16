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
    public class BlogPostComment : ValueObject
    {
        public string Text { get; }
        public int UserId { get; }
        public DateTime CreationTime { get; }
        public DateTime LastUpdatedTime { get; }

        [JsonConstructor]
        public BlogPostComment(string text, int userId, DateTime creationTime, DateTime lastUpdatedTime)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Invalid Text.");
            if (creationTime == null) throw new ArgumentException("Invalid Creation time.");
            if (lastUpdatedTime == null) throw new ArgumentException("Invalid Creation time.");
            if (userId == 0) throw new ArgumentException("Field required");

            Text = text;
            UserId = userId;
            LastUpdatedTime = lastUpdatedTime;
            CreationTime = creationTime;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
            yield return UserId;
            yield return LastUpdatedTime;
            yield return CreationTime;
        }
    }
}
