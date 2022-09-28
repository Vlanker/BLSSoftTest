using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarSystemWithEFCore.Data.Entities;

namespace StarSystemWithEFCore.Data.Mapping;

public class SpaceObjectMap : IEntityTypeConfiguration<SpaceObject>
{
    public void Configure(EntityTypeBuilder<SpaceObject> builder)
    {
        builder.ToTable("space_objects", "public");

        builder.HasKey(spaceObject => spaceObject.Id)
            .HasName("space_object_pkey");

        builder.Property(spaceObject => spaceObject.Id)
            .HasColumnName("space_object_id");

        builder.Property(spaceObject => spaceObject.Name)
            .HasColumnName("space_object_name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(spaceObject => spaceObject.Age)
            .HasColumnName("space_object_age")
            .IsRequired();

        builder.Property(spaceObject => spaceObject.Weight)
            .HasColumnName("space_object_weight")
            .IsRequired();

        builder.Property(spaceObject => spaceObject.Diameter)
            .HasColumnName("space_object_diameter")
            .IsRequired();

        builder.Property(spaceObject => spaceObject.SpaceObjectTypeId)
            .HasColumnName("space_object_space_object_type_id")
            .IsRequired();

        builder.Property(spaceObject => spaceObject.StarSystemId)
            .HasColumnName("space_object_star_system_type_id")
            .IsRequired();

        builder.HasOne<SpaceObjectType>(spaceObject => spaceObject.SpaceObjectType)
            .WithMany(spaceObjectType => spaceObjectType.SpaceObjects)
            .HasForeignKey(spaceObject => spaceObject.SpaceObjectTypeId)
            .HasConstraintName("space_object_space_object_type_id_fkey")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<StarSystem>(spaceObject => spaceObject.StarSystem)
            .WithMany(starSystem => starSystem.SpaceObjects)
            .HasForeignKey(spaceObject => spaceObject.StarSystemId)
            .HasConstraintName("space_object_star_system_id_fkey")
            .OnDelete(DeleteBehavior.Restrict);
    }
}