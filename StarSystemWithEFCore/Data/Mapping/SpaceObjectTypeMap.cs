using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarSystemWithEFCore.Data.Entities;

namespace StarSystemWithEFCore.Data.Mapping;

public class SpaceObjectTypeMap : IEntityTypeConfiguration<SpaceObjectType>
{
    public void Configure(EntityTypeBuilder<SpaceObjectType> builder)
    {
        builder.ToTable("space_object_types", "public");

        builder.HasKey(spaceObjectType => spaceObjectType.Id)
            .HasName("space_object_type_pkey");

        builder.Property(spaceObjectType => spaceObjectType.Id)
            .HasColumnName("space_object_type_id");

        builder.Property(spaceObjectType => spaceObjectType.Name)
            .HasColumnName("space_object_type_name")
            .HasMaxLength(255)
            .IsRequired();
    }
}