using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos.BundlePayRecord
{
    public class BundlePayRecordDto
    {
        public int? Id { get; set; } = 0;
        public int TouristId { get; set; }
        public int BundleId { get; init; }

        public double Price { get; init; }
        public DateTime DateCreated { get; init; }
    }
}
