using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourEquipment : Entity
    {
        public long TourId { get; set; }  
        public long EquipmentId { get; set; }  

        public bool IsSelected { get; set; }
        public TourEquipment()
        {
            
        }

        public TourEquipment(long tourId, long equipmentId, bool isSelected)
        {
            TourId = tourId;
            EquipmentId = equipmentId;
            IsSelected = isSelected;
        }
    }
}
