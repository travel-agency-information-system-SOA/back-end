using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;



namespace Explorer.Blog.Core.Domain
{

    public enum BlogPostStatus { DRAFT, PUBLISHED, CLOSED, ACTIVE, FAMOUS };
    public class BlogPost : Entity
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public DateTime CreationDate { get; init; }
        public List<string>? ImageURLs { get; init; }
        public List<BlogPostComment>? Comments { get; init; }
        public List<BlogPostRating>? Ratings { get; init; }
        public BlogPostStatus Status { get; init; }

        public BlogPost(string title, string description, DateTime creationDate, List<string>? imageURLs, List<BlogPostComment>? comments, BlogPostStatus status, List<BlogPostRating>? ratings)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Invalid Title.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
            if (creationDate == default) throw new ArgumentException("Invalid Creation Date.");
            if (status != BlogPostStatus.DRAFT && status != BlogPostStatus.PUBLISHED && status != BlogPostStatus.CLOSED)
                throw new ArgumentException("Invalid Post Status");

            Title = title;
            Description = description;
            CreationDate = creationDate;
            ImageURLs = imageURLs ?? new List<string>();
            Comments = comments ?? new List<BlogPostComment>();
            Ratings = ratings ?? new List<BlogPostRating>();
            Status = status;
        }

        public void AddComment(BlogPostCommentDto commentDto)
        {
            // Map the DTO to the domain entity
            var Comment = new BlogPostComment(commentDto.Text, commentDto.UserId, DateTime.Now, DateTime.Now);

            // Add the comment to the collection
            Comments.Add(Comment);
        }

        public void RemoveComment(int userId, DateTime creationTime)
        {
            // Assuming a hypothetical method that identifies and removes the comment based on certain criteria
            var commentToRemove = Comments.FirstOrDefault(c =>
                c.UserId == userId &&
                c.CreationTime.Equals(creationTime));

            if (commentToRemove != null)
            {
                Comments.Remove(commentToRemove);
            }
        }

        public void EditComment(BlogPostCommentDto editedComment)
        {
            // Assuming a hypothetical method that identifies and edits the comment based on certain criteria
            var commentToEdit = Comments.FirstOrDefault(c =>
                c.UserId == editedComment.UserId &&
                c.CreationTime.Equals(editedComment.CreationTime));

            if (commentToEdit != null)
            {
                Comments.Remove(commentToEdit);

                // Add the edited comment to the list
                Comments.Add(new BlogPostComment(editedComment.Text, editedComment.UserId, editedComment.CreationTime, DateTime.Now));
            }
        }
        public void AddRating(BlogPostRatingDto ratingDto)
        {
            var rating = Ratings.FirstOrDefault(c =>
                c.UserId == ratingDto.UserId);

            if (rating != null)
            {
                Ratings.Remove(rating);

                Ratings.Add(new BlogPostRating(ratingDto.UserId, ratingDto.CreationTime,  ratingDto.IsPositive));
            }
            else
            {
                Ratings.Add(new BlogPostRating(ratingDto.UserId, ratingDto.CreationTime, ratingDto.IsPositive));
            }

        }
        public void RemoveRating(int userId)
        {
            // Assuming a hypothetical method that identifies and removes the comment based on certain criteria
            var ratingToRemove = Ratings.FirstOrDefault(c =>
                c.UserId == userId);

            if (ratingToRemove != null)
            {
                Ratings.Remove(ratingToRemove);
            }
        }


    }
}
