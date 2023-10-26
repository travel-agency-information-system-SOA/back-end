namespace Explorer.Tours.API.Dtos
{
    public class PreferencesDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PreferredDifficulty { get; set; }
        public List<int>? TransportationPreferences { get; set; }
        public List<string>? InterestTags { get; set; }
    }
}
