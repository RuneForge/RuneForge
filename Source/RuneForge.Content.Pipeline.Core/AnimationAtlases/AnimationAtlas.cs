using System.Collections.Generic;
using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Core.AnimationAtlases
{
    [XmlRoot(ElementName = "AnimationAtlas")]
    public class AnimationAtlas
    {
        [XmlElement(ElementName = "TextureAssetName")]
        public string TextureAssetName { get; set; }

        [XmlArray(ElementName = "Animations")]
        [XmlArrayItem(ElementName = "Animation")]
        public List<Animation2D> Animations { get; set; }
    }
}
