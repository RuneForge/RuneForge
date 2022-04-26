using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
{
    public class UnitShelterOccupantComponentPrototypeReader : ComponentPrototypeReader<UnitShelterOccupantComponentPrototype>
    {
        public override UnitShelterOccupantComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            return new UnitShelterOccupantComponentPrototype();
        }
    }
}
