using RuneForge.Data.Components;
using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class TextureAtlasComponentFactory : ComponentFactory<TextureAtlasComponent, TextureAtlasComponentPrototype, TextureAtlasComponentDto>
    {
        public override TextureAtlasComponent CreateComponentFromPrototype(TextureAtlasComponentPrototype componentPrototype, TextureAtlasComponentPrototype componentPrototypeOverride)
        {
            return new TextureAtlasComponent(componentPrototype.TextureAtlasAssetName, componentPrototype.HasPlayerColor);
        }

        public override TextureAtlasComponent CreateComponentFromDto(TextureAtlasComponentDto componentDto)
        {
            return new TextureAtlasComponent(componentDto.TextureAtlasAssetName, componentDto.HasPlayerColor);
        }
    }
}
