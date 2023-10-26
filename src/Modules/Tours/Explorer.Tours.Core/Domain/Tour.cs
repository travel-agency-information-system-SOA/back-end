using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
	public class Tour : Entity	
	{
		public string Name { get; init; }

		public DifficultyLevel DifficultyLevel { get; init; }

		public string Description { get; init; }

		public List<string> Tags { get; init; }

		public TourStatus Status { get; init; }

		public int Price { get; init; }

		public int GuideId { get; init; }

		public Tour(string name, DifficultyLevel difficultyLevel, string? description, int guideId)
		{
			if (string.IsNullOrWhiteSpace(name) || guideId == 0) throw new ArgumentException("Field empty.");

			if (!Enum.IsDefined(typeof(DifficultyLevel), difficultyLevel))
			{
				throw new ArgumentException("Invalid DifficultyLevel value.");
			}
			Name = name;
			DifficultyLevel = difficultyLevel;
			Description = description;
			Status = TourStatus.Draft;
			Price = 0;
			GuideId = guideId;

		}
	}
}
