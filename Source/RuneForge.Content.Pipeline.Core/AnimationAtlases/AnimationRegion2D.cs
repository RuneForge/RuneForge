using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Core.TextureAtlases;

namespace RuneForge.Content.Pipeline.Core.AnimationAtlases
{
    public class AnimationRegion2D : TextureRegion2D
    {
        [XmlAttribute(AttributeName = "FrameTimeMilliseconds")]
        public float FrameTimeMilliseconds { get; set; }
    }
}
