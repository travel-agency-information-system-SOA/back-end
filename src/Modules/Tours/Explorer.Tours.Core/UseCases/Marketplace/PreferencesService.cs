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
    }
}
