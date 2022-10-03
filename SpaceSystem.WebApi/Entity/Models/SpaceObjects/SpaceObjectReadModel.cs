namespace SpaceSystem.WebApi.Entity.Models.SpaceObjects;

public class SpaceObjectReadModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Age { get; set; }
    public double Diameter { get; set; }
    public double Weight { get; set; }
    public int SpaceObjectTypeId { get; set; }
    public int StarSystemId { get; set; }
}