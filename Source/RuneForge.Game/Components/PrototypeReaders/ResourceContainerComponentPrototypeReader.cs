using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
{
    public class ResourceContainerComponentPrototypeReader : ComponentPrototypeReader<ResourceContainerComponentPrototype>
    {
        public override ResourceContainerComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            decimal goldAmount = contentReader.ReadDecimal();
            return new ResourceContainerComponentPrototype(goldAmount);
        }
    }
}
