using System.Collections.Generic;
using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
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
