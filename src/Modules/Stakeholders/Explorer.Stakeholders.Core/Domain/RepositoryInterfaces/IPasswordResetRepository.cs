namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IPasswordResetRepository
    {
        PasswordReset Create(PasswordReset passwordReset);
        PasswordReset? GetByUserId(long userId);
        PasswordReset? GetByToken(string token);
        PasswordReset? ValidateToken(string token);
        void Delete(PasswordReset passwordReset);
    }
}
