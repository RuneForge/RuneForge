using System.Xml.Serialization;

using RuneForge.Game.Maps;

namespace RuneForge.Content.Pipeline.Game.Maps
{
    public class MapTilesetDecorationPrototype
    {
        [XmlAttribute(AttributeName = "Tier")]
        public MapDecorationTier Tier { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public MapDecorationTypes Type { get; set; }

        [XmlElement(ElementName = "TextureRegionName")]
        public string TextureRegionName { get; set; }
    }
}
