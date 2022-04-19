using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Entities;
using RuneForge.Content.Pipeline.Game.Entities.Components;

namespace RuneForge.Content.Pipeline.Game.Units
{
    public class UnitInstancePrototype
    {
        [XmlElement(ElementName = "OwnerId")]
        public Guid OwnerId { get; set; }

        [XmlElement(ElementName = "EntityPrototypeAssetName")]
        public string EntityPrototypeAssetName { get; set; }

        [XmlArray(ElementName = "ComponentPrototypeOverrides")]
        [XmlArrayItem(ElementName = "LocationComponentPrototype", Type = typeof(LocationComponentPrototype))]
        [XmlArrayItem(ElementName = "DirectionComponentPrototype", Type = typeof(DirectionComponentPrototype))]
        public List<ComponentPrototype> ComponentPrototypeOverrides { get; set; }
    }
}
