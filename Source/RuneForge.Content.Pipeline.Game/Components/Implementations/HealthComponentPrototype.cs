using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Components.Attributes;
using RuneForge.Content.Pipeline.Game.Components.PrototypeWriters;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
    [ComponentPrototypeWriter(typeof(HealthComponentPrototypeWriter))]
    public class HealthComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Components.Implementations.HealthComponentPrototype, RuneForge.Game";

        [XmlAttribute(AttributeName = "Health")]
        public decimal Health { get; set; }

        [XmlAttribute(AttributeName = "MaxHealth")]
        public decimal MaxHealth { get; set; }

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
