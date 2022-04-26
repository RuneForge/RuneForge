using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
{
    public class UnitShelterComponentPrototypeReader : ComponentPrototypeReader<UnitShelterComponentPrototype>
    {
        public override UnitShelterComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            int occupantsLimit = contentReader.ReadInt32();
            return new UnitShelterComponentPrototype(occupantsLimit);
        }
    }
}
