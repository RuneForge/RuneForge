using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Components.Implementations;

namespace RuneForge.Content.Pipeline.Game.Components.PrototypeWriters
{
    public class MovementComponentPrototypeWriter : ComponentPrototypeWriter<MovementComponentPrototype>
    {
        public override void WriteComponentPrototype(ContentWriter contentWriter, MovementComponentPrototype componentPrototype)
        {
            contentWriter.Write(componentPrototype.MovementSpeed);
        }
    }
}
