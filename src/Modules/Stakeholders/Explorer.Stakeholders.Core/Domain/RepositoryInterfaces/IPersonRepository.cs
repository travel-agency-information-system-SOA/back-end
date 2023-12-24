namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IPersonRepository
    {
        bool Exists(string mail);
        List<Person> GetAll();
        Person? Get(long personId);
        Person Create(Person person);
        Person Update(Person person);
    }
}
