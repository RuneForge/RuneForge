using RuneForge.Game.Entities.Components;

namespace RuneForge.Game.Entities.ComponentFactories
{
    public class TextureAtlasComponentFactory : ComponentFactory<TextureAtlasComponent, TextureAtlasComponentPrototype>
    {
        public override TextureAtlasComponent CreateComponentFromPrototype(TextureAtlasComponentPrototype componentPrototype, TextureAtlasComponentPrototype componentPrototypeOverride)
        {
            return new TextureAtlasComponent(componentPrototype.TextureAtlasAssetName, componentPrototype.HasPlayerColor);
        }
    }
}
