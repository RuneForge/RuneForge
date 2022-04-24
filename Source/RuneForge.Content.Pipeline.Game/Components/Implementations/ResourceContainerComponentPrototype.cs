using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
    public class ResourceContainerComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Components.Implementations.ResourceContainerComponentPrototype, RuneForge.Game";

        [XmlAttribute(AttributeName = "GoldAmount")]
        public decimal GoldAmount { get; set; }

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
