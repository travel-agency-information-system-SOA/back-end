using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Explorer.Tours.API.Dtos
{
	public class TourDTO
	{
	    public int Id { get; set; }
		public string Name {  get; set; }

		public string? Description { get; set; }

		public string DifficultyLevel { get; set; }

		public List<string> Tags { get; set; }
		
        public int Price { get; set; }

		public string Status { get; set; }

		public int GuideId { get; set; }
	}
}
