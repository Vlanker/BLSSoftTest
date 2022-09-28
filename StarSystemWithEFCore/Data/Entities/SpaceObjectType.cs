namespace StarSystemWithEFCore.Data.Entities;

public class SpaceObjectType : IHaveIdentifier
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<SpaceObject> SpaceObjects { get; set; } = null!;

    public SpaceObjectType()
    {
        SpaceObjects = new HashSet<SpaceObject>();
    }
}