using AutoMapper;
using SpaceSystem.WebApi.Entity.Models.SpaceObjects;
using DataSpaceObject = StarSystemWithEFCore.Data.Entities.SpaceObject;

namespace SpaceSystem.WebApi.Entity.Mapping;

public class SpaceObjectProfiler : Profile
{
    public SpaceObjectProfiler()
    {
        CreateMap<DataSpaceObject, SpaceObjectReadModel>();
        CreateMap<SpaceObjectCreateModel, DataSpaceObject>();
        CreateMap<SpaceObjectReadModel, DataSpaceObject>();
        CreateMap<SpaceObjectUpdateModel, DataSpaceObject>();
    }
}