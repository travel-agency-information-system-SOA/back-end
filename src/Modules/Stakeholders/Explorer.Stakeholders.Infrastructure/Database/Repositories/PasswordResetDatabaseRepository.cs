using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class PasswordResetDatabaseRepository : IPasswordResetRepository
    {
        private readonly StakeholdersContext _dbContext;

        public PasswordResetDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PasswordReset Create(PasswordReset passwordReset)
        {
            _dbContext.PasswordResets.Add(passwordReset);
            _dbContext.SaveChanges();
            return passwordReset;
        }

        public PasswordReset? GetByUserId(long userId)
        {
            var password = _dbContext.PasswordResets.FirstOrDefault(p => p.UserId == userId);
            return password;
        }

        public PasswordReset? GetByToken(string token)
        {
            var password = _dbContext.PasswordResets.FirstOrDefault(p => p.Token == token);
            if (password == null) throw new Exception("Not found");
            return password;
        }

        public PasswordReset? ValidateToken(string token)
        {
            var password = _dbContext.PasswordResets.FirstOrDefault(p => p.Token == token && p.ExpiryTime > DateTime.UtcNow);
            return password;
        }

        public void Delete(PasswordReset passwordReset)
        {
            _dbContext.PasswordResets.Remove(passwordReset);
            _dbContext.SaveChanges();
        }
    }
}
