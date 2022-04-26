using System.Collections.Generic;
using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Components;
using RuneForge.Content.Pipeline.Game.Components.Implementations;

namespace RuneForge.Content.Pipeline.Game.Units
{
    [XmlRoot]
    public class UnitPrototype
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlArray(ElementName = "ComponentPrototypes")]
        [XmlArrayItem(ElementName = "TextureAtlasComponentPrototype", Type = typeof(TextureAtlasComponentPrototype))]
        [XmlArrayItem(ElementName = "AnimationAtlasComponentPrototype", Type = typeof(AnimationAtlasComponentPrototype))]
        [XmlArrayItem(ElementName = "AnimationStateComponentPrototype", Type = typeof(AnimationStateComponentPrototype))]
        [XmlArrayItem(ElementName = "OrderQueueComponentPrototype", Type = typeof(OrderQueueComponentPrototype))]
        [XmlArrayItem(ElementName = "LocationComponentPrototype", Type = typeof(LocationComponentPrototype))]
        [XmlArrayItem(ElementName = "DirectionComponentPrototype", Type = typeof(DirectionComponentPrototype))]
        [XmlArrayItem(ElementName = "MovementComponentPrototype", Type = typeof(MovementComponentPrototype))]
        [XmlArrayItem(ElementName = "ResourceContainerComponentPrototype", Type = typeof(ResourceContainerComponentPrototype))]
        [XmlArrayItem(ElementName = "UnitShelterOccupantComponentPrototype", Type = typeof(UnitShelterOccupantComponentPrototype))]
        [XmlArrayItem(ElementName = "HealthComponentPrototype", Type = typeof(HealthComponentPrototype))]
        public List<ComponentPrototype> ComponentPrototypes { get; set; }
    }
}
