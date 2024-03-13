using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.InteropServices;

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

        public TourExecution GetByUser(int userId)
        {
            return _tourExecutions
                .Include(te => te.TourPoints)
                .Include(te => te.Position)
                .SingleOrDefault(te => te.UserId == userId && te.Status == TourExecutionStatus.InProgress);
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

        public int Create(int userId, int tourId)
        {
            var maxId = _context.TourExecutions.Max(entity => (int?)entity.Id) ?? 0;
            var id = maxId + 1;
            var execution = new TourExecution(userId, tourId, TourExecutionStatus.InProgress);

            execution.SetId(id);
            _context.Add(execution);



            _context.SaveChanges();

            return id;
        }

        public void CreatePoint(int executionId, int tourPointId)
        {
            var point = new TourPointExecution(executionId, tourPointId, DateTime.UtcNow);

            var maxId = _context.TourPointExecutions.Max(entity => (int?)entity.Id) ?? 0;

            var id = maxId + 1;
            point.SetId(id);
            _context.Add(point);

            _context.SaveChanges();
        }

        public void CreatePosition(double longitude, double latitude, int executionId)
        {
            
            var position = new TourExecutionPosition(executionId, DateTime.UtcNow, latitude, longitude);
            var maxId = _context.TourExecutionPositions.Max(entity => (int?)entity.Id) ?? 0;

            var id = maxId + 1;
            position.SetId(id);

            _context.Add(position);
            _context.SaveChanges();

        }


        //za recenzije
        public PagedResult<TourExecution> GetAll(int page, int pageSize)
        {
            var executions = _tourExecutions.Include(t => t.Position).Include(t => t.TourPoints).GetPaged(page, pageSize);
            return executions.Result;
        }
    }
}
