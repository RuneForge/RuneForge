using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Game.Units
{
    [XmlRoot]
    public class UnitPrototype
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
    }
}
