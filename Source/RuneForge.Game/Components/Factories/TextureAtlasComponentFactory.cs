using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class TextureAtlasComponentFactory : ComponentFactory<TextureAtlasComponent, TextureAtlasComponentPrototype>
    {
        public override TextureAtlasComponent CreateComponentFromPrototype(TextureAtlasComponentPrototype componentPrototype, TextureAtlasComponentPrototype componentPrototypeOverride)
        {
            return new TextureAtlasComponent(componentPrototype.TextureAtlasAssetName, componentPrototype.HasPlayerColor);
        }
    }
}
