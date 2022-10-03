using AutoMapper;
using SpaceSystem.WebApi.Entity.Models.SpaceObjectTypes;
using DataSpaceObjectType = StarSystemWithEFCore.Data.Entities.SpaceObjectType;

namespace SpaceSystem.WebApi.Entity.Mapping;

public class SpaceObjectTypeProfiler : Profile
{
    public SpaceObjectTypeProfiler()
    {
        CreateMap<DataSpaceObjectType, SpaceObjectTypeReadModel>();
        CreateMap<SpaceObjectTypeCreateModel, DataSpaceObjectType>();
        CreateMap<SpaceObjectTypeReadModel, DataSpaceObjectType>();
        CreateMap<SpaceObjectTypeUpdateModel, DataSpaceObjectType>();
    }
}