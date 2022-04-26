using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Components.Attributes;
using RuneForge.Content.Pipeline.Game.Components.PrototypeWriters;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
    [ComponentPrototypeWriter(typeof(DurabilityComponentPrototypeWriter))]
    public class DurabilityComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Components.Implementations.DurabilityComponentPrototype, RuneForge.Game";

        [XmlAttribute(AttributeName = "Durability")]
        public decimal Durability { get; set; }

        [XmlAttribute(AttributeName = "MaxDurability")]
        public decimal MaxDurability { get; set; }

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
