using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
	public class TourCharacteristicDTO
	{
		public int Distance {  get; set; }

		public TimeSpan Duration { get; set; }

		public string TransportType { get; set; }
	}
}
