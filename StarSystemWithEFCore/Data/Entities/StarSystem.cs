namespace StarSystemWithEFCore.Data.Entities;

public class StarSystem : IHaveIdentifier
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Age { get; set; }
    public int? CenterOfGravityId { get; set; }

    public virtual SpaceObject CenterOfGravityStarSystem { get; set; } = null!;
    public virtual ICollection<SpaceObject> SpaceObjects { get; set; } = null!;

    public StarSystem()
    {
        SpaceObjects = new HashSet<SpaceObject>();
    }
}