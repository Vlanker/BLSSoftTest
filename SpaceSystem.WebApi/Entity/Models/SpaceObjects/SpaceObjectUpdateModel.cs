using System.ComponentModel.DataAnnotations;

namespace SpaceSystem.WebApi.Entity.Models.SpaceObjects;

public class SpaceObjectUpdateModel
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = null!;
    public double Age { get; set; }
    public double Diameter { get; set; }
    public double Weight { get; set; }
    [Required] public int SpaceObjectTypeId { get; set; }
    [Required] public int StarSystemId { get; set; }
}