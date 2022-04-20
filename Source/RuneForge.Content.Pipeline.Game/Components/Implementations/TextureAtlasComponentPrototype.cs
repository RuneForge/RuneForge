using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Components.Attributes;
using RuneForge.Content.Pipeline.Game.Components.PrototypeWriters;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
    [ComponentPrototypeWriter(typeof(TextureAtlasComponentPrototypeWriter))]
    public class TextureAtlasComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Components.Implementations.TextureAtlasComponentPrototype, RuneForge.Game";

        [XmlAttribute(AttributeName = "TextureAtlasAssetName")]
        public string TextureAtlasAssetName { get; set; }

        [XmlAttribute(AttributeName = "HasPlayerColor")]
        public bool HasPlayerColor { get; set; }

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
