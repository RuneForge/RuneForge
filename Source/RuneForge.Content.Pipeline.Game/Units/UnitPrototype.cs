using System.Collections.Generic;
using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Entities;
using RuneForge.Content.Pipeline.Game.Entities.Components;

namespace RuneForge.Content.Pipeline.Game.Units
{
    [XmlRoot]
    public class UnitPrototype
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlArray(ElementName = "ComponentPrototypes")]
        [XmlArrayItem(ElementName = "LocationComponentPrototype", Type = typeof(LocationComponentPrototype))]
        [XmlArrayItem(ElementName = "DirectionComponentPrototype", Type = typeof(DirectionComponentPrototype))]
        public List<ComponentPrototype> ComponentPrototypes { get; set; }
    }
}
