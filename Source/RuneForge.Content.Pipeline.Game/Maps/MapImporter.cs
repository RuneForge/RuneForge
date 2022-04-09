using System.IO;
using System.Xml.Serialization;

using Microsoft.Xna.Framework.Content.Pipeline;

namespace RuneForge.Content.Pipeline.Game.Maps
{
    [ContentImporter(".xml", DisplayName = "Map Importer - RuneForge", DefaultProcessor = "PassThroughProcessor")]
    public class MapImporter : ContentImporter<Map>
    {
        public override Map Import(string fileName, ContentImporterContext context)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Map));
            using StreamReader reader = new StreamReader(fileName);
            return (Map)xmlSerializer.Deserialize(reader);
        }
    }
}
