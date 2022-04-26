using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
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
