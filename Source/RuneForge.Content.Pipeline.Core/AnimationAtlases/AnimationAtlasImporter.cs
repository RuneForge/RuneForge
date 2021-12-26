using System.IO;
using System.Xml.Serialization;

using Microsoft.Xna.Framework.Content.Pipeline;

namespace RuneForge.Content.Pipeline.Core.AnimationAtlases
{
    [ContentImporter(".xml", DisplayName = "Animation Atlas Importer - RuneForge", DefaultProcessor = "PassThroughProcessor")]
    public class AnimationAtlasImporter : ContentImporter<AnimationAtlas>
    {
        public override AnimationAtlas Import(string fileName, ContentImporterContext context)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(AnimationAtlas));
            using StreamReader reader = new StreamReader(fileName);
            return (AnimationAtlas)xmlSerializer.Deserialize(reader);
        }
    }
}
