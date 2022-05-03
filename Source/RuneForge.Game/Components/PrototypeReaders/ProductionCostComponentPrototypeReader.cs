using System;

using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
{
    public class ProductionCostComponentPrototypeReader : ComponentPrototypeReader<ProductionCostComponentPrototype>
    {
        public override ProductionCostComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            decimal goldAmount = contentReader.ReadDecimal();
            TimeSpan productionTime = TimeSpan.FromMilliseconds(contentReader.ReadSingle());
            return new ProductionCostComponentPrototype(goldAmount, productionTime);
        }
    }
}
