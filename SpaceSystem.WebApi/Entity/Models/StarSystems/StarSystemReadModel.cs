using SpaceSystem.WebApi.Entity.Models.SpaceObjects;

namespace SpaceSystem.WebApi.Entity.Models.StarSystems;

public class StarSystemReadModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Age { get; set; }
    public int? CenterOfGravityId { get; set; }
    public IEnumerable<SpaceObjectReadModel> SpaceObjects { get; set; } = null!;
}