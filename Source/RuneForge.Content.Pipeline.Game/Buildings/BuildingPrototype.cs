using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Game.Buildings
{
    [XmlRoot]
    public class BuildingPrototype
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
    }
}
