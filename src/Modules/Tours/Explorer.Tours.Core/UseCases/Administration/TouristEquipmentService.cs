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


        public Result<ObservableCollection<TouristEquipmentDto>> GetOtherEquipment(int touristId)
        {
            var allEquipment = CrudRepository.GetPaged(0, 0).Results.ToList();
            var results = new ObservableCollection<TouristEquipmentDto>();
            foreach (var item in allEquipment)
            {
                if (item.TouristId != touristId) results.Add(MapToDto(item));
            }

            return results;
        }

        public Result<ObservableCollection<TouristEquipmentDto>> GetTouristEquipment(int touristId)
        {
            var allEquipment = CrudRepository.GetPaged(0, 0).Results.ToList();
            var results = new ObservableCollection<TouristEquipmentDto>();
            foreach (var item in allEquipment)
            {
                if (item.TouristId == touristId) results.Add(MapToDto(item));
            }

            return results;
        }
    }
}
