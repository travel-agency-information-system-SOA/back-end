using AutoMapper;
using Explorer.BuildingBlocks.Core;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TouristEquipmentService :CrudService<TouristEquipmentDto, TouristEquipment>, ITouristEquipmentService
{
    public TouristEquipmentService(ICrudRepository<TouristEquipment> repository, IMapper mapper) : base(repository, mapper) { }

     
    }
}
