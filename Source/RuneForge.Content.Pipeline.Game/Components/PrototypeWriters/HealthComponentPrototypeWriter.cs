using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Components.Implementations;

namespace RuneForge.Content.Pipeline.Game.Components.PrototypeWriters
{
    public class HealthComponentPrototypeWriter : ComponentPrototypeWriter<HealthComponentPrototype>
    {
        public override void WriteComponentPrototype(ContentWriter contentWriter, HealthComponentPrototype componentPrototype)
        {
            contentWriter.Write(componentPrototype.Health);
            contentWriter.Write(componentPrototype.MaxHealth);
        }
    }
}
