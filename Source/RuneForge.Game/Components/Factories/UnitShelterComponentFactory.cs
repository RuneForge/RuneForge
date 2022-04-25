using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class UnitShelterComponentFactory : ComponentFactory<UnitShelterComponent, UnitShelterComponentPrototype>
    {
        public override UnitShelterComponent CreateComponentFromPrototype(UnitShelterComponentPrototype componentPrototype, UnitShelterComponentPrototype componentPrototypeOverride)
        {
            return new UnitShelterComponent(componentPrototype.OccupantsLimit);
        }
    }
}
