using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    internal class TourRepository : ITourRepository
	{
		private readonly DbSet<Tour> _tours;
		private readonly ToursContext _context;

		public TourRepository(ToursContext context)
		{
			 _context = context;
			 _tours = _context.Set<Tour>();
		}

		
		public PagedResult<Tour> GetAll(int page, int pageSize)
		{
			var tours = _tours.Include(t => t.TourReviews).Include(t=>t.TourPoints).GetPaged(page, pageSize);
			return tours.Result;
		}


		public PagedResult<Tour> GetByUserId(int userId, int page, int pageSize)
		{
			var tours= _tours.Include(t => t.TourPoints).Where(t=>t.UserId == userId).GetPagedById(page, pageSize);
			return tours.Result;
		}

        public Tour GetById(int tourId)
		{
            Tour tour = _tours.Include(t => t.TourPoints).FirstOrDefault(t => t.Id == tourId);
            if (tour == null) throw new KeyNotFoundException("Not found");
			return tour;
		}

        public PagedResult<Tour> GetAllPublished(int page, int pageSize)
        {
			var tours = _tours.Include(t => t.TourPoints).Where(t => t.Status == TourStatus.Published).GetPagedById(page, pageSize);
            return tours.Result;
        }
    

        public Tour GetByTourId(int tourId)
        {
            var tour = _tours.Include(t => t.TourPoints).SingleOrDefault(t => t.Id == tourId);
            return tour;
        }

    
        

        public Result DeleteAgreggate(int tourId)
		{
			var tourToDelete = _tours.Where(t => t.Id == tourId).Include(t => t.TourPoints).Include(t => t.TourReviews.Where(tr => tr.TourId == tourId)).FirstOrDefault();
			if(tourToDelete != null)
			{
				_context.RemoveRange(tourToDelete.TourPoints);
				_context.RemoveRange(tourToDelete.TourReviews);
				_context.Remove(tourToDelete);

				_context.SaveChanges();
				return Result.Ok();
			}

			return Result.Fail("Tour not found");
		}

		
	}

}
