using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Components.Implementations;

namespace RuneForge.Content.Pipeline.Game.Components.PrototypeWriters
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
