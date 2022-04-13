using System.Xml.Serialization;

using RuneForge.Game.Maps;

namespace RuneForge.Content.Pipeline.Game.Maps
{
    public class MapDecorationCell
    {
        [XmlAttribute(AttributeName = "Tier")]
        public MapDecorationCellTier Tier { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public MapDecorationCellTypes Type { get; set; }
    }
}
