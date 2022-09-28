using StarSystemWithEFCore.Data.Entities;

namespace StarSystemWithEFCore.Data;

public static class DbInitialize
{
    public static void Initialize(StarSystemContext context)
    {
        if (context.SpaceObjectTypes.Any())
        {
            return; // DB has been seaded
        }

        // populate database with sample data
        var spaceObjectTypes = new SpaceObjectType[]
        {
            new()
            {
                Id = 1,
                Name = "Star"
            },

            new()
            {
                Id = 2,
                Name = "Planet"
            },

            new()
            {
                Id = 3,
                Name = "Planetary satellite"
            },

            new()
            {
                Id = 4,
                Name = "Asteroid"
            },

            new()
            {
                Id = 5,
                Name = "Comet"
            },

            new()
            {
                Id = 6,
                Name = "Space debris"
            }
        };

        context.SpaceObjectTypes.AddRange(spaceObjectTypes);
        context.SaveChanges();

        StarSystem solarSystem = new()
        {
            Id = 1,
            Name = "Solar System",
            Age = 4.57e9,
        };

        context.StarSystems.Add(solarSystem);
        context.SaveChanges();

        var spaceObjects = new SpaceObject[]
        {
            new()
            {
                Id = 1,
                Name = "Sun",
                Age = 4.54e9,
                Diameter = 1.392e6,
                Weight = 1.98847e30,
                SpaceObjectTypeId = 1,
                StarSystemId = 1
            },

            new()
            {
                Id = 2,
                Name = "Earth",
                Age = 4.54e9,
                Diameter = 12742,
                Weight = 5.9722e24,
                SpaceObjectTypeId = 2,
                StarSystemId = 1
            },

            new()
            {
                Id = 3,
                Name = "Moon",
                Age = 4.36e9,
                Diameter = 3476,
                Weight = 7.3477 * 1022,
                SpaceObjectTypeId = 3,
                StarSystemId = 1
            },

            new()
            {
                Id = 4,
                Name = "Lennon Asteroid",
                Age = double.NaN,
                Diameter = 15,
                Weight = double.NaN,
                SpaceObjectTypeId = 4,
                StarSystemId = 1
            },

            new()
            {
                Id = 5,
                Name = "Comet Hale–Bop (C/1995 O1)",
                Age = double.NaN,
                Diameter = double.NaN,
                Weight = double.NaN,
                SpaceObjectTypeId = 5,
                StarSystemId = 1
            },

            new()
            {
                Id = 6,
                Name = "WT1190F ",
                Age = 1,
                Diameter = 1,
                Weight = .1,
                SpaceObjectTypeId = 6,
                StarSystemId = 1
            }
        };

        context.SpaceObjects.AddRange(spaceObjects);
        context.SaveChanges();

        solarSystem.CenterOfGravityId = spaceObjects[0].Id;
        solarSystem.SpaceObjects = spaceObjects; 

        context.StarSystems.Update(solarSystem);
        context.SaveChanges();
    }
}