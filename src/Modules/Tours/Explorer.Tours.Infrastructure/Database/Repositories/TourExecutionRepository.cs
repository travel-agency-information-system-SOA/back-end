using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    internal class TourExecutionRepository : ITourExecutionRepository
    {
        private readonly DbSet<TourExecution> _tourExecutions;
        private readonly ToursContext _context;

        public TourExecutionRepository(ToursContext context)
        {
            _context = context;
            _tourExecutions = _context.Set<TourExecution>();
        }

        public PagedResult<TourExecution> GetById(int id, int page, int pageSize)
        {
            var tourExecution = _tourExecutions
                .Include(te => te.TourPoints)
                .Include(te => te.Position)
                .Where(te => te.Id == id)
                .GetPaged(page, pageSize);

            return tourExecution.Result;
        }

        public TourExecution GetById(int id)
        {
            return _tourExecutions
                .Include(te => te.TourPoints)
                .Include(te => te.Position)
                .SingleOrDefault(te => te.Id == id);
        }

        public Result Update(TourExecution updatedTourExecution)
        {
            try
            {
                var existingTourExecution = _tourExecutions
                    .Include(te => te.TourPoints)
                    .Include(te => te.Position)
                    .SingleOrDefault(te => te.Id == updatedTourExecution.Id);

                if (existingTourExecution == null)
                {
                    return Result.Fail("TourExecution not found");
                }

                foreach (var tourPoint in existingTourExecution.TourPoints)
                {
                    _context.Entry(tourPoint).State = EntityState.Detached;
                }

                existingTourExecution.UpdateFrom(updatedTourExecution);

                _context.SaveChanges();
                //_context.Update(existingTourExecution);
                
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail($"Failed to update TourExecution. Error: {ex.Message}");
            }
        }
    }
}
