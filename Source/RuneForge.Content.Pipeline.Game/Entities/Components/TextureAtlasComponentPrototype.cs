using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Entities.Attributes;
using RuneForge.Content.Pipeline.Game.Entities.ComponentWriters;

namespace RuneForge.Content.Pipeline.Game.Entities.Components
{
    [ComponentPrototypeWriter(typeof(TextureAtlasComponentPrototypeWriter))]
    public class TextureAtlasComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Entities.Components.TextureAtlasComponentPrototype, RuneForge.Game";

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
