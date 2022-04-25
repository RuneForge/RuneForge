using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
    public class UnitShelterComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Components.Implementations.UnitShelterComponentPrototype, RuneForge.Game";

        [XmlAttribute(AttributeName = "OccupantsLimit")]
        public int OccupantsLimit { get; set; }

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
