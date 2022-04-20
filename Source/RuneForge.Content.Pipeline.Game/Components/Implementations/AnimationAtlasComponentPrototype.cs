using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Components.Attributes;
using RuneForge.Content.Pipeline.Game.Components.PrototypeWriters;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
    [ComponentPrototypeWriter(typeof(AnimationAtlasComponentPrototypeWriter))]
    public class AnimationAtlasComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Components.Implementations.AnimationAtlasComponentPrototype, RuneForge.Game";

        [XmlAttribute(AttributeName = "AnimationAtlasAssetName")]
        public string AnimationAtlasAssetName { get; set; }

        [XmlAttribute(AttributeName = "HasPlayerColor")]
        public bool HasPlayerColor { get; set; }

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
