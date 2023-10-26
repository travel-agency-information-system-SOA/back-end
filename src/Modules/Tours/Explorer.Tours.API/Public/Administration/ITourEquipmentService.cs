using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ITourEquipmentService
    {
        Task<Result<List<TourEquipmentDto>>> GetTourEquipmentAsync(long tourId);
        Task<Result> AddEquipmentToTourAsync(long tourId, long equipmentId);
        Task<Result> RemoveEquipmentFromTourAsync(long tourId, long equipmentId);
    }
}
