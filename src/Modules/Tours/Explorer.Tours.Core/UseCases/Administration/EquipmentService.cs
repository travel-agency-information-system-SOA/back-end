using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration;

public class EquipmentService : CrudService<EquipmentDto, Equipment>, IEquipmentService
{
    public EquipmentService(ICrudRepository<Equipment> repository, IMapper mapper) : base(repository, mapper) {}

    public Result<PagedResult<EquipmentDto>> GetById(List<int> ids)
    {
        List<EquipmentDto> result = new List<EquipmentDto>();
        foreach (int id in ids) {
            var res = CrudRepository.Get(id);
            
            result.Add(MapToDto(res));
        }
        var pagedResult = new PagedResult<EquipmentDto>(result, result.Count);
        return Result.Ok(pagedResult);
    }
}