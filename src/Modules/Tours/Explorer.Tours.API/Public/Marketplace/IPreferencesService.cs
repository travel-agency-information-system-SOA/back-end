using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Marketplace
{
    public interface IPreferencesService
    {
        Result<PagedResult<PreferencesDto>> GetPaged(int page, int pageSize);
        Result<PreferencesDto> Create(PreferencesDto preferences);
        Result<PreferencesDto> Update(PreferencesDto preferences);
        Result Delete(int id);
        Result<PreferencesDto> GetByUserId(int page, int pageSize, int userId);
    }
}
