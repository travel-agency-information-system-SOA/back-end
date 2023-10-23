using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;


namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly DbSet<Problem> _problems;  

        public ProblemRepository(ProblemsContext contex)
        {
            _problems = contex.Problems;
        }

        public void Report(Problem problem)
        {
            _problems.Add(problem);
        }

        public List<Problem> GetByUserId(int userId)
        {
            return _problems.Where(problem => problem.Id == userId).ToList();
        }
    }
}