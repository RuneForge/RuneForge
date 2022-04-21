using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
{
    public class OrderQueueComponentPrototypeReader : ComponentPrototypeReader<OrderQueueComponentPrototype>
    {
        public override OrderQueueComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            return new OrderQueueComponentPrototype();
        }
    }
}
