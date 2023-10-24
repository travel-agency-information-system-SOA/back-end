using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ITourKeyPointService
    {
        Task<Result<List<TourKeyPointDto>>> GetTourKeyPointAsync(long tourId);
        Task<Result> AddKeyPointToTourAsync(long tourId, long pointId);
        Task<Result> RemoveKeyPointFromTourAsync(long tourId, long pointId);
    }
}
