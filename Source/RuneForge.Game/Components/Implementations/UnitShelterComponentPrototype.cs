using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Factories;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
    [ComponentFactory(typeof(UnitShelterComponentFactory))]
    [ComponentPrototypeReader(typeof(UnitShelterComponentPrototypeReader))]
    public class UnitShelterComponentPrototype : ComponentPrototype
    {
        public int OccupantsLimit { get; }

        public UnitShelterComponentPrototype(int occupantsLimit)
        {
            OccupantsLimit = occupantsLimit;
        }
    }
}
