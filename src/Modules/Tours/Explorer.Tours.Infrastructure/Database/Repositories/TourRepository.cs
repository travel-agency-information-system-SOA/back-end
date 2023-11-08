using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
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
		

		public PagedResult<Tour> GetByUserId(int userId, int page, int pageSize)
		{
			var tours= _tours.Include(t => t.TourPoints).Where(t=>t.GuideId == userId).GetPagedById(page, pageSize);
			return tours.Result;
		}
	}
}
