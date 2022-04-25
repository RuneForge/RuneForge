using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Components.Implementations;

namespace RuneForge.Content.Pipeline.Game.Components.PrototypeWriters
{
    public class UnitShelterComponentPrototypeWriter : ComponentPrototypeWriter<UnitShelterComponentPrototype>
    {
        public override void WriteComponentPrototype(ContentWriter contentWriter, UnitShelterComponentPrototype componentPrototype)
        {
            contentWriter.Write(componentPrototype.OccupantsLimit);
        }
    }
}
