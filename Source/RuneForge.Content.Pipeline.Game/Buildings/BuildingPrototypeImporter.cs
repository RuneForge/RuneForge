using System.IO;
using System.Xml.Serialization;

using Microsoft.Xna.Framework.Content.Pipeline;

namespace RuneForge.Content.Pipeline.Game.Buildings
{
    [ContentImporter(".xml", DisplayName = "Building Prototype Importer - RuneForge", DefaultProcessor = "PassThroughProcessor")]
    public class BuildingPrototypeImporter : ContentImporter<BuildingPrototype>
    {
        public override BuildingPrototype Import(string fileName, ContentImporterContext context)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(BuildingPrototype));
            using StreamReader reader = new StreamReader(fileName);
            return (BuildingPrototype)xmlSerializer.Deserialize(reader);
        }
    }
}
