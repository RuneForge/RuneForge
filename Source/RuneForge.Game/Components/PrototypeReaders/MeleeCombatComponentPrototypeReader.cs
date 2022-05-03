using System;

using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
{
    public class MeleeCombatComponentPrototypeReader : ComponentPrototypeReader<MeleeCombatComponentPrototype>
    {
        public override MeleeCombatComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            decimal attackPower = contentReader.ReadDecimal();
            TimeSpan cycleTime = TimeSpan.FromMilliseconds(contentReader.ReadSingle());
            TimeSpan actionTime = TimeSpan.FromMilliseconds(contentReader.ReadSingle());
            return new MeleeCombatComponentPrototype(attackPower, cycleTime, actionTime);
        }
    }
}
