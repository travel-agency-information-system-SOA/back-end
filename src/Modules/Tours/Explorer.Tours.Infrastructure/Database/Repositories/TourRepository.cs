using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
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

		public TourRepository(ToursContext context)
		{
			_tours = context.Tours; 
		}
		public void Create(Tour tour)
		{
			_tours.Add(tour);
		}

		public List<Tour> GetByUserId(int userId)
		{
			return _tours.Where(tour => tour.GuideId == userId).ToList();
		}
	}
}
