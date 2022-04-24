using System.Xml.Serialization;

using RuneForge.Game.Components.Entities;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
    public class ResourceSourceComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Components.Implementations.ResourceSourceComponentPrototype, RuneForge.Game";

        [XmlAttribute(AttributeName = "ResourceType")]
        public ResourceTypes ResourceType { get; set; }

        [XmlAttribute(AttributeName = "AmountGiven")]
        public decimal AmountGiven { get; set; }

        [XmlAttribute(AttributeName = "ExtractionTimeMilliseconds")]
        public float ExtractionTimeMilliseconds { get; set; }

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
