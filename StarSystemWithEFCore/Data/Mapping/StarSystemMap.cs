using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarSystemWithEFCore.Data.Entities;

namespace StarSystemWithEFCore.Data.Mapping;

public class StarSystemMap : IEntityTypeConfiguration<StarSystem>
{
    public void Configure(EntityTypeBuilder<StarSystem> builder)
    {
        builder.ToTable("star_systems", "public");

        builder.HasKey(starSystem => starSystem.Id)
            .HasName("star_system_pkey");

        builder.Property(starSystem => starSystem.Id)
            .HasColumnName("star_system_id")
            .IsRequired();

        builder.Property(starSystem => starSystem.Name)
            .HasColumnName("star_system_name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(starSystem => starSystem.Age)
            .HasColumnName("star_system_age");

        builder.Property(starSystem => starSystem.CenterOfGravityId)
            .HasColumnName("center_of_gravity_id")
            .IsRequired(false);

        builder.HasOne<SpaceObject>(starSystem => starSystem.CenterOfGravityStarSystem)
            .WithMany(spaceObject => spaceObject.CenterOfGravityStarSystems)
            .HasForeignKey(starSystem => starSystem.CenterOfGravityId)
            .HasConstraintName("star_system_center_of_gravity_id_fkey")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}