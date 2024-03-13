using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.Competitions;
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
	public class CompetitionRepository : ICompetitionRepository
	{
		private readonly DbSet<Competition> _competitions;
		private readonly ToursContext _context;

		public CompetitionRepository(ToursContext context)
		{
			_context = context;
			_competitions = _context.Set<Competition>();
		}

		public PagedResult<Competition> GetAll(int page, int pageSize)
		{
			var competitions = _competitions.Include(c => c.CompetitionApplies).GetPaged(page, pageSize);
			return competitions.Result;
		}

	}
}
