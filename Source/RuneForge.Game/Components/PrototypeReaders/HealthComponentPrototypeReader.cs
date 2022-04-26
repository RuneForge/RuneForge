using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
{
    public class HealthComponentPrototypeReader : ComponentPrototypeReader<HealthComponentPrototype>
    {
        public override HealthComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            decimal health = contentReader.ReadDecimal();
            decimal maxHealth = contentReader.ReadDecimal();
            return new HealthComponentPrototype(health, maxHealth);
        }
    }
}
