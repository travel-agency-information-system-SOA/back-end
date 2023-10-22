using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class GuideReview : Entity
    {
        public int UserId { get; init; }
        public int GuideId {  get; init; }
        public int Rating { get; init; }
        public string? Comment { get; init; }
        public DateTime SubmissionDate { get; init; }

        public GuideReview(int userId, int guideId, int rating, string comment, DateTime submissionDate)
        {
            if(rating < 1 || rating > 5) throw new ArgumentOutOfRangeException("Invalid Value.");
            UserId = userId;
            GuideId = guideId;
            Rating = rating;
            Comment = comment;
            SubmissionDate = submissionDate;
        }
    }
}
