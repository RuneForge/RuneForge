using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
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
