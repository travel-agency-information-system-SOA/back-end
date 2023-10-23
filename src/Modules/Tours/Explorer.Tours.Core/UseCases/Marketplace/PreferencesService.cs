using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Marketplace;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Marketplace
{
    public class PreferencesService : CrudService<PreferencesDto, Preferences>, IPreferencesService
    {
        public PreferencesService(ICrudRepository<Preferences> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<PreferencesDto> GetByUserId(int page, int pageSize, int userId)
        {
            var result = CrudRepository.GetPaged(page, pageSize);
            var dto = result.Results.FirstOrDefault(x => x.UserId == userId);

            if (dto != null)
            {
                return MapToDto(dto);
            }
            else
            {
                return Result.Fail(FailureCode.NotFound);
            }
        }
    }
}
