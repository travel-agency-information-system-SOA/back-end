using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class Preferences : Entity
    {
        public long UserId { get; init; }
        public DifficultyLevel PreferredDifficulty { get; private set; }
        public List<int>? TransportationPreferences { get; init; } // Setnja, Bicikla, Auto, Camac
        public List<string>? InterestTags { get; init; }

        public Preferences(long userId, DifficultyLevel preferredDifficulty, List<int> transportationPreferences, List<string> interestTags)
        {
            UserId = userId;
            PreferredDifficulty = preferredDifficulty;
            TransportationPreferences = transportationPreferences;
            InterestTags = interestTags;

            Validate();
        }

        private void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
        }
    }

    public enum DifficultyLevel
    {
        Easy,
        Moderate,
        Difficult
    }
}
