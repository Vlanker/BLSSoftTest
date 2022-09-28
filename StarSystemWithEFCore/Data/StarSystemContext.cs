using Microsoft.EntityFrameworkCore;
using StarSystemWithEFCore.Data.Entities;
using StarSystemWithEFCore.Data.Mapping;

namespace StarSystemWithEFCore.Data;

#pragma warning disable CS1591
public class StarSystemContext : DbContext
{
    // these properties map to tables in the database
    public virtual DbSet<SpaceObject> SpaceObjects { get; set; } = null!;
    public virtual DbSet<SpaceObjectType> SpaceObjectTypes { get; set; } = null!;
    public virtual DbSet<StarSystem> StarSystems { get; set; } = null!;

    public StarSystemContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        optionsBuilder.UseNpgsql(ProjectConstants.DefaultConnection);
        // optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SpaceObjectTypeMap());
        modelBuilder.ApplyConfiguration(new SpaceObjectMap());
        modelBuilder.ApplyConfiguration(new StarSystemMap());
    }
}
#pragma warning restore CS1591