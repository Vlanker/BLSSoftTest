using System.ComponentModel.DataAnnotations;

namespace SpaceSystem.WebApi.Entity.Models.StarSystems;

public class StarSystemCreateModel
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = null!;
    public double Age { get; set; }
    public int? CenterOfGravityId { get; set; }
}