using System.IO;
using System.Xml.Serialization;

using Microsoft.Xna.Framework.Content.Pipeline;

namespace RuneForge.Content.Pipeline.Core.TextureAtlases
{
    [ContentImporter(".xml", DisplayName = "Texture Atlas Importer - RuneForge", DefaultProcessor = "PassThroughProcessor")]
    public class TextureAtlasImporter : ContentImporter<TextureAtlas>
    {
        public override TextureAtlas Import(string fileName, ContentImporterContext context)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TextureAtlas));
            using StreamReader reader = new StreamReader(fileName);
            return (TextureAtlas)xmlSerializer.Deserialize(reader);
        }
    }
}
