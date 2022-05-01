using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
{
    public class DurabilityComponentPrototypeReader : ComponentPrototypeReader<DurabilityComponentPrototype>
    {
        public override DurabilityComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            decimal durability = contentReader.ReadDecimal();
            decimal maxDurability = contentReader.ReadDecimal();
            return new DurabilityComponentPrototype(durability, maxDurability);
        }
    }
}
