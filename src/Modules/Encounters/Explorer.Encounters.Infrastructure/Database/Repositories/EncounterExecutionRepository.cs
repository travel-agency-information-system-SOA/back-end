using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class EncounterExecutionRepository : IEncounterExecutionRepository
    {
        private readonly DbSet<EncounterExecution> _encounterExecutions;
        private readonly EncountersContext _context;

        public EncounterExecutionRepository(EncountersContext context)
        {
            _context = context;
            _encounterExecutions = _context.Set<EncounterExecution>();
        }

        public Result Update(EncounterExecution encounterExecution)
        {
            var existingExecution = _encounterExecutions.SingleOrDefault(e => e.Id == encounterExecution.Id);

            _context.Entry(encounterExecution).State = EntityState.Detached;

            existingExecution.IsCompleted = encounterExecution.IsCompleted;
            existingExecution.CompletionTime = encounterExecution.CompletionTime;
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}
