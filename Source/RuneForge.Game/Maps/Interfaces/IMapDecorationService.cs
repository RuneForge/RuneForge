using System.Collections.ObjectModel;

namespace RuneForge.Game.Maps.Interfaces
{
    public interface IMapDecorationService
    {
        public MapDecoration GetMapDecoration(int mapDecorationId);

        public ReadOnlyCollection<MapDecoration> GetMapDecorations();

        public void AddMapDecoration(MapDecoration mapDecoration);

        public void RemoveMapDecoration(int mapDecorationId);
    }
}
