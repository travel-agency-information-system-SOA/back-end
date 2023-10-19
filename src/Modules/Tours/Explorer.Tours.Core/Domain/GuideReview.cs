using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class GuideReview : Entity
    {
        public int GuideId {  get; init; }
        public int Rating { get; init; }
        public string? Comment { get; init; }
        public DateTime SubmissionDate { get; init; }

        public GuideReview(int guideId, int rating, string comment, DateTime submissionDate)
        {
            if(rating < 1 || rating > 5) throw new ArgumentOutOfRangeException("Invalid Value.");
            GuideId = guideId;
            Rating = rating;
            Comment = comment;
            SubmissionDate = submissionDate;
        }
    }
}
