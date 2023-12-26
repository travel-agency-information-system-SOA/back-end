using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class PasswordReset : Entity
    {
        public long UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryTime { get; set; }

        public PasswordReset(long userId, string token, DateTime expiryTime)
        {
            UserId = userId;
            Token = token;
            ExpiryTime = expiryTime;
            Validate();
        }

        private void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
            if (string.IsNullOrEmpty(Token)) throw new ArgumentException("Invalid Token");
        }
    }
}
