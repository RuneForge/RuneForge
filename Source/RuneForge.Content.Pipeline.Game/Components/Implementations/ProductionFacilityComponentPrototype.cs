using System.Collections.Generic;
using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Components.Attributes;
using RuneForge.Content.Pipeline.Game.Components.PrototypeWriters;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
    [ComponentPrototypeWriter(typeof(ProductionFacilityComponentPrototypeWriter))]
    public class ProductionFacilityComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Components.Implementations.ProductionFacilityComponentPrototype, RuneForge.Game";

        [XmlArray(ElementName = "UnitCodesProduced")]
        [XmlArrayItem(ElementName = "UnitCodeProduced")]
        public List<string> UnitCodesProduced { get; set; }

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
