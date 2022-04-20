using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Entities.Components;

namespace RuneForge.Content.Pipeline.Game.Entities.ComponentWriters
{
    public class TextureAtlasComponentPrototypeWriter : ComponentPrototypeWriter<TextureAtlasComponentPrototype>
    {
        public override void WriteComponentPrototype(ContentWriter contentWriter, TextureAtlasComponentPrototype componentPrototype)
        {
            contentWriter.Write(componentPrototype.TextureAtlasAssetName);
            contentWriter.Write(componentPrototype.HasPlayerColor);
        }
    }
}
