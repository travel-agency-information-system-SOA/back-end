using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
	public class CompetitionApplyDto
	{
		public int Id { get; set; }

		public long CompetitionId { get; set; }

		public string ImageUrl { get; set; }

		public int UserId { get; set; }

		public int NumLikes { get; set; }
	}
}
