using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
	public class CompetitionDto
	{
		public int Id { get; set; }

		public long TourId { get;  set; }

		public DateTime? StartDate { get; set; }

		public int Duration { get;  set; }

		public List<CompetitionApplyDto> CompetitionApplies { get; set; }

		public string Status { get; set; }
	}
}