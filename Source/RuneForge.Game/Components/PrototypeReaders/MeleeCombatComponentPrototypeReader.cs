using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
{
    public class MeleeCombatComponentPrototypeReader : ComponentPrototypeReader<MeleeCombatComponentPrototype>
    {
        public override MeleeCombatComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            return new MeleeCombatComponentPrototype();
        }
    }
}
