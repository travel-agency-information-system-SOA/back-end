using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
	public class TourCharacteristic : ValueObject
	{
		public double Distance { get;  private set; }

		public double Duration { get; private set; }

		public TransportType TransportType { get; private set; }

		[JsonConstructor]
		public TourCharacteristic(double distance, double duration, TransportType transportType)
		{
			Distance = distance;
			Duration = duration;
			TransportType = transportType;
		}

		
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Distance;
			yield return Duration;
			yield return TransportType;
		}
	}
}
