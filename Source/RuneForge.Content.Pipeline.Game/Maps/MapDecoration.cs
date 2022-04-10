using System.Xml.Serialization;

using RuneForge.Game.Maps;

namespace RuneForge.Content.Pipeline.Game.Maps
{
    public class MapDecoration
    {
        [XmlAttribute(AttributeName = "Tier")]
        public MapDecorationTier Tier { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public MapDecorationType Type { get; set; }
    }
}
