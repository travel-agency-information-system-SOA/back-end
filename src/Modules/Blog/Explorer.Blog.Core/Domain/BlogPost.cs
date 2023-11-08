using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Explorer.Blog.Core.Domain
{

    public enum BlogPostStatus { DRAFT, PUBLISHED, CLOSED };
    public class BlogPost : Entity
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public DateTime CreationDate { get; init; }
        public List<int>? ImageIDs { get; init; }
        public BlogPostStatus Status { get; init; }

        public BlogPost(string title, string description, DateTime creationDate, List<int>? imageIDs, BlogPostStatus status)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Invalid Title.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
            if (creationDate == default) throw new ArgumentException("Invalid Creation Date.");
            if (status != BlogPostStatus.DRAFT && status != BlogPostStatus.PUBLISHED && status != BlogPostStatus.CLOSED)
                throw new ArgumentException("Invalid Post Status");

            Title = title;
            Description = description;
            CreationDate = creationDate;
            ImageIDs = imageIDs;
            Status = status;
        }
    }
}
