using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Components.Attributes;
using RuneForge.Content.Pipeline.Game.Components.PrototypeWriters;
using RuneForge.Game.Components.Entities;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
    [ComponentPrototypeWriter(typeof(ResourceStorageComponentPrototypeWriter))]
    public class ResourceStorageComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Components.Implementations.ResourceStorageComponentPrototype, RuneForge.Game";

        [XmlAttribute(AttributeName = "AcceptedResourceTypes")]
        public ResourceTypes AcceptedResourceTypes { get; set; }

        [XmlAttribute(AttributeName = "TransferTimeMilliseconds")]
        public float TransferTimeMilliseconds { get; set; }

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
