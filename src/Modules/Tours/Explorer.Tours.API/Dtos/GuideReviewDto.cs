namespace Explorer.Tours.API.Dtos
{
    public class GuideReviewDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GuideId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
}
