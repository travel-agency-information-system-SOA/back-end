using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Competitions
{
	public class Competition : Entity
	{
        public long TourId { get; private set; }

		public DateTime? StartDate {  get; set; }

		public int Duration { get; private set; }

		public ICollection<CompetitionApply> CompetitionApplies { get; } = new List<CompetitionApply>();

		public CompetitionStatus Status { get; set; }

		public Competition() { }

		public Competition(long tourId, DateTime? startDate, int duration, CompetitionStatus status)
		{
			TourId = tourId;
			StartDate = startDate;
			Duration = duration;
			Status = status;
		}





	}
}
