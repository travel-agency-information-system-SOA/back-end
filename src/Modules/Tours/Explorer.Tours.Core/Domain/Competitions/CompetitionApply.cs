using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Competitions
{
	public class CompetitionApply : Entity
    {
		public long CompetitionId { get; set; }

		public Competition? Competition { get; set; }

		public string ImageUrl { get; set; }

		public int UserId { get; set; }

		public int NumLikes { get; set; }


		public CompetitionApply(long competitionId, string imageUrl, int userId, int numLikes)
		{
			CompetitionId = competitionId;
			ImageUrl = imageUrl;
			UserId = userId;
			NumLikes = numLikes;
		}


	}
}
