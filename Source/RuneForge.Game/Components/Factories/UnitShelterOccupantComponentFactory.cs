using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class UnitShelterOccupantComponentFactory : ComponentFactory<UnitShelterOccupantComponent, UnitShelterOccupantComponentPrototype>
    {
        public override UnitShelterOccupantComponent CreateComponentFromPrototype(UnitShelterOccupantComponentPrototype componentPrototype, UnitShelterOccupantComponentPrototype componentPrototypeOverride)
        {
            return new UnitShelterOccupantComponent();
        }
    }
}
