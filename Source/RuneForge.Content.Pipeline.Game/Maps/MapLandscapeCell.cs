using System.Xml.Serialization;

using RuneForge.Game.Maps;

namespace RuneForge.Content.Pipeline.Game.Maps
{
    public class MapLandscapeCell
    {
        [XmlAttribute(AttributeName = "Tier")]
        public MapLandscapeCellTier Tier { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public MapLandscapeCellTypes Type { get; set; }
    }
}
