using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class TourPointExecution : Entity
    {
        public long TourExecutionId { get; private set; }
        public int TourPointId { get; init; }
        public DateTime? CompletionTime { get; init; }
        public bool Completed { get; init; }
        public string Secret { get; private set; }
        public TourExecution? ТоurExecution {get; init; }
        public TourPointExecution(long tourExecutionId, int tourPointId, DateTime? completionTime)
        {
            TourExecutionId = tourExecutionId;
            TourPointId = tourPointId;
            CompletionTime = completionTime;
            Completed = false;
            Secret = "First reach the tour point to reveal secret ! ";
            Validate();
        }
        private void Validate()
        {
            if (TourExecutionId < 0)
                throw new ArgumentException("TourExecutionId must be a positive integer.");
            if (TourPointId < 0)
                throw new ArgumentException("TourPointId must be a positive integer.");
        }

        public void SetId(int id)
        {
            Id = id;
        }
        


    }
}
