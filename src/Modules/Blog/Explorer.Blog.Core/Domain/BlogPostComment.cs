using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Explorer.Blog.Core.Domain
{
    public class BlogPostComment : Entity
    {
        public string Text { get; init; }
        public int UserId { get; init; }
        public int BlogId { get; init; }
        public DateTime CreationTime { get; init; }
        public DateTime LastUpdatedTime { get; init; }


        public BlogPostComment(string text, int userId, int blogId, DateTime creationTime, DateTime lastUpdatedTime)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Invalid Text.");
            if (creationTime == null) throw new ArgumentException("Invalid Creation time.");
            if (lastUpdatedTime == null) throw new ArgumentException("Invalid Creation time.");
            if (userId == 0) throw new ArgumentException("Field required");
            if (blogId == 0) throw new ArgumentException("Field required");

            Text = text;
            UserId = userId;
            BlogId = blogId;
            LastUpdatedTime = lastUpdatedTime;
            CreationTime = creationTime;
        }
        
    }
}
