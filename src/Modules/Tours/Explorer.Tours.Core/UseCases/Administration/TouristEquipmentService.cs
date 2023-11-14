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
    public class TouristEquipmentService : CrudService<TouristEquipmentDto, TouristEquipment>, ITouristEquipmentService
    {

        //private readonly ICrudRepository<Equipment> _equipmentRepository;

        public TouristEquipmentService(ICrudRepository<TouristEquipment> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<TouristEquipmentDto> AddToMyEquipment(int touristId, int equipmentId)
        {
            var allTouristEquipment = CrudRepository.GetPaged(0, 0).Results.ToList();
            var result = new TouristEquipmentDto();
            foreach (var item in allTouristEquipment)
            {
                if (item.TouristId == touristId)
                {
                    item.Equipment.Add(equipmentId);
                    CrudRepository.Update(item);
                    result = MapToDto(item);
                    break;
                }

            }

            return result;
        }

        public Result<TouristEquipmentDto> DeleteFromMyEquipment(int touristId, int equipmentId)
        {
            var allTouristEquipment = CrudRepository.GetPaged(0, 0).Results.ToList();
            var result = new TouristEquipmentDto();
            foreach (var item in allTouristEquipment)
            {
                if (item.TouristId == touristId)
                {
                    item.Equipment.Remove(equipmentId);
                    CrudRepository.Update(item);
                    result = MapToDto(item);
                    break;
                }

            }

            return result;
        }

        public Result<TouristEquipmentDto> GetTouristEquipment(int touristId)
        {
            var allTouristEquipment = CrudRepository.GetPaged(0, 0).Results.ToList();
            var result = new TouristEquipmentDto();
            foreach (var item in allTouristEquipment)
            {
                if (item.TouristId == touristId) {
                    result = MapToDto(item);
                    break;
                }

            }

            return result;
        }

        public Result<TouristEquipmentDto> Create(int touristId)
        {
            List<int> lista = new List<int>();
            TouristEquipment te = new TouristEquipment(touristId,lista);
            var result = CrudRepository.Create(te);
            return MapToDto(result);
        }
        /*
        private List<Equipment> GetByIds(List<int> ids)
        {
            List<Equipment> result = new List<Equipment>();

            foreach (int id in ids)
            {
                var res = _equipmentRepository.Get(id);
                result.Add(res);
            }

            return result;
        }
        */
    }
}
