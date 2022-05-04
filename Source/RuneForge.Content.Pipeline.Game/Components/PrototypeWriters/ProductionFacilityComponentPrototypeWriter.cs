using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Components.Implementations;

namespace RuneForge.Content.Pipeline.Game.Components.PrototypeWriters
{
    public class ProductionFacilityComponentPrototypeWriter : ComponentPrototypeWriter<ProductionFacilityComponentPrototype>
    {
        public override void WriteComponentPrototype(ContentWriter contentWriter, ProductionFacilityComponentPrototype componentPrototype)
        {
            List<string> unitCodesProduced = componentPrototype.UnitCodesProduced;
            if (unitCodesProduced == null)
                throw new InvalidOperationException("The component should have at least an empty list of produced units' codes.");
            else
            {
                contentWriter.Write(unitCodesProduced.Count);
                foreach (string unitCodeProduced in unitCodesProduced)
                    contentWriter.Write(unitCodeProduced);
            }
        }
    }
}
