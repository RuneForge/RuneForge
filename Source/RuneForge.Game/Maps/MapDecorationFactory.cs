using RuneForge.Data.Maps;
using RuneForge.Game.Maps.Interfaces;

namespace RuneForge.Game.Maps
{
    public class MapDecorationFactory : IMapDecorationFactory
    {
        private int m_nextDecorationId;

        public MapDecorationFactory()
        {
            m_nextDecorationId = 1;
        }

        public MapDecoration CreateFromPrototype(int x, int y, MapDecorationPrototype prototype)
        {
            int id = m_nextDecorationId++;
            return new MapDecoration(id, prototype.Name, x, y);
        }

        public MapDecoration CreateFromDto(MapDecorationDto mapDecorationDto)
        {
            return new MapDecoration(mapDecorationDto.Id, mapDecorationDto.Name, mapDecorationDto.X, mapDecorationDto.Y);
        }
    }
}
