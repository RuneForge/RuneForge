using System.Collections.Generic;
using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Core.TextureAtlases
{
    [XmlRoot(ElementName = "TextureAtlas")]
    public class TextureAtlas
    {
        [XmlElement(ElementName = "TextureAssetName")]
        public string TextureAssetName { get; set; }

        [XmlArray(ElementName = "TextureRegions")]
        [XmlArrayItem(ElementName = "TextureRegion")]
        public List<TextureRegion2D> TextureRegions { get; set; }
    }
}
