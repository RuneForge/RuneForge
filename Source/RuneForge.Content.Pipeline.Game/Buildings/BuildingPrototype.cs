using System.Collections.Generic;
using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Entities;

namespace RuneForge.Content.Pipeline.Game.Buildings
{
    [XmlRoot]
    public class BuildingPrototype
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlArray(ElementName = "ComponentPrototypes")]
        public List<ComponentPrototype> ComponentPrototypes { get; set; }
    }
}
