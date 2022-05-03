using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Components.Implementations;

namespace RuneForge.Content.Pipeline.Game.Components.PrototypeWriters
{
    public class ProductionCostComponentPrototypeWriter : ComponentPrototypeWriter<ProductionCostComponentPrototype>
    {
        public override void WriteComponentPrototype(ContentWriter contentWriter, ProductionCostComponentPrototype componentPrototype)
        {
            contentWriter.Write(componentPrototype.GoldAmount);
            contentWriter.Write(componentPrototype.ProductionTimeMilliseconds);
        }
    }
}
