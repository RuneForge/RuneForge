using System.IO;
using System.Xml.Serialization;

using Microsoft.Xna.Framework.Content.Pipeline;

namespace RuneForge.Content.Pipeline.Game.Units
{
    [ContentImporter(".xml", DisplayName = "Unit Prototype Importer - RuneForge", DefaultProcessor = "PassThroughProcessor")]
    public class UnitPrototypeImporter : ContentImporter<UnitPrototype>
    {
        public override UnitPrototype Import(string fileName, ContentImporterContext context)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(UnitPrototype));
            using StreamReader reader = new StreamReader(fileName);
            return (UnitPrototype)xmlSerializer.Deserialize(reader);
        }
    }
}
