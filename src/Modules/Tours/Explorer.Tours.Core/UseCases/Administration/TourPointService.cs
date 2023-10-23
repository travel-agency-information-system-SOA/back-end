using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourPointService : CrudService<TourPointDto, TourPoint>, ITourPointService
    {
        public TourPointService(ICrudRepository<TourPoint> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
