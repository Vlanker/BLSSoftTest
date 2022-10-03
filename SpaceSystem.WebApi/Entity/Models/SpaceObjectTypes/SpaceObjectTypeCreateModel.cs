using System.ComponentModel.DataAnnotations;

namespace SpaceSystem.WebApi.Entity.Models.SpaceObjectTypes;

public class SpaceObjectTypeCreateModel
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = null!;
}