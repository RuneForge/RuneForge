using RuneForge.Data.Maps;

namespace RuneForge.Game.Maps.Interfaces
{
    public interface IMapDecorationFactory
    {
        public MapDecoration CreateFromPrototype(int x, int y, MapDecorationPrototype prototype);

        public MapDecoration CreateFromDto(MapDecorationDto mapDecorationDto);
    }
}
