using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Entities.Attributes;
using RuneForge.Content.Pipeline.Game.Entities.ComponentWriters;

namespace RuneForge.Content.Pipeline.Game.Entities.Components
{
    [ComponentPrototypeWriter(typeof(AnimationAtlasComponentPrototypeWriter))]
    public class AnimationAtlasComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Entities.Components.AnimationAtlasComponentPrototype, RuneForge.Game";

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
