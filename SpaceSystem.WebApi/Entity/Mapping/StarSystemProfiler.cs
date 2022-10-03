using AutoMapper;
using SpaceSystem.WebApi.Entity.Models.StarSystems;
using DataStarSystem = StarSystemWithEFCore.Data.Entities.StarSystem;

namespace SpaceSystem.WebApi.Entity.Mapping;

public class StarSystemProfiler : Profile
{
    public StarSystemProfiler()
    {
        CreateMap<DataStarSystem, StarSystemReadModel>();
        CreateMap<StarSystemCreateModel, DataStarSystem>();
        CreateMap<StarSystemReadModel, DataStarSystem>();
        CreateMap<StarSystemUpdateModel, DataStarSystem>();
    }
}