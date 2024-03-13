using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System.Collections.ObjectModel;

namespace Explorer.Tours.Core.UseCases.Administration;

public class EquipmentService : CrudService<EquipmentDto, Equipment>, IEquipmentService
{
    
    public EquipmentService(ICrudRepository<Equipment> repository, IMapper mapper) : base(repository, mapper) {}

    public Result<ObservableCollection<EquipmentDto>> GetOtherEquipment(List<int> ids)
    {
        var result = CrudRepository.GetPaged(0, 0).Results.ToList();
        var allEquipment = CrudRepository.GetPaged(0, 0).Results.ToList(); ;
        foreach (var id in ids)
        {
            foreach(var item in allEquipment)
            {
                if(id == item.Id) result.Remove(item);
            }
        }
        
        var ret = new ObservableCollection<EquipmentDto>((IEnumerable<EquipmentDto>)MapToDto(result).Value);
        return ret;
    }

    public Result<ObservableCollection<EquipmentDto>> GetTouristEquipment(List<int> ids)
    {
        List<EquipmentDto> result = new List<EquipmentDto>();
        foreach (int id in ids) {
            var res = CrudRepository.Get(id);
            
            result.Add(MapToDto(res));
        }

        var ret = new ObservableCollection<EquipmentDto>(result);
        return ret;
    }
}