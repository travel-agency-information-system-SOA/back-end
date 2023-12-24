namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IUserRepository
{
    bool Exists(string username);
    User? GetActiveByName(string username);
    List<User> GetAll();
    User? Get(long userId);
    User Create(User user);
    User Update(User user);
    long GetPersonId(long userId);
    string GetPersonEmail(long userId);
    User? GetUserByToken(string verificationToken);
}