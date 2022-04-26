using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Components.Implementations;

namespace RuneForge.Content.Pipeline.Game.Components.PrototypeWriters
{
    public class DurabilityComponentPrototypeWriter : ComponentPrototypeWriter<DurabilityComponentPrototype>
    {
        public override void WriteComponentPrototype(ContentWriter contentWriter, DurabilityComponentPrototype componentPrototype)
        {
            contentWriter.Write(componentPrototype.Durability);
            contentWriter.Write(componentPrototype.MaxDurability);
        }
    }
}
