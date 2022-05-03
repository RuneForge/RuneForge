using System.Collections.Generic;
using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Components;
using RuneForge.Content.Pipeline.Game.Components.Implementations;

namespace RuneForge.Content.Pipeline.Game.Buildings
{
    [XmlRoot]
    public class BuildingPrototype
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Code")]
        public string Code { get; set; }

        [XmlArray(ElementName = "ComponentPrototypes")]
        [XmlArrayItem(ElementName = "TextureAtlasComponentPrototype", Type = typeof(TextureAtlasComponentPrototype))]
        [XmlArrayItem(ElementName = "AnimationAtlasComponentPrototype", Type = typeof(AnimationAtlasComponentPrototype))]
        [XmlArrayItem(ElementName = "OrderQueueComponentPrototype", Type = typeof(OrderQueueComponentPrototype))]
        [XmlArrayItem(ElementName = "LocationComponentPrototype", Type = typeof(LocationComponentPrototype))]
        [XmlArrayItem(ElementName = "ResourceContainerComponentPrototype", Type = typeof(ResourceContainerComponentPrototype))]
        [XmlArrayItem(ElementName = "ResourceSourceComponentPrototype", Type = typeof(ResourceSourceComponentPrototype))]
        [XmlArrayItem(ElementName = "ResourceStorageComponentPrototype", Type = typeof(ResourceStorageComponentPrototype))]
        [XmlArrayItem(ElementName = "UnitShelterComponentPrototype", Type = typeof(UnitShelterComponentPrototype))]
        [XmlArrayItem(ElementName = "DurabilityComponentPrototype", Type = typeof(DurabilityComponentPrototype))]
        public List<ComponentPrototype> ComponentPrototypes { get; set; }
    }
}
