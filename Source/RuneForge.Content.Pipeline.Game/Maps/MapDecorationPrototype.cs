using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Game.Maps
{
    public class MapDecorationPrototype
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
    }
}
