using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class PersonDatabaseRepository : IPersonRepository
    {
        private readonly StakeholdersContext _dbContext;

        public PersonDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Exists(string mail)
        {
            return _dbContext.People.Any(person => person.Email == mail);
        }

        public List<Person> GetAll()
        {
            return _dbContext.People.ToList();
        }

        public Person? Get(long personId)
        {
            return _dbContext.People.FirstOrDefault(person => person.Id == personId);
        }

        public Person? GetByEmail(string mail)
        {
            return _dbContext.People.FirstOrDefault(person => person.Email == mail);
        }

        public Person Create(Person person)
        {
            _dbContext.People.Add(person);
            _dbContext.SaveChanges();
            return person;
        }

        public Person Update(Person person)
        {
            _dbContext.People.Update(person);
            _dbContext.SaveChanges();
            return person;
        }
    }
}
