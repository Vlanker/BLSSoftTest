namespace StarSystemWithEFCore.Data.Entities;

public class SpaceObject : IHaveIdentifier
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Age { get; set; }
    public double Diameter { set; get; }
    public double Weight { get; set; }
    public int SpaceObjectTypeId { get; set; }
    public int StarSystemId { get; set; }

    public virtual SpaceObjectType SpaceObjectType { get; set; } = null!;
    public virtual ICollection<StarSystem> CenterOfGravityStarSystems { get; set; } = null!;
    public virtual StarSystem StarSystem { get; set; } = null!;

    public SpaceObject()
    {
        CenterOfGravityStarSystems = new HashSet<StarSystem>();
    }
}