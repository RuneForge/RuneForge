using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Components.Attributes;
using RuneForge.Content.Pipeline.Game.Components.PrototypeWriters;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
    [ComponentPrototypeWriter(typeof(ProductionCostComponentPrototypeWriter))]
    public class ProductionCostComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Components.Implementations.ProductionCostComponentPrototype, RuneForge.Game";

        [XmlAttribute(AttributeName = "GoldAmount")]
        public decimal GoldAmount { get; set; }

        [XmlAttribute(AttributeName = "ProductionTime")]
        public float ProductionTimeMilliseconds { get; set; }

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
